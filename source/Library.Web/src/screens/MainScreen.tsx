import React, { useState } from 'react';
import * as ImagePicker from 'expo-image-picker';
import {
  View,
  Text,
  TouchableOpacity,
  ScrollView,
  StyleSheet,
  SafeAreaView,
  Alert,
  TextInput,
  KeyboardAvoidingView,
  Platform,
  Image,
  Modal,
} from 'react-native';
import { User, Book, GENRES, getGenreText, getStatusText, getStatusColor } from '../types';
import { COLORS, SPACING, FONT_SIZES, FONT_WEIGHTS, BORDER_RADIUS, SHADOWS } from '../constants';
import { ProfileHeader } from '../components/ProfileHeader';
import { OrganizationsSection } from '../components/OrganizationsSection';
import { MyBooksSection } from '../components/MyBooksSection';
import { LoanHistorySection } from '../components/LoanHistorySection';
import { BookDetailScreen } from '../components/BookDetailScreen';
import { UserProfileScreen } from '../components/UserProfileScreen';
import { BookVisibilityModal } from '../components/BookVisibilityModal';
import { AdvancedFilters } from '../components/AdvancedFilters';
import { OrganizationRulesForm } from '../components/OrganizationRulesForm';
import { ExtendedOrganization, Organization, OrganizationRules } from '../types';

interface MainScreenProps {
  user: User;
  onLogout: () => void;
}

