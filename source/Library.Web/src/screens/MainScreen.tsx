import React, { useState } from 'react';
import { View, StyleSheet } from 'react-native';
import { User, Book } from '../types';
import { COLORS } from '../constants';
import { useBooks } from '../hooks/useBooks';
import { useOrganizations } from '../hooks/useOrganizations';
import { TabContent } from '../components/TabContent';
import { BookForm } from '../components/BookForm';
import { BookDetailScreen } from './BookDetailScreen';
import { UserProfileScreen } from './UserProfileScreen';
import { BookVisibilityModal } from '../components/BookVisibilityModal';
import { LoadingScreen } from '../components/LoadingScreen';
import { OrganizationForm } from '../components/OrganizationForm';

interface MainScreenProps {
  user: User;
  onLogout: () => void;
  activeTab?: string;
}

export const MainScreen: React.FC<MainScreenProps> = ({ 
  user, 
  onLogout, 
  activeTab = 'books' 
}) => {
  // Hooks
    const booksHook = useBooks({ 
    currentUserId: user.userId
  });
  const organizationsHook = useOrganizations();

  // UI State
  const [showBookForm, setShowBookForm] = useState(false);
  const [showBookDetail, setShowBookDetail] = useState(false);
  const [selectedBook, setSelectedBook] = useState<Book | null>(null);
  const [showUserProfile, setShowUserProfile] = useState(false);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [showVisibilityModal, setShowVisibilityModal] = useState(false);
  const [selectedBookForVisibility, setSelectedBookForVisibility] = useState<Book | null>(null);
  const [showOrganizationForm, setShowOrganizationForm] = useState(false);

  // Handlers
  const handleBookPress = (book: Book) => {
    setSelectedBook(book);
    setShowBookDetail(true);
  };

  const handleAddBook = async (bookData: Omit<Book, 'bookId' | 'createdAt'>) => {
    try {
      await booksHook.addBook(bookData, user);
      setShowBookForm(false);
    } catch (error) {
      console.error('Erro ao adicionar livro:', error);
      // O erro já foi tratado no hook
    }
  };

  const handleEditBook = (book: Book) => {
    setSelectedBook(book);
    setShowBookForm(true);
    setShowBookDetail(false);
  };

  const handleDeleteBook = (bookId: number) => {
    booksHook.deleteBook(bookId);
  };

  const handleViewUserProfile = (user: User) => {
    setSelectedUser(user);
    setShowUserProfile(true);
  };

  const handleOpenVisibilityModal = (book: Book) => {
    setSelectedBookForVisibility(book);
    setShowVisibilityModal(true);
  };

  const handleSaveVisibilitySettings = async (visibilitySettings: Book['visibilitySettings']) => {
    if (!selectedBookForVisibility) return;
    booksHook.updateBookVisibility(selectedBookForVisibility.bookId, visibilitySettings);
    setShowVisibilityModal(false);
    setSelectedBookForVisibility(null);
  };

  const handleFiltersChange = (newFilters: any) => {
    booksHook.setSearchQuery(newFilters.searchQuery);
    booksHook.setSelectedGenres(newFilters.selectedGenres);
    booksHook.setSelectedStatuses(newFilters.selectedStatuses);
    booksHook.setSelectedOrganizations(newFilters.selectedOrganizations);
  };

  const handleCreateOrganization = async (orgData: any) => {
    try {
      console.log('🏢 Criando organização:', orgData);
      
      const newOrganization = await organizationsHook.createOrganization(orgData, user.userId);
      
      if (newOrganization) {
        console.log('✅ Organização criada com sucesso:', newOrganization);
      }
    } catch (error) {
      console.error('❌ Erro ao criar organização:', error);
    }
  };

  // Current filters state
  const filters = {
    searchQuery: booksHook.searchQuery,
    selectedGenres: booksHook.selectedGenres,
    selectedStatuses: booksHook.selectedStatuses,
    selectedOrganizations: booksHook.selectedOrganizations,
  };

  // Render conditions
  if (booksHook.isLoading) {
    return <LoadingScreen message="Carregando livros..." />;
  }

  if (showBookForm) {
    return (
      <BookForm
        initialData={selectedBook || undefined}
        onSave={handleAddBook}
        onCancel={() => {
          setShowBookForm(false);
          setSelectedBook(null);
        }}
        title={selectedBook ? 'Editar Livro' : 'Adicionar Livro'}
      />
    );
  }

  if (showBookDetail && selectedBook) {
    return (
      <>
        <BookDetailScreen 
          book={selectedBook} 
          currentUserId={user.userId}
          isVisible={showBookDetail}
          onClose={() => setShowBookDetail(false)}
          onEdit={handleEditBook}
          onDelete={handleDeleteBook}
          onRequestLoan={(bookId) => {
            console.log('Request loan:', bookId);
            setShowBookDetail(false);
          }}
          onReturnBook={(bookId) => {
            console.log('Return book:', bookId);
            setShowBookDetail(false);
          }}
          onViewUserProfile={handleViewUserProfile}
          onOpenVisibility={handleOpenVisibilityModal}
        />
        
        {selectedBookForVisibility && (
          <BookVisibilityModal
            book={selectedBookForVisibility}
            organizations={organizationsHook.organizations}
            isVisible={showVisibilityModal}
            onClose={() => {
              setShowVisibilityModal(false);
              setSelectedBookForVisibility(null);
            }}
            onSave={handleSaveVisibilitySettings}
          />
        )}
      </>
    );
  }

  if (showUserProfile && selectedUser) {
    const userBooks = booksHook.allBooks.filter(book => book.ownerId === selectedUser.userId);
    const fullUserData = selectedUser;
    
    return (
      <>
        <UserProfileScreen
          user={fullUserData}
          userBooks={userBooks}
          currentUserId={user.userId}
          isVisible={showUserProfile}
          onClose={() => {
            setShowUserProfile(false);
            setSelectedUser(null);
          }}
          onRequestLoan={(bookId) => console.log('Request loan:', bookId)}
          onContactUser={(userId) => console.log('Contact user:', userId)}
          organizationSettings={{
            showAdditionalInfo: false,
            showPhone: false,
            showAddress: false,
            showPersonalInfo: false,
          }}
        />
        
        {selectedBookForVisibility && (
          <BookVisibilityModal
            book={selectedBookForVisibility}
            organizations={organizationsHook.organizations}
            isVisible={showVisibilityModal}
            onClose={() => {
              setShowVisibilityModal(false);
              setSelectedBookForVisibility(null);
            }}
            onSave={handleSaveVisibilitySettings}
          />
        )}
      </>
    );
  }

  return (
    <>
      <View style={styles.contentArea}>
        <TabContent
          activeTab={activeTab}
          filteredBooks={booksHook.filteredBooks}
          myBooks={booksHook.myBooks}
          onBookPress={handleBookPress}
          onFiltersChange={handleFiltersChange}
          filters={filters}
          organizations={organizationsHook.organizations}
          user={user}
          onLogout={onLogout}
          onAddBook={() => setShowBookForm(true)}
          onEditBook={handleEditBook}
          onDeleteBook={handleDeleteBook}
          onViewBook={handleBookPress}
          onViewOrganization={(org) => console.log('Ver organização:', org)}
          onCreateOrganization={() => setShowOrganizationForm(true)}
          onViewLoan={(loan) => console.log('Ver empréstimo:', loan)}
          onUpdateProfile={async (updatedUser) => {
            console.log('Atualizando perfil:', updatedUser);
            return Promise.resolve();
          }}
        />
      </View>

      {selectedBookForVisibility && (
        <BookVisibilityModal
          book={selectedBookForVisibility}
          organizations={organizationsHook.organizations}
          isVisible={showVisibilityModal}
          onClose={() => {
            setShowVisibilityModal(false);
            setSelectedBookForVisibility(null);
          }}
          onSave={handleSaveVisibilitySettings}
        />
      )}

      <OrganizationForm
        isVisible={showOrganizationForm}
        onClose={() => setShowOrganizationForm(false)}
        onSubmit={handleCreateOrganization}
        isLoading={organizationsHook.isLoading}
      />
    </>
  );
};

const styles = StyleSheet.create({
  contentArea: {
    flex: 1,
    overflow: 'scroll',
  },
}); 