export const MainScreen: React.FC<MainScreenProps> = ({ user, onLogout }) => {
  const [activeTab, setActiveTab] = useState('profile');
  const [myBooks, setMyBooks] = useState<Book[]>([]);
  const [allBooks, setAllBooks] = useState<Book[]>([]);
  const [showAddBookForm, setShowAddBookForm] = useState(false);
  const [showBookDetail, setShowBookDetail] = useState(false);
  const [selectedBook, setSelectedBook] = useState<Book | null>(null);
  const [showUserProfile, setShowUserProfile] = useState(false);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [showVisibilityModal, setShowVisibilityModal] = useState(false);
  const [selectedBookForVisibility, setSelectedBookForVisibility] = useState<Book | null>(null);
  const [showEditBookForm, setShowEditBookForm] = useState(false);
  const [editingBook, setEditingBook] = useState<Book | null>(null);
  const [selectedOrganization, setSelectedOrganization] = useState<any>(null);
  const [showOrganizationDetail, setShowOrganizationDetail] = useState(false);
  const [showCreateOrganization, setShowCreateOrganization] = useState(false);
  const [createOrgForm, setCreateOrgForm] = useState<{
    name: string;
    description: string;
    image: string | null;
    rules: OrganizationRules;
  }>({
    name: '',
    description: '',
    image: null,
    rules: {
      loanDurationDays: 30,
      meetingFrequency: 'na',
      requireCompleteUserInfo: false,
    },
  });
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedGenres, setSelectedGenres] = useState<number[]>([]);
  const [selectedStatuses, setSelectedStatuses] = useState<number[]>([1]); // Filtro inicial: apenas disponíveis
  const [selectedOrganizations, setSelectedOrganizations] = useState<number[]>([]);

  // Dados de exemplo para organizações
  const sampleOrganizations: ExtendedOrganization[] = [
    {
      id: 1,
      name: 'Biblioteca Comunitária',
      description: 'Uma biblioteca colaborativa onde membros compartilham seus livros pessoais.',
      memberCount: 15,
      role: 'Admin' as const,
      isActive: true,
      image: 'https://via.placeholder.com/100',
      createdAt: '2023-01-01',
      rules: {
        loanDurationDays: 30,
        meetingFrequency: 'weekly' as const,
        meetingDay: 'saturday' as const,
        meetingTime: '14:00',
        requireCompleteUserInfo: true
      },
      regulations: [
        'Cada membro pode emprestar até 3 livros simultaneamente',
        'O prazo de empréstimo é de 30 dias',
        'Renovação pode ser solicitada até 3 dias antes do vencimento',
        'Livros atrasados geram multa de R$ 1,00 por dia',
        'Livros danificados devem ser repostos pelo usuário',
        'Respeite o próximo leitor - mantenha os livros em bom estado',
        'Para dúvidas, entre em contato com a administração'
      ],
      stats: {
        totalBooks: 45,
        activeLoans: 8,
        totalMembers: 15,
        monthlyLoans: 12
      }
    },
    {
      id: 2,
      name: 'Clube de Leitura',
      description: 'Grupo de discussão sobre livros e literatura.',
      memberCount: 8,
      role: 'Leader' as const,
      isActive: true,
      image: 'https://via.placeholder.com/100',
      createdAt: '2023-02-01',
      rules: {
        loanDurationDays: 21,
        meetingFrequency: 'weekly' as const,
        meetingDay: 'tuesday' as const,
        meetingTime: '19:00',
        requireCompleteUserInfo: false
      },
      regulations: [
        'Reuniões semanais às terças-feiras às 19h',
        'Cada membro deve ler pelo menos 1 livro por mês',
        'Discussões devem ser respeitosas e construtivas',
        'Livros são compartilhados entre membros do clube',
        'Resenhas são incentivadas e compartilhadas',
        'Eventos especiais são organizados mensalmente',
        'Para participar, entre em contato com o líder do clube'
      ],
      stats: {
        totalBooks: 25,
        activeLoans: 3,
        totalMembers: 8,
        monthlyLoans: 5
      }
    },
    {
      id: 3,
      name: 'Grupo de Estudos',
      description: 'Foco em livros técnicos e acadêmicos.',
      memberCount: 12,
      role: 'Member' as const,
      isActive: true,
      image: 'https://via.placeholder.com/100',
      createdAt: '2023-03-01',
      rules: {
        loanDurationDays: 15,
        meetingFrequency: 'monthly' as const,
        meetingWeek: 2 as const,
        meetingTime: '18:00',
        requireCompleteUserInfo: true
      },
      regulations: [
        'Foco em livros técnicos e acadêmicos',
        'Sessões de estudo em grupo às quartas-feiras',
        'Cada membro deve contribuir com pelo menos 2 livros técnicos',
        'Discussões técnicas são incentivadas',
        'Livros devem ser mantidos em excelente estado',
        'Empréstimos por até 15 dias para livros técnicos',
        'Para dúvidas técnicas, consulte os moderadores'
      ],
      stats: {
        totalBooks: 35,
        activeLoans: 5,
        totalMembers: 12,
        monthlyLoans: 8
      }
    },
  ];

  // Dados de exemplo para organizações (formato para o modal de visibilidade)
  const sampleOrganizationsForVisibility: Organization[] = [
    {
      organizationId: 1,
      name: 'Biblioteca Comunitária',
      description: 'Uma biblioteca colaborativa onde membros compartilham seus livros pessoais.',
      memberCount: 15,
      createdAt: '2023-01-01',
      rules: {
        loanDurationDays: 30,
        meetingFrequency: 'weekly' as const,
        meetingDay: 'saturday' as const,
        meetingTime: '14:00',
        requireCompleteUserInfo: false
      },
    },
    {
      organizationId: 2,
      name: 'Clube de Leitura',
      description: 'Grupo de discussão sobre livros e literatura.',
      memberCount: 8,
      createdAt: '2023-02-01',
      rules: {
        loanDurationDays: 21,
        meetingFrequency: 'weekly' as const,
        meetingDay: 'tuesday' as const,
        meetingTime: '19:00',
        requireCompleteUserInfo: false
      },
    },
    {
      organizationId: 3,
      name: 'Grupo de Estudos',
      description: 'Foco em livros técnicos e acadêmicos.',
      memberCount: 12,
      createdAt: '2023-03-01',
      rules: {
        loanDurationDays: 15,
        meetingFrequency: 'monthly' as const,
        meetingWeek: 2 as const,
        meetingTime: '18:00',
        requireCompleteUserInfo: true,
      },
    },
    {
      organizationId: 4,
      name: 'Clube de Fantasia',
      description: 'Especializado em literatura fantástica.',
      memberCount: 6,
      createdAt: '2023-04-01',
      rules: {
        loanDurationDays: 30,
        meetingFrequency: 'na' as const,
        requireCompleteUserInfo: false,
      },
    },
  ];



  // Dados de exemplo para histórico de empréstimos
  const sampleLoanHistory = [
    {
      id: 1,
      bookName: 'O Senhor dos Anéis',
      bookAuthor: 'J.R.R. Tolkien',
      borrowerName: 'Maria Silva',
      lenderName: user.name,
      loanDate: '2024-01-15',
      returnDate: '2024-02-15',
      status: 'Active' as const,
      isLent: true,
    },
    {
      id: 2,
      bookName: '1984',
      bookAuthor: 'George Orwell',
      borrowerName: user.name,
      lenderName: 'João Santos',
      loanDate: '2024-01-10',
      returnDate: '2024-02-10',
      status: 'Returned' as const,
      isLent: false,
    },
    {
      id: 3,
      bookName: 'Dom Casmurro',
      bookAuthor: 'Machado de Assis',
      borrowerName: 'Ana Costa',
      lenderName: user.name,
      loanDate: '2024-01-05',
      returnDate: '2024-02-05',
      status: 'Overdue' as const,
      isLent: true,
    },
  ];

  // Dados de exemplo de usuários
  const sampleUsers: User[] = [
    {
      userId: 999,
      name: 'João Santos',
      email: 'joao@email.com',
      image: 'https://via.placeholder.com/100',
      phone: '(11) 99999-9999',
      address: 'São Paulo, SP',
      additionalInfo: 'Apaixonado por fantasia e ficção científica',
      birthDay: '1985-03-15',
      createdAt: '2023-06-01',
    },
    {
      userId: 888,
      name: 'Ana Costa',
      email: 'ana@email.com',
      image: 'https://via.placeholder.com/100',
      phone: '(11) 88888-8888',
      address: 'São Paulo, SP',
      additionalInfo: 'Líder do Clube de Leitura',
      birthDay: '1990-07-22',
      createdAt: '2023-08-15',
    },
    {
      userId: 777,
      name: 'Pedro Oliveira',
      email: 'pedro@email.com',
      image: 'https://via.placeholder.com/100',
      phone: '(11) 77777-7777',
      address: 'São Paulo, SP',
      additionalInfo: 'Admin da Biblioteca Comunitária',
      birthDay: '1982-11-08',
      createdAt: '2023-01-10',
    },
    {
      userId: 666,
      name: 'Carla Mendes',
      email: 'carla@email.com',
      image: 'https://via.placeholder.com/100',
      phone: '(11) 66666-6666',
      address: 'São Paulo, SP',
      additionalInfo: 'Membro ativo do Clube de Leitura',
      birthDay: '1988-04-30',
      createdAt: '2023-09-20',
    },
  ];

  // Sample data for books
  const sampleBooks: Book[] = [
    // Meus Livros (que eu possuo)
    {
      bookId: 1,
      name: 'O Senhor dos Anéis',
      author: 'J.R.R. Tolkien',
      genre: 5, // Fantasy
      description: 'Uma épica aventura de fantasia',
      url: 'https://example.com/book1',
      image: 'https://via.placeholder.com/150',
      bookStatusId: 1, // Available
      ownerId: user.userId,
      createdAt: '2024-01-01',
      visibilitySettings: {
        isPublic: true,
        visibleOrganizations: [],
        hiddenOrganizations: [4], // Oculto do Clube de Fantasia
      },
    },
    {
      bookId: 2,
      name: '1984',
      author: 'George Orwell',
      genre: 9, // ScienceFiction
      description: 'Distopia clássica',
      url: 'https://example.com/book2',
      image: 'https://via.placeholder.com/150',
      bookStatusId: 1, // Available
      ownerId: user.userId,
      createdAt: '2024-01-02',
    },
    {
      bookId: 3,
      name: 'Dom Casmurro',
      author: 'Machado de Assis',
      genre: 4, // Drama
      description: 'Romance brasileiro clássico',
      url: 'https://example.com/book3',
      image: 'https://via.placeholder.com/150',
      bookStatusId: 2, // Lent - emprestado para alguém
      ownerId: user.userId,
      createdAt: '2024-01-03',
      loanInfo: {
        borrowerId: 555,
        borrowerName: 'Maria Silva',
        borrowerEmail: 'maria@email.com',
        loanDate: '2024-01-20',
        returnDate: '2024-02-20',
        isOverdue: false,
      },
    },
    // Livros que Estou Lendo (emprestados de outros usuários)
    {
      bookId: 4,
      name: 'O Hobbit',
      author: 'J.R.R. Tolkien',
      genre: 5, // Fantasy
      description: 'Aventura de um hobbit',
      url: 'https://example.com/book4',
      image: 'https://via.placeholder.com/150',
      bookStatusId: 2, // Lent
      ownerId: 999, // Outro usuário
      createdAt: '2024-01-04',
      loanInfo: {
        borrowerId: user.userId,
        borrowerName: user.name,
        borrowerEmail: user.email,
        loanDate: '2024-01-15',
        returnDate: '2024-02-15',
        isOverdue: false,
      },
      ownerInfo: {
        name: 'João Santos',
        email: 'joao@email.com',
        image: 'https://via.placeholder.com/100',
        organization: 'Biblioteca Comunitária',
        organizationRole: 'Membro',
        totalBooks: 25,
        booksLent: 8,
        booksAvailable: 17,
      },
    },
    {
      bookId: 5,
      name: 'A Revolução dos Bichos',
      author: 'George Orwell',
      genre: 9, // ScienceFiction
      description: 'Fábula política',
      url: 'https://example.com/book5',
      image: 'https://via.placeholder.com/150',
      bookStatusId: 2, // Lent
      ownerId: 888, // Outro usuário
      createdAt: '2024-01-05',
      loanInfo: {
        borrowerId: user.userId,
        borrowerName: user.name,
        borrowerEmail: user.email,
        loanDate: '2024-01-10',
        returnDate: '2024-02-10',
        isOverdue: true,
      },
      ownerInfo: {
        name: 'Ana Costa',
        email: 'ana@email.com',
        image: 'https://via.placeholder.com/100',
        organization: 'Clube de Leitura',
        organizationRole: 'Líder',
        totalBooks: 18,
        booksLent: 5,
        booksAvailable: 13,
      },
    },
    // Lista de Espera (solicitados)
    {
      bookId: 6,
      name: 'Duna',
      author: 'Frank Herbert',
      genre: 9, // ScienceFiction
      description: 'Épico de ficção científica',
      url: 'https://example.com/book6',
      image: 'https://via.placeholder.com/150',
      bookStatusId: 3, // Reserved
      ownerId: 777, // Outro usuário
      createdAt: '2024-01-06',
      ownerInfo: {
        name: 'Pedro Oliveira',
        email: 'pedro@email.com',
        image: 'https://via.placeholder.com/100',
        organization: 'Biblioteca Comunitária',
        organizationRole: 'Admin',
        totalBooks: 32,
        booksLent: 12,
        booksAvailable: 20,
      },
    },
    {
      bookId: 7,
      name: 'O Guia do Mochileiro das Galáxias',
      author: 'Douglas Adams',
      genre: 3, // Comedy
      description: 'Comédia de ficção científica',
      url: 'https://example.com/book7',
      image: 'https://via.placeholder.com/150',
      bookStatusId: 3, // Reserved
      ownerId: 666, // Outro usuário
      createdAt: '2024-01-07',
      ownerInfo: {
        name: 'Carla Mendes',
        email: 'carla@email.com',
        image: 'https://via.placeholder.com/100',
        organization: 'Clube de Leitura',
        organizationRole: 'Membro',
        totalBooks: 15,
        booksLent: 3,
        booksAvailable: 12,
      },
    },
  ];

  // Initialize sample books
  React.useEffect(() => {
    setMyBooks(sampleBooks);
    setAllBooks(sampleBooks);
  }, []);

  const handleUpdateProfile = async (updatedUser: Partial<User>) => {
    // Aqui você implementaria a chamada para a API
    console.log('Atualizando perfil:', updatedUser);
    // Simular sucesso
    return Promise.resolve();
  };

  const handleViewOrganization = (org: any) => {
    setSelectedOrganization(org);
    setShowOrganizationDetail(true);
  };

  const handleCreateOrganization = () => {
    setShowCreateOrganization(true);
  };

  const handleCloseCreateOrganization = () => {
    setShowCreateOrganization(false);
    setCreateOrgForm({
      name: '',
      description: '',
      image: null,
          rules: {
      loanDurationDays: 30,
      meetingFrequency: 'na',
      requireCompleteUserInfo: false,
    },
    });
  };

  const handleSaveOrganization = () => {
    if (!createOrgForm.name.trim()) {
      Alert.alert('Erro', 'Nome da organização é obrigatório');
      return;
    }

    if (!createOrgForm.description.trim()) {
      Alert.alert('Erro', 'Descrição da organização é obrigatória');
      return;
    }

    // Criar nova organização
    const newOrganization: ExtendedOrganization = {
      id: sampleOrganizations.length + 1,
      name: createOrgForm.name,
      description: createOrgForm.description,
      memberCount: 1, // Apenas o criador
      role: 'Admin',
      isActive: true,
      image: createOrgForm.image || 'https://via.placeholder.com/100',
      createdAt: new Date().toISOString().split('T')[0],
      rules: createOrgForm.rules,
      regulations: [
        'Cada membro pode emprestar até 3 livros simultaneamente',
        'O prazo de empréstimo é de 30 dias',
        'Renovação pode ser solicitada até 3 dias antes do vencimento',
        'Livros atrasados geram multa de R$ 1,00 por dia',
        'Livros danificados devem ser repostos pelo usuário',
        'Respeite o próximo leitor - mantenha os livros em bom estado',
        'Para dúvidas, entre em contato com a administração'
      ],
      stats: {
        totalBooks: 0,
        activeLoans: 0,
        totalMembers: 1,
        monthlyLoans: 0
      }
    };

    // Adicionar à lista (em um app real, seria uma chamada API)
    sampleOrganizations.push(newOrganization);

    Alert.alert('Sucesso', 'Organização criada com sucesso!');
    handleCloseCreateOrganization();
  };

  // Funções de filtro
  const getFilteredBooks = () => {
    // Filtrar apenas livros que NÃO são meus (livros de outros usuários)
    let filtered = allBooks.filter(book => book.ownerId !== user.userId);

    // Filtro por texto
    if (searchQuery.trim()) {
      const query = searchQuery.toLowerCase();
      filtered = filtered.filter(book => 
        book.name.toLowerCase().includes(query) ||
        book.author.toLowerCase().includes(query) ||
        getGenreText(book.genre).toLowerCase().includes(query)
      );
    }

    // Filtro por gêneros (múltipla seleção)
    if (selectedGenres.length > 0) {
      filtered = filtered.filter(book => selectedGenres.includes(book.genre));
    }

    // Filtro por status (múltipla seleção)
    if (selectedStatuses.length > 0) {
      filtered = filtered.filter(book => selectedStatuses.includes(book.bookStatusId));
    }

    // Filtro por organizações (múltipla seleção)
    if (selectedOrganizations.length > 0) {
      filtered = filtered.filter(book => {
        // Simular que alguns livros pertencem a organizações específicas
        // Em um app real, isso viria do backend
        const orgId = Math.ceil(book.ownerId / 5);
        return selectedOrganizations.includes(orgId);
      });
    }

    return filtered;
  };

  const clearFilters = () => {
    setSearchQuery('');
    setSelectedGenres([]);
    setSelectedStatuses([1]); // Manter apenas disponíveis como padrão
    setSelectedOrganizations([]);
  };

  // Funções para toggle dos filtros
  const toggleGenre = (genreId: number) => {
    setSelectedGenres(prev => 
      prev.includes(genreId) 
        ? prev.filter(id => id !== genreId)
        : [...prev, genreId]
    );
  };

  const toggleStatus = (statusId: number) => {
    setSelectedStatuses(prev => 
      prev.includes(statusId) 
        ? prev.filter(id => id !== statusId)
        : [...prev, statusId]
    );
  };

  const toggleOrganization = (orgId: number) => {
    setSelectedOrganizations(prev => 
      prev.includes(orgId) 
        ? prev.filter(id => id !== orgId)
        : [...prev, orgId]
    );
  };

  // Estatísticas gerais das organizações
  const getOrganizationStats = () => {
    const totalBooks = sampleOrganizations.reduce((sum, org) => sum + org.stats.totalBooks, 0);
    const totalMembers = sampleOrganizations.reduce((sum, org) => sum + org.stats.totalMembers, 0);
    const activeLoans = sampleOrganizations.reduce((sum, org) => sum + org.stats.activeLoans, 0);
    const monthlyLoans = sampleOrganizations.reduce((sum, org) => sum + org.stats.monthlyLoans, 0);

    return {
      totalBooks,
      totalMembers,
      activeLoans,
      monthlyLoans,
      totalOrganizations: sampleOrganizations.length,
    };
  };

  const handleCloseOrganizationDetail = () => {
    setShowOrganizationDetail(false);
    setSelectedOrganization(null);
  };

  const handleEditBook = (book: Book) => {
    console.log('handleEditBook chamada com:', book);
    setEditingBook(book);
    setShowEditBookForm(true);
    setShowBookDetail(false); // Fechar a tela de detalhes
    console.log('Estado atualizado - editingBook:', book, 'showEditBookForm: true');
  };

  const handleDeleteBook = (bookId: number) => {
    setMyBooks(prev => prev.filter(book => book.bookId !== bookId));
    Alert.alert('Sucesso', 'Livro excluído com sucesso!');
  };

  const handleViewBook = (book: Book) => {
    setSelectedBook(book);
    setShowBookDetail(true);
  };

  const handleViewLoan = (loan: any) => {
    Alert.alert('Detalhes do Empréstimo', `Visualizando empréstimo de ${loan.bookName}`);
  };

  const handleViewUserProfile = (user: User) => {
    console.log('handleViewUserProfile chamada com:', user);
    setSelectedUser(user);
    setShowUserProfile(true);
    console.log('Estado atualizado - selectedUser:', user, 'showUserProfile: true');
  };

  const handleRequestLoan = (bookId: number) => {
    Alert.alert('Solicitar Empréstimo', 'Solicitação enviada com sucesso!');
  };

  const handleContactUser = (userId: number) => {
    Alert.alert('Contato', 'Funcionalidade de contato será implementada');
  };

  const handleOpenVisibilityModal = (book: Book) => {
    console.log('handleOpenVisibilityModal chamada com:', book);
    setSelectedBookForVisibility(book);
    setShowVisibilityModal(true);
    console.log('Estado atualizado - selectedBookForVisibility:', book, 'showVisibilityModal: true');
  };

  // Funções para seleção de imagem
  const requestPermissions = async () => {
    const { status } = await ImagePicker.requestMediaLibraryPermissionsAsync();
    if (status !== 'granted') {
      Alert.alert('Permissão necessária', 'Precisamos de permissão para acessar sua galeria.');
      return false;
    }
    return true;
  };

  const requestCameraPermissions = async () => {
    const { status } = await ImagePicker.requestCameraPermissionsAsync();
    if (status !== 'granted') {
      Alert.alert('Permissão necessária', 'Precisamos de permissão para acessar sua câmera.');
      return false;
    }
    return true;
  };

  const pickImage = async () => {
    const hasPermission = await requestPermissions();
    if (!hasPermission) return;

    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [3, 4],
      quality: 0.8,
    });

    if (!result.canceled && result.assets[0]) {
      setEditBookImage(result.assets[0].uri);
    }
  };

  const takePhoto = async () => {
    const hasPermission = await requestCameraPermissions();
    if (!hasPermission) return;

    const result = await ImagePicker.launchCameraAsync({
      allowsEditing: true,
      aspect: [3, 4],
      quality: 0.8,
    });

    if (!result.canceled && result.assets[0]) {
      setEditBookImage(result.assets[0].uri);
    }
  };

  const showImagePickerOptions = () => {
    Alert.alert(
      'Selecionar Imagem',
      'Escolha uma opção:',
      [
        { text: 'Cancelar', style: 'cancel' },
        { text: '📷 Tirar Foto', onPress: takePhoto },
        { text: '🖼️ Escolher da Galeria', onPress: pickImage },
      ]
    );
  };

  const handleSaveVisibilitySettings = async (visibilitySettings: Book['visibilitySettings']) => {
    if (!selectedBookForVisibility) return;

    // Atualizar o livro com as novas configurações de visibilidade
    const updatedBooks = myBooks.map(book => 
      book.bookId === selectedBookForVisibility.bookId 
        ? { ...book, visibilitySettings }
        : book
    );
    setMyBooks(updatedBooks);

    // Atualizar também na lista geral de livros
    const updatedAllBooks = allBooks.map(book => 
      book.bookId === selectedBookForVisibility.bookId 
        ? { ...book, visibilitySettings }
        : book
    );
    setAllBooks(updatedAllBooks);

    setShowVisibilityModal(false);
    setSelectedBookForVisibility(null);
  };

  // Configurações da organização - pode ser carregada da API
  const getOrganizationSettings = (organizationId?: number) => {
    // Por padrão, todas as informações adicionais ficam ocultas
    const defaultSettings = {
      showAdditionalInfo: false,
      showPhone: false,
      showAddress: false,
      showPersonalInfo: false,
    };

    // Exemplo: organização 1 permite mostrar telefone e informações pessoais
    if (organizationId === 1) {
      return {
        showAdditionalInfo: true,
        showPhone: true,
        showAddress: false,
        showPersonalInfo: true,
      };
    }

    // Exemplo: organização 2 permite mostrar todas as informações
    if (organizationId === 2) {
      return {
        showAdditionalInfo: true,
        showPhone: true,
        showAddress: true,
        showPersonalInfo: true,
      };
    }

    // Para teste: mostrar algumas informações (remover em produção)
    // return {
    //   showAdditionalInfo: true,
    //   showPhone: true,
    //   showAddress: true,
    //   showPersonalInfo: true,
    // };

    return defaultSettings;
  };

  const renderBooksTab = () => {
    const filteredBooks = getFilteredBooks();

    // Estado dos filtros
    const filterState = {
      searchQuery,
      selectedGenres,
      selectedStatuses,
      selectedOrganizations,
    };

    const handleFiltersChange = (newFilters: any) => {
      setSearchQuery(newFilters.searchQuery);
      setSelectedGenres(newFilters.selectedGenres);
      setSelectedStatuses(newFilters.selectedStatuses);
      setSelectedOrganizations(newFilters.selectedOrganizations);
    };

    return (
      <View style={styles.tabContent}>
        <Text style={styles.tabTitle}>Biblioteca</Text>
        <Text style={styles.tabSubtitle}>Livros disponíveis de outros usuários</Text>

        {/* Filtros Avançados Foldable */}
        <AdvancedFilters
          filters={filterState}
          onFiltersChange={handleFiltersChange}
          organizations={sampleOrganizations.map(org => ({ id: org.id, name: org.name }))}
          isExpanded={false}
          onToggle={(isExpanded) => console.log('Filters expanded:', isExpanded)}
        />

        {/* Lista de Livros */}
        <View style={styles.booksSection}>
          <Text style={styles.booksSectionTitle}>
            📚 Livros Disponíveis ({filteredBooks.length})
          </Text>
          <ScrollView style={styles.booksList}>
            {filteredBooks.map((book) => (
              <TouchableOpacity
                key={book.bookId}
                style={styles.bookCard}
                onPress={() => handleViewBook(book)}
                activeOpacity={0.7}
              >
                <View style={styles.bookImageContainer}>
                  {book.image ? (
                    <Image source={{ uri: book.image }} style={styles.bookImage} />
                  ) : (
                    <Text style={styles.bookImagePlaceholder}>📚</Text>
                  )}
                </View>
                <View style={styles.bookInfo}>
                  <Text style={styles.bookTitle}>{book.name}</Text>
                  <Text style={styles.bookAuthor}>por {book.author}</Text>
                  <Text style={styles.bookGenre}>{getGenreText(book.genre)}</Text>
                  <View style={styles.bookMeta}>
                    <View style={[styles.statusBadge, { backgroundColor: getStatusColor(book.bookStatusId) }]}>
                      <Text style={styles.statusText}>{getStatusText(book.bookStatusId)}</Text>
                    </View>
                    <Text style={styles.ownerInfo}>de {book.ownerInfo?.name || 'Usuário'}</Text>
                  </View>
                </View>
              </TouchableOpacity>
            ))}
          </ScrollView>
        </View>
      </View>
    );
  };

  const renderNotificationsTab = () => (
    <View style={styles.tabContent}>
      <Text style={styles.tabTitle}>Notificações</Text>
      <Text style={styles.tabSubtitle}>Suas mensagens e alertas</Text>
      
      <ScrollView style={styles.notificationsList}>
        {/* Notificação de exemplo - Novo livro disponível */}
        <View style={styles.notificationCard}>
          <View style={styles.notificationHeader}>
            <Text style={styles.notificationIcon}>📚</Text>
            <Text style={styles.notificationTitle}>Novo livro disponível</Text>
            <Text style={styles.notificationTime}>2h atrás</Text>
          </View>
          <Text style={styles.notificationMessage}>
            "O Senhor dos Anéis" foi adicionado à biblioteca da organização "Clube de Leitura"
          </Text>
        </View>

        {/* Notificação de exemplo - Empréstimo próximo do vencimento */}
        <View style={styles.notificationCard}>
          <View style={styles.notificationHeader}>
            <Text style={styles.notificationIcon}>⏰</Text>
            <Text style={styles.notificationTitle}>Empréstimo vence em 2 dias</Text>
            <Text style={styles.notificationTime}>1 dia atrás</Text>
          </View>
          <Text style={styles.notificationMessage}>
            O livro "1984" deve ser devolvido até 15/01/2024
          </Text>
        </View>

        {/* Notificação de exemplo - Nova reunião */}
        <View style={styles.notificationCard}>
          <View style={styles.notificationHeader}>
            <Text style={styles.notificationIcon}>📅</Text>
            <Text style={styles.notificationTitle}>Nova reunião agendada</Text>
            <Text style={styles.notificationTime}>3 dias atrás</Text>
          </View>
          <Text style={styles.notificationMessage}>
            Reunião quinzenal do "Clube de Leitura" será na próxima terça-feira às 19h
          </Text>
        </View>

        {/* Notificação de exemplo - Novo membro */}
        <View style={styles.notificationCard}>
          <View style={styles.notificationHeader}>
            <Text style={styles.notificationIcon}>👤</Text>
            <Text style={styles.notificationTitle}>Novo membro na organização</Text>
            <Text style={styles.notificationTime}>1 semana atrás</Text>
          </View>
          <Text style={styles.notificationMessage}>
            Maria Silva se juntou à "Biblioteca Comunitária"
          </Text>
        </View>

        {/* Notificação de exemplo - Sistema */}
        <View style={styles.notificationCard}>
          <View style={styles.notificationHeader}>
            <Text style={styles.notificationIcon}>🔧</Text>
            <Text style={styles.notificationTitle}>Atualização do sistema</Text>
            <Text style={styles.notificationTime}>2 semanas atrás</Text>
          </View>
          <Text style={styles.notificationMessage}>
            Novas funcionalidades foram adicionadas ao sistema de biblioteca
          </Text>
        </View>
      </ScrollView>
    </View>
  );

  const renderOrganizationTab = () => (
    <View style={styles.tabContent}>
      <Text style={styles.tabTitle}>Minhas Organizações</Text>
      <Text style={styles.tabSubtitle}>Gerencie suas organizações</Text>

      {/* Botão para criar nova organização */}
      <TouchableOpacity
        style={styles.createOrgButton}
        onPress={handleCreateOrganization}
      >
        <Text style={styles.createOrgButtonText}>➕ Criar Nova Organização</Text>
      </TouchableOpacity>

      {/* Lista de organizações */}
      <ScrollView style={styles.organizationsList}>
        {sampleOrganizations.map((org) => (
          <TouchableOpacity
            key={org.id}
            style={styles.orgCard}
            onPress={() => handleViewOrganization(org)}
            activeOpacity={0.7}
          >
            <View style={styles.orgCardHeader}>
              <View style={styles.orgImageContainer}>
                {org.image ? (
                  <Image source={{ uri: org.image }} style={styles.orgImage} />
                ) : (
                  <Text style={styles.orgImagePlaceholder}>🏢</Text>
                )}
              </View>
              <View style={styles.orgCardInfo}>
                <Text style={styles.orgCardTitle}>{org.name}</Text>
                <Text style={styles.orgCardDescription}>{org.description}</Text>
                <View style={styles.orgCardMeta}>
                  <Text style={styles.orgCardRole}>{org.role}</Text>
                  <Text style={styles.orgCardMembers}>{org.memberCount} membros</Text>
                </View>
              </View>
            </View>
            
            <View style={styles.orgCardStats}>
              <View style={styles.orgStatItem}>
                <Text style={styles.orgStatNumber}>{org.stats.totalBooks}</Text>
                <Text style={styles.orgStatLabel}>Livros</Text>
              </View>
              <View style={styles.orgStatDivider} />
              <View style={styles.orgStatItem}>
                <Text style={styles.orgStatNumber}>{org.stats.activeLoans}</Text>
                <Text style={styles.orgStatLabel}>Empréstimos</Text>
              </View>
              <View style={styles.orgStatDivider} />
              <View style={styles.orgStatItem}>
                <Text style={styles.orgStatNumber}>{org.stats.monthlyLoans}</Text>
                <Text style={styles.orgStatLabel}>Este Mês</Text>
              </View>
            </View>
          </TouchableOpacity>
        ))}
      </ScrollView>
    </View>
  );

  const renderOrganizationDetail = () => {
    if (!selectedOrganization) return null;

    return (
      <Modal
        visible={showOrganizationDetail}
        transparent={true}
        animationType="slide"
        onRequestClose={handleCloseOrganizationDetail}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.modalContent}>
            {/* Header */}
            <View style={styles.modalHeader}>
              <Text style={styles.modalTitle}>{selectedOrganization.name}</Text>
              <TouchableOpacity onPress={handleCloseOrganizationDetail} style={styles.closeButton}>
                <Text style={styles.closeButtonText}>✕</Text>
              </TouchableOpacity>
            </View>

            <ScrollView style={styles.modalBody} showsVerticalScrollIndicator={false}>
              {/* Informações da Organização */}
              <View style={styles.orgDetailSection}>
                <View style={styles.orgDetailHeader}>
                  <View style={styles.orgDetailImageContainer}>
                    {selectedOrganization.image ? (
                      <Image source={{ uri: selectedOrganization.image }} style={styles.orgDetailImage} />
                    ) : (
                      <Text style={styles.orgDetailImagePlaceholder}>🏢</Text>
                    )}
                  </View>
                  <View style={styles.orgDetailInfo}>
                    <Text style={styles.orgDetailDescription}>{selectedOrganization.description}</Text>
                    <Text style={styles.orgDetailRole}>Seu papel: {selectedOrganization.role}</Text>
                    <Text style={styles.orgDetailMembers}>{selectedOrganization.memberCount} membros</Text>
                  </View>
                </View>
              </View>

              {/* Estatísticas */}
              <View style={styles.orgDetailSection}>
                <Text style={styles.sectionTitle}>📊 Estatísticas</Text>
                <View style={styles.orgDetailStats}>
                  <View style={styles.orgDetailStatItem}>
                    <Text style={styles.orgDetailStatNumber}>{selectedOrganization.stats.totalBooks}</Text>
                    <Text style={styles.orgDetailStatLabel}>Total de Livros</Text>
                  </View>
                  <View style={styles.orgDetailStatItem}>
                    <Text style={styles.orgDetailStatNumber}>{selectedOrganization.stats.activeLoans}</Text>
                    <Text style={styles.orgDetailStatLabel}>Empréstimos Ativos</Text>
                  </View>
                  <View style={styles.orgDetailStatItem}>
                    <Text style={styles.orgDetailStatNumber}>{selectedOrganization.stats.totalMembers}</Text>
                    <Text style={styles.orgDetailStatLabel}>Membros</Text>
                  </View>
                  <View style={styles.orgDetailStatItem}>
                    <Text style={styles.orgDetailStatNumber}>{selectedOrganization.stats.monthlyLoans}</Text>
                    <Text style={styles.orgDetailStatLabel}>Este Mês</Text>
                  </View>
                </View>
              </View>

              {/* Regulamento */}
              <View style={styles.orgDetailSection}>
                <Text style={styles.sectionTitle}>📋 Regulamento</Text>
                <View style={styles.regulationsList}>
                  {selectedOrganization.regulations.map((regulation: string, index: number) => (
                    <View key={index} style={styles.regulationItem}>
                      <Text style={styles.regulationNumber}>{index + 1}.</Text>
                      <Text style={styles.regulationText}>{regulation}</Text>
                    </View>
                  ))}
                </View>
              </View>
            </ScrollView>
          </View>
        </View>
      </Modal>
    );
  };

  const renderCreateOrganizationModal = () => {
    return (
      <Modal
        visible={showCreateOrganization}
        transparent={true}
        animationType="slide"
        onRequestClose={handleCloseCreateOrganization}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.modalContent}>
            {/* Header */}
            <View style={styles.modalHeader}>
              <Text style={styles.modalTitle}>Criar Nova Organização</Text>
              <TouchableOpacity onPress={handleCloseCreateOrganization} style={styles.closeButton}>
                <Text style={styles.closeButtonText}>✕</Text>
              </TouchableOpacity>
            </View>

            <ScrollView style={styles.modalBody} showsVerticalScrollIndicator={false}>
              {/* Imagem da Organização */}
              <View style={styles.inputContainer}>
                <Text style={styles.inputLabel}>Imagem da Organização</Text>
                <View style={styles.imageSection}>
                  <View style={styles.imagePreview}>
                    {createOrgForm.image ? (
                      <Image source={{ uri: createOrgForm.image }} style={styles.bookImage} />
                    ) : (
                      <Text style={styles.imagePlaceholder}>🏢</Text>
                    )}
                  </View>
                  <TouchableOpacity
                    style={styles.imagePickerButton}
                    onPress={showImagePickerOptions}
                  >
                    <Text style={styles.imagePickerText}>📷 Selecionar Imagem</Text>
                  </TouchableOpacity>
                </View>
              </View>

              {/* Nome da Organização */}
              <View style={styles.inputContainer}>
                <Text style={styles.inputLabel}>Nome da Organização *</Text>
                <TextInput 
                  style={styles.input} 
                  placeholder="Ex: Biblioteca Comunitária"
                  value={createOrgForm.name}
                  onChangeText={(text) => setCreateOrgForm({ ...createOrgForm, name: text })}
                />
              </View>

              {/* Descrição */}
              <View style={styles.inputContainer}>
                <Text style={styles.inputLabel}>Descrição *</Text>
                <TextInput 
                  style={[styles.input, styles.textArea]} 
                  placeholder="Descreva o propósito da organização..."
                  value={createOrgForm.description}
                  onChangeText={(text) => setCreateOrgForm({ ...createOrgForm, description: text })}
                  multiline={true}
                  numberOfLines={4}
                />
              </View>

              {/* Formulário de Regras */}
              <OrganizationRulesForm
                rules={createOrgForm.rules}
                onRulesChange={(rules) => setCreateOrgForm({ ...createOrgForm, rules })}
              />

              {/* Informações Adicionais */}
              <View style={styles.infoSection}>
                <Text style={styles.infoTitle}>ℹ️ Informações</Text>
                <Text style={styles.infoText}>
                  • Você será o administrador da organização{'\n'}
                  • Poderá convidar outros membros{'\n'}
                  • Definir regulamentos específicos{'\n'}
                  • Gerenciar livros compartilhados
                </Text>
              </View>
            </ScrollView>

            {/* Botões de Ação */}
            <View style={styles.modalActions}>
              <TouchableOpacity
                style={[styles.modalButton, styles.cancelButton]}
                onPress={handleCloseCreateOrganization}
              >
                <Text style={styles.cancelButtonText}>Cancelar</Text>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.modalButton, styles.saveButton]}
                onPress={handleSaveOrganization}
              >
                <Text style={styles.saveButtonText}>Criar Organização</Text>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>
    );
  };

  const renderProfileTab = () => (
    <ScrollView style={styles.profileScrollView} showsVerticalScrollIndicator={false}>
      <View style={styles.profileContent}>
        {/* Cabeçalho do Perfil */}
        <ProfileHeader user={user} onUpdateProfile={handleUpdateProfile} />

        {/* Seção de Organizações */}
        <OrganizationsSection
          organizations={sampleOrganizations}
          onViewOrganization={handleViewOrganization}
          onCreateOrganization={handleCreateOrganization}
        />

        {/* Seção de Livros */}
        <MyBooksSection
          books={myBooks}
          currentUserId={user.userId}
          onAddBook={() => setShowAddBookForm(true)}
          onEditBook={handleEditBook}
          onDeleteBook={handleDeleteBook}
          onViewBook={handleViewBook}
        />

        {/* Seção de Histórico de Empréstimos */}
        <LoanHistorySection
          loanHistory={sampleLoanHistory}
          onViewLoan={handleViewLoan}
        />

        {/* Botão de Logout */}
        <TouchableOpacity style={styles.logoutButton} onPress={onLogout}>
          <Text style={styles.logoutButtonText}>Sair</Text>
        </TouchableOpacity>
      </View>
    </ScrollView>
  );

  const [editFormData, setEditFormData] = useState({
    name: '',
    author: '',
    genre: 1,
    description: '',
    url: '',
  });
  const [editBookImage, setEditBookImage] = useState<string | null>(null);

  // Atualizar formData quando editingBook mudar
  React.useEffect(() => {
    if (editingBook) {
      setEditFormData({
        name: editingBook.name,
        author: editingBook.author,
        genre: editingBook.genre,
        description: editingBook.description,
        url: editingBook.url,
      });
      setEditBookImage(editingBook.image || null);
    }
  }, [editingBook]);

  const renderEditBookForm = () => {
    console.log('renderEditBookForm chamada - editingBook:', editingBook);
    if (!editingBook) {
      console.log('editingBook é null, retornando null');
      return null;
    }

          const handleSave = () => {
        // Atualizar o livro
        const updatedBook = { 
          ...editingBook, 
          ...editFormData,
          image: editBookImage || editingBook.image, // Manter imagem atual se não foi alterada
        };
        
        // Atualizar na lista de meus livros
        setMyBooks(prev => prev.map(book => 
          book.bookId === editingBook.bookId ? updatedBook : book
        ));
        
        // Atualizar na lista geral
        setAllBooks(prev => prev.map(book => 
          book.bookId === editingBook.bookId ? updatedBook : book
        ));

        Alert.alert('Sucesso', 'Livro atualizado com sucesso!');
        setShowEditBookForm(false);
        setEditingBook(null);
        setEditBookImage(null); // Limpar imagem temporária
      };

    return (
      <KeyboardAvoidingView
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        style={styles.formContainer}
      >
        <ScrollView style={styles.formScrollContent}>
          <View style={styles.formHeader}>
            <Text style={styles.formTitle}>Editar Livro</Text>
            <TouchableOpacity
              style={styles.closeButton}
              onPress={() => {
                setShowEditBookForm(false);
                setEditingBook(null);
              }}
            >
              <Text style={styles.closeButtonText}>✕</Text>
            </TouchableOpacity>
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>Nome do Livro</Text>
            <TextInput 
              style={styles.input} 
              placeholder="Digite o nome do livro"
              value={editFormData.name}
              onChangeText={(text) => setEditFormData({ ...editFormData, name: text })}
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>Autor</Text>
            <TextInput 
              style={styles.input} 
              placeholder="Digite o nome do autor"
              value={editFormData.author}
              onChangeText={(text) => setEditFormData({ ...editFormData, author: text })}
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>Gênero</Text>
            <TouchableOpacity
              style={styles.input}
              onPress={() => {
                Alert.alert(
                  'Selecionar Gênero',
                  'Escolha o gênero:',
                  GENRES.map(genre => ({
                    text: genre.name,
                    onPress: () => setEditFormData({ ...editFormData, genre: genre.id }),
                  }))
                );
              }}
            >
              <Text style={styles.genrePickerText}>
                {getGenreText(editFormData.genre)}
              </Text>
            </TouchableOpacity>
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>Descrição</Text>
            <TextInput
              style={[styles.input, styles.textArea]}
              placeholder="Digite uma descrição do livro"
              multiline
              numberOfLines={4}
              value={editFormData.description}
              onChangeText={(text) => setEditFormData({ ...editFormData, description: text })}
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>URL para compra (opcional)</Text>
            <TextInput 
              style={styles.input} 
              placeholder="https://..."
              value={editFormData.url}
              onChangeText={(text) => setEditFormData({ ...editFormData, url: text })}
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>Imagem do Livro</Text>
            <View style={styles.imageSection}>
              <View style={styles.imagePreview}>
                {editBookImage ? (
                  <Image source={{ uri: editBookImage }} style={styles.bookImage} />
                ) : editingBook.image ? (
                  <Image source={{ uri: editingBook.image }} style={styles.bookImage} />
                ) : (
                  <Text style={styles.imagePlaceholder}>📚</Text>
                )}
              </View>
              <TouchableOpacity
                style={styles.imagePickerButton}
                onPress={showImagePickerOptions}
              >
                <Text style={styles.imagePickerText}>📷 Alterar Imagem</Text>
              </TouchableOpacity>
            </View>
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>Visibilidade</Text>
            <TouchableOpacity
              style={styles.visibilityButton}
              onPress={() => {
                handleOpenVisibilityModal(editingBook);
              }}
            >
              <Text style={styles.visibilityButtonText}>👁️ Configurar Visibilidade</Text>
            </TouchableOpacity>
          </View>

                  <TouchableOpacity
          style={styles.saveButton}
          onPress={handleSave}
        >
          <Text style={styles.saveButtonText}>Salvar Alterações</Text>
        </TouchableOpacity>


        </ScrollView>
      </KeyboardAvoidingView>
    );
  };

  const renderAddBookForm = () => (
    <KeyboardAvoidingView
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      style={styles.formContainer}
    >
      <ScrollView style={styles.formScrollContent}>
        <View style={styles.formHeader}>
          <Text style={styles.formTitle}>Adicionar Livro</Text>
          <TouchableOpacity
            style={styles.closeButton}
            onPress={() => setShowAddBookForm(false)}
          >
            <Text style={styles.closeButtonText}>✕</Text>
          </TouchableOpacity>
        </View>

        <View style={styles.inputContainer}>
          <Text style={styles.inputLabel}>Nome do Livro</Text>
          <TextInput style={styles.input} placeholder="Digite o nome do livro" />
        </View>

        <View style={styles.inputContainer}>
          <Text style={styles.inputLabel}>Autor</Text>
          <TextInput style={styles.input} placeholder="Digite o nome do autor" />
        </View>

        <View style={styles.inputContainer}>
          <Text style={styles.inputLabel}>Gênero</Text>
          <TouchableOpacity
            style={styles.input}
            onPress={() => {
              Alert.alert(
                'Selecionar Gênero',
                'Escolha o gênero:',
                GENRES.map(genre => ({
                  text: genre.name,
                  onPress: () => console.log('Gênero selecionado:', genre.name),
                }))
              );
            }}
          >
            <Text style={styles.genrePickerText}>Selecione o gênero</Text>
          </TouchableOpacity>
        </View>

        <View style={styles.inputContainer}>
          <Text style={styles.inputLabel}>Descrição</Text>
          <TextInput
            style={[styles.input, styles.textArea]}
            placeholder="Digite uma descrição do livro"
            multiline
            numberOfLines={4}
          />
        </View>

        <View style={styles.inputContainer}>
          <Text style={styles.inputLabel}>URL para compra (opcional)</Text>
          <TextInput style={styles.input} placeholder="https://..." />
        </View>

        <View style={styles.inputContainer}>
          <Text style={styles.inputLabel}>Imagem (opcional)</Text>
          <TouchableOpacity
            style={styles.imagePickerButton}
            onPress={() => {
              Alert.alert('Imagem', 'Funcionalidade de upload será implementada');
            }}
          >
            <Text style={styles.imagePickerText}>Selecionar Imagem</Text>
          </TouchableOpacity>
        </View>

        <View style={styles.inputContainer}>
          <Text style={styles.inputLabel}>Visibilidade</Text>
          <TouchableOpacity
            style={styles.visibilityButton}
            onPress={() => {
              const newBook: Book = {
                bookId: Date.now(), // ID temporário
                name: 'Novo Livro',
                author: 'Autor',
                genre: 1,
                description: 'Descrição',
                url: '',
                image: '',
                bookStatusId: 1,
                ownerId: user.userId,
                createdAt: new Date().toISOString(),
              };
              handleOpenVisibilityModal(newBook);
            }}
          >
            <Text style={styles.visibilityButtonText}>👁️ Configurar Visibilidade</Text>
          </TouchableOpacity>
        </View>

        <TouchableOpacity
          style={styles.saveButton}
          onPress={() => {
            Alert.alert('Sucesso', 'Livro adicionado com sucesso!');
            setShowAddBookForm(false);
          }}
        >
          <Text style={styles.saveButtonText}>Salvar Livro</Text>
        </TouchableOpacity>


      </ScrollView>
    </KeyboardAvoidingView>
  );

  if (showEditBookForm) {
    console.log('Renderizando formulário de edição - showEditBookForm:', showEditBookForm, 'editingBook:', editingBook);
    return (
      <>
        {renderEditBookForm()}
        
        {/* Modal de Visibilidade para edição */}
        {selectedBookForVisibility && (
          <BookVisibilityModal
            book={selectedBookForVisibility}
            organizations={sampleOrganizationsForVisibility}
            isVisible={showVisibilityModal}
            onClose={() => {
              console.log('Fechando modal de visibilidade');
              setShowVisibilityModal(false);
              setSelectedBookForVisibility(null);
            }}
            onSave={handleSaveVisibilitySettings}
          />
        )}
      </>
    );
  }

  if (showAddBookForm) {
    return (
      <>
        {renderAddBookForm()}
        
        {/* Modal de Visibilidade para formulário */}
        {selectedBookForVisibility && (
          <BookVisibilityModal
            book={selectedBookForVisibility}
            organizations={sampleOrganizationsForVisibility}
            isVisible={showVisibilityModal}
            onClose={() => {
              console.log('Fechando modal de visibilidade');
              setShowVisibilityModal(false);
              setSelectedBookForVisibility(null);
            }}
            onSave={handleSaveVisibilitySettings}
          />
        )}
      </>
    );
  }

  if (showCreateOrganization) {
    return (
      <>
        {renderCreateOrganizationModal()}
      </>
    );
  }

  if (showOrganizationDetail && selectedOrganization) {
    return (
      <>
        {renderOrganizationDetail()}
      </>
    );
  }

  if (showUserProfile && selectedUser) {
    console.log('Renderizando UserProfileScreen');
    console.log('showUserProfile:', showUserProfile);
    console.log('selectedUser:', selectedUser);
    
    // Filtrar livros do usuário selecionado
    const userBooks = sampleBooks.filter(book => book.ownerId === selectedUser.userId);
    console.log('userBooks encontrados:', userBooks.length);
    
    // Encontrar dados completos do usuário
    const fullUserData = sampleUsers.find(u => u.userId === selectedUser.userId) || selectedUser;
    console.log('fullUserData:', fullUserData);
    
    return (
      <>
        <UserProfileScreen
          user={fullUserData}
          userBooks={userBooks}
          currentUserId={user.userId}
          isVisible={showUserProfile}
          onClose={() => {
            console.log('Fechando UserProfileScreen');
            setShowUserProfile(false);
            setSelectedUser(null);
          }}
          onRequestLoan={handleRequestLoan}
          onContactUser={handleContactUser}
          organizationSettings={getOrganizationSettings()}
        />
        
        {/* Modal de Visibilidade para perfil */}
        {selectedBookForVisibility && (
          <BookVisibilityModal
            book={selectedBookForVisibility}
            organizations={sampleOrganizationsForVisibility}
            isVisible={showVisibilityModal}
            onClose={() => {
              console.log('Fechando modal de visibilidade');
              setShowVisibilityModal(false);
              setSelectedBookForVisibility(null);
            }}
            onSave={handleSaveVisibilitySettings}
          />
        )}
      </>
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
            Alert.alert('Solicitar Empréstimo', 'Funcionalidade será implementada');
            setShowBookDetail(false);
          }}
          onReturnBook={(bookId) => {
            Alert.alert('Marcar como Lido', 'Funcionalidade será implementada');
            setShowBookDetail(false);
          }}
          onViewUserProfile={handleViewUserProfile}
          onOpenVisibility={handleOpenVisibilityModal}
        />
        
        {/* Modal de Visibilidade */}
        {selectedBookForVisibility && (
          <BookVisibilityModal
            book={selectedBookForVisibility}
            organizations={sampleOrganizationsForVisibility}
            isVisible={showVisibilityModal}
            onClose={() => {
              console.log('Fechando modal de visibilidade');
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
      <SafeAreaView style={styles.container}>
        <View style={styles.contentArea}>
          {activeTab === 'books' && renderBooksTab()}
          {activeTab === 'notifications' && renderNotificationsTab()}
          {activeTab === 'organization' && renderOrganizationTab()}
          {activeTab === 'profile' && renderProfileTab()}
        </View>

        <View style={styles.tabBar}>
          <TouchableOpacity
            style={[styles.tabButton, activeTab === 'books' && styles.activeTabButton]}
            onPress={() => setActiveTab('books')}
          >
            <Text style={[styles.tabButtonText, activeTab === 'books' && styles.activeTabButtonText]}>
              📚 Livros
            </Text>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.tabButton, activeTab === 'notifications' && styles.activeTabButton]}
            onPress={() => setActiveTab('notifications')}
          >
            <Text style={[styles.tabButtonText, activeTab === 'notifications' && styles.activeTabButtonText]}>
              🔔 Notificações
            </Text>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.tabButton, activeTab === 'organization' && styles.activeTabButton]}
            onPress={() => setActiveTab('organization')}
          >
            <Text style={[styles.tabButtonText, activeTab === 'organization' && styles.activeTabButtonText]}>
              🏢 Org
            </Text>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.tabButton, activeTab === 'profile' && styles.activeTabButton]}
            onPress={() => setActiveTab('profile')}
          >
            <Text style={[styles.tabButtonText, activeTab === 'profile' && styles.activeTabButtonText]}>
              👤 Perfil
            </Text>
          </TouchableOpacity>
        </View>
      </SafeAreaView>
    </>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: COLORS.background,
  },
  contentArea: {
    flex: 1,
    paddingBottom: 80,
  },
  tabContent: {
    flex: 1,
    padding: SPACING.lg,
  },
  tabTitle: {
    fontSize: FONT_SIZES.xxl,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.text.primary,
    marginBottom: SPACING.sm,
  },
  tabSubtitle: {
    fontSize: FONT_SIZES.md,
    color: COLORS.text.secondary,
    marginBottom: SPACING.lg,
  },
  booksList: {
    flex: 1,
  },
  bookCard: {
    backgroundColor: COLORS.white,
    borderRadius: BORDER_RADIUS.lg,
    padding: SPACING.md,
    marginBottom: SPACING.md,
    flexDirection: 'row',
    ...SHADOWS.small,
  },
  bookImageContainer: {
    width: 60,
    height: 80,
    backgroundColor: COLORS.gray[200],
    borderRadius: BORDER_RADIUS.md,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: SPACING.md,
  },
  bookImagePlaceholder: {
    fontSize: FONT_SIZES.xl,
  },
  bookInfo: {
    flex: 1,
  },
  bookTitle: {
    fontSize: FONT_SIZES.lg,
    fontWeight: FONT_WEIGHTS.semibold,
    color: COLORS.text.primary,
    marginBottom: SPACING.xs,
  },
  bookAuthor: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    marginBottom: SPACING.xs,
  },
  bookGenre: {
    fontSize: FONT_SIZES.xs,
    color: COLORS.primary,
    marginBottom: SPACING.sm,
  },
  statusBadge: {
    alignSelf: 'flex-start',
    paddingHorizontal: SPACING.sm,
    paddingVertical: SPACING.xs,
    borderRadius: BORDER_RADIUS.sm,
  },
  statusText: {
    color: COLORS.white,
    fontSize: FONT_SIZES.xs,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  regulationContent: {
    flex: 1,
  },

  orgInfo: {
    backgroundColor: COLORS.white,
    borderRadius: BORDER_RADIUS.lg,
    padding: SPACING.lg,
    marginTop: SPACING.lg,
    ...SHADOWS.small,
  },
  orgTitle: {
    fontSize: FONT_SIZES.xl,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.text.primary,
    marginBottom: SPACING.sm,
  },
  orgDescription: {
    fontSize: FONT_SIZES.md,
    color: COLORS.text.secondary,
    marginBottom: SPACING.lg,
    lineHeight: 24,
  },
  orgStats: {
    flexDirection: 'row',
    justifyContent: 'space-around',
  },
  statItem: {
    alignItems: 'center',
  },
  statNumber: {
    fontSize: FONT_SIZES.xxl,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.primary,
  },
  statLabel: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    marginTop: SPACING.xs,
  },
  profileScrollView: {
    flex: 1,
  },
  profileContent: {
    padding: SPACING.lg,
    paddingBottom: SPACING.xl,
  },
  logoutButton: {
    backgroundColor: COLORS.error,
    padding: SPACING.md,
    borderRadius: BORDER_RADIUS.md,
    alignItems: 'center',
    marginTop: SPACING.lg,
  },
  logoutButtonText: {
    color: COLORS.white,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  tabBar: {
    flexDirection: 'row',
    backgroundColor: COLORS.white,
    borderTopWidth: 1,
    borderTopColor: COLORS.gray[200],
    position: 'absolute',
    bottom: 0,
    left: 0,
    right: 0,
    paddingBottom: 20,
    paddingTop: 10,
    ...SHADOWS.medium,
  },
  tabButton: {
    flex: 1,
    alignItems: 'center',
    paddingVertical: SPACING.sm,
  },
  activeTabButton: {
    backgroundColor: COLORS.gray[50],
  },
  tabButtonText: {
    fontSize: FONT_SIZES.xs,
    color: COLORS.text.secondary,
    textAlign: 'center',
  },
  activeTabButtonText: {
    color: COLORS.primary,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  formContainer: {
    flex: 1,
    backgroundColor: COLORS.background,
  },
  formScrollContent: {
    flex: 1,
    padding: SPACING.lg,
    paddingBottom: 100,
  },
  formHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: SPACING.xl,
  },
  formTitle: {
    fontSize: FONT_SIZES.xxl,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.text.primary,
  },

  inputContainer: {
    marginBottom: SPACING.lg,
  },
  inputLabel: {
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
    color: COLORS.text.primary,
    marginBottom: SPACING.sm,
  },
  input: {
    borderWidth: 1,
    borderColor: COLORS.gray[300],
    borderRadius: BORDER_RADIUS.md,
    padding: SPACING.md,
    fontSize: FONT_SIZES.md,
    backgroundColor: COLORS.white,
  },
  genrePickerText: {
    color: COLORS.text.secondary,
  },
  imagePickerButton: {
    borderWidth: 1,
    borderColor: COLORS.gray[300],
    borderRadius: BORDER_RADIUS.md,
    padding: SPACING.md,
    backgroundColor: COLORS.white,
    alignItems: 'center',
  },
  imagePickerText: {
    color: COLORS.primary,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },

  visibilityButton: {
    borderWidth: 1,
    borderColor: COLORS.info,
    borderRadius: BORDER_RADIUS.md,
    padding: SPACING.md,
    backgroundColor: COLORS.info + '20',
    alignItems: 'center',
  },
  visibilityButtonText: {
    color: COLORS.info,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  imageSection: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: SPACING.md,
  },
  imagePreview: {
    width: 80,
    height: 100,
    backgroundColor: COLORS.gray[200],
    borderRadius: BORDER_RADIUS.md,
    justifyContent: 'center',
    alignItems: 'center',
    overflow: 'hidden',
  },
  bookImage: {
    width: '100%',
    height: '100%',
    borderRadius: BORDER_RADIUS.md,
  },
  imagePlaceholder: {
    fontSize: 32,
    color: COLORS.gray[400],
  },
  // Estilos para organizações
  createOrgButton: {
    backgroundColor: COLORS.primary,
    padding: SPACING.md,
    borderRadius: BORDER_RADIUS.md,
    alignItems: 'center',
    marginBottom: SPACING.lg,
  },
  createOrgButtonText: {
    color: COLORS.white,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  organizationsList: {
    flex: 1,
  },
  orgCard: {
    backgroundColor: COLORS.white,
    borderRadius: BORDER_RADIUS.lg,
    padding: SPACING.md,
    marginBottom: SPACING.md,
    ...SHADOWS.small,
  },
  orgCardHeader: {
    flexDirection: 'row',
    marginBottom: SPACING.md,
  },
  orgImageContainer: {
    width: 60,
    height: 60,
    backgroundColor: COLORS.gray[200],
    borderRadius: BORDER_RADIUS.md,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: SPACING.md,
  },
  orgImage: {
    width: '100%',
    height: '100%',
    borderRadius: BORDER_RADIUS.md,
  },
  orgImagePlaceholder: {
    fontSize: 24,
    color: COLORS.gray[400],
  },
  orgCardInfo: {
    flex: 1,
  },
  orgCardTitle: {
    fontSize: FONT_SIZES.lg,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.text.primary,
    marginBottom: SPACING.xs,
  },
  orgCardDescription: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    marginBottom: SPACING.sm,
  },
  orgCardMeta: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  orgCardRole: {
    fontSize: FONT_SIZES.xs,
    color: COLORS.primary,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  orgCardMembers: {
    fontSize: FONT_SIZES.xs,
    color: COLORS.text.secondary,
  },
  orgCardStats: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingTop: SPACING.sm,
    borderTopWidth: 1,
    borderTopColor: COLORS.gray[200],
  },
  orgStatItem: {
    alignItems: 'center',
  },
  orgStatNumber: {
    fontSize: FONT_SIZES.lg,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.primary,
  },
  orgStatLabel: {
    fontSize: FONT_SIZES.xs,
    color: COLORS.text.secondary,
    marginTop: SPACING.xs,
  },
  orgStatDivider: {
    width: 1,
    height: '100%',
    backgroundColor: COLORS.gray[300],
  },
  // Estilos para modal de detalhes da organização
  orgDetailSection: {
    marginBottom: SPACING.lg,
  },
  orgDetailHeader: {
    flexDirection: 'row',
    marginBottom: SPACING.md,
  },
  orgDetailImageContainer: {
    width: 80,
    height: 80,
    backgroundColor: COLORS.gray[200],
    borderRadius: BORDER_RADIUS.md,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: SPACING.md,
  },
  orgDetailImage: {
    width: '100%',
    height: '100%',
    borderRadius: BORDER_RADIUS.md,
  },
  orgDetailImagePlaceholder: {
    fontSize: 32,
    color: COLORS.gray[400],
  },
  orgDetailInfo: {
    flex: 1,
  },
  orgDetailDescription: {
    fontSize: FONT_SIZES.md,
    color: COLORS.text.secondary,
    marginBottom: SPACING.sm,
  },
  orgDetailRole: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.primary,
    fontWeight: FONT_WEIGHTS.semibold,
    marginBottom: SPACING.xs,
  },
  orgDetailMembers: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
  },
  orgDetailStats: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: SPACING.md,
  },
  orgDetailStatItem: {
    flex: 1,
    minWidth: '45%',
    alignItems: 'center',
    padding: SPACING.md,
    backgroundColor: COLORS.gray[50],
    borderRadius: BORDER_RADIUS.md,
  },
  orgDetailStatNumber: {
    fontSize: FONT_SIZES.xl,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.primary,
  },
  orgDetailStatLabel: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    marginTop: SPACING.xs,
    textAlign: 'center',
  },
  regulationsList: {
    gap: SPACING.sm,
  },
  regulationItem: {
    flexDirection: 'row',
    padding: SPACING.sm,
    backgroundColor: COLORS.gray[50],
    borderRadius: BORDER_RADIUS.sm,
  },
  regulationNumber: {
    fontSize: FONT_SIZES.sm,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.primary,
    marginRight: SPACING.sm,
    minWidth: 20,
  },
  regulationText: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.primary,
    flex: 1,
  },
  // Estilos para modais
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContent: {
    backgroundColor: COLORS.white,
    borderRadius: BORDER_RADIUS.lg,
    width: '90%',
    maxHeight: '80%',
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 8,
    },
    shadowOpacity: 0.2,
    shadowRadius: 16,
    elevation: 8,
  },
  modalHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: SPACING.lg,
    borderBottomWidth: 1,
    borderBottomColor: COLORS.gray[200],
  },
  modalTitle: {
    fontSize: FONT_SIZES.xl,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.text.primary,
    flex: 1,
  },
  closeButton: {
    padding: SPACING.sm,
  },
  closeButtonText: {
    fontSize: FONT_SIZES.xl,
    color: COLORS.text.secondary,
  },
  modalBody: {
    padding: SPACING.lg,
  },
  sectionTitle: {
    fontSize: FONT_SIZES.lg,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.text.primary,
    marginBottom: SPACING.md,
  },
  // Estilos para modal de criar organização
  infoSection: {
    backgroundColor: COLORS.gray[50],
    padding: SPACING.md,
    borderRadius: BORDER_RADIUS.md,
    marginTop: SPACING.lg,
  },
  infoTitle: {
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
    color: COLORS.text.primary,
    marginBottom: SPACING.sm,
  },
  infoText: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    lineHeight: 20,
  },
  modalActions: {
    flexDirection: 'row',
    padding: SPACING.lg,
    borderTopWidth: 1,
    borderTopColor: COLORS.gray[200],
    gap: SPACING.md,
  },
  modalButton: {
    flex: 1,
    padding: SPACING.md,
    borderRadius: BORDER_RADIUS.md,
    alignItems: 'center',
  },
  cancelButton: {
    backgroundColor: COLORS.gray[300],
  },
  cancelButtonText: {
    color: COLORS.text.primary,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  saveButton: {
    backgroundColor: COLORS.primary,
  },
  saveButtonText: {
    color: COLORS.white,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  textArea: {
    height: 100,
    textAlignVertical: 'top',
  },
  // Estilos para a seção de livros
  booksSection: {
    flex: 1,
    marginTop: SPACING.md,
  },
  booksSectionTitle: {
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
    color: COLORS.text.primary,
    marginBottom: SPACING.sm,
  },
  bookMeta: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: SPACING.xs,
  },
  ownerInfo: {
    fontSize: FONT_SIZES.xs,
    color: COLORS.text.secondary,
    fontStyle: 'italic',
  },
  // Estilos para notificações
  notificationsList: {
    flex: 1,
  },
  notificationCard: {
    backgroundColor: COLORS.white,
    borderRadius: BORDER_RADIUS.lg,
    padding: SPACING.md,
    marginBottom: SPACING.md,
    ...SHADOWS.small,
  },
  notificationHeader: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: SPACING.sm,
  },
  notificationIcon: {
    fontSize: FONT_SIZES.lg,
    marginRight: SPACING.sm,
  },
  notificationTitle: {
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
    color: COLORS.text.primary,
    flex: 1,
  },
  notificationTime: {
    fontSize: FONT_SIZES.xs,
    color: COLORS.text.secondary,
  },
  notificationMessage: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    lineHeight: 20,
  },
}); 