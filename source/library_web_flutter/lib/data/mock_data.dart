import 'models/user_model.dart';
import 'models/book_model.dart';
import 'models/organization_model.dart';

class MockData {
  static final List<UserModel> users = [
    UserModel(
      userId: 1,
      name: 'João Silva',
      email: 'joao@email.com',
      phone: '(11) 99999-9999',
      address: 'São Paulo, SP',
      image: 'https://picsum.photos/200',
      createdAt: '2024-01-01',
    ),
    UserModel(
      userId: 2,
      name: 'Maria Santos',
      email: 'maria@email.com',
      phone: '(11) 88888-8888',
      address: 'Rio de Janeiro, RJ',
      image: 'https://picsum.photos/201',
      createdAt: '2024-01-02',
    ),
  ];

  static final List<BookModel> books = [
    BookModel(
      bookId: 1,
      name: 'O Senhor dos Anéis',
      author: 'J.R.R. Tolkien',
      genre: 5, // Fantasy
      description: 'Uma épica aventura de fantasia que segue a jornada de Frodo Baggins para destruir o Um Anel.',
      url: 'https://www.amazon.com.br/senhor-dos-anéis',
      image: 'https://picsum.photos/200/300',
      bookStatusId: 1, // Available
      ownerId: 1,
      createdAt: '2024-01-01',
      ownerInfo: BookOwnerInfo(
        name: 'João Silva',
        email: 'joao@email.com',
        totalBooks: 15,
        booksLent: 3,
        booksAvailable: 12,
      ),
    ),
    BookModel(
      bookId: 2,
      name: '1984',
      author: 'George Orwell',
      genre: 10, // Thriller
      description: 'Um romance distópico que retrata uma sociedade totalitária.',
      url: 'https://www.amazon.com.br/1984',
      image: 'https://picsum.photos/201/300',
      bookStatusId: 2, // Borrowed
      ownerId: 2,
      createdAt: '2024-01-02',
      ownerInfo: BookOwnerInfo(
        name: 'Maria Santos',
        email: 'maria@email.com',
        totalBooks: 8,
        booksLent: 1,
        booksAvailable: 7,
      ),
    ),
    BookModel(
      bookId: 3,
      name: 'Dom Casmurro',
      author: 'Machado de Assis',
      genre: 4, // Drama
      description: 'Um clássico da literatura brasileira sobre ciúme e traição.',
      url: 'https://www.amazon.com.br/dom-casmurro',
      image: 'https://picsum.photos/202/300',
      bookStatusId: 1, // Available
      ownerId: 1,
      createdAt: '2024-01-03',
      ownerInfo: BookOwnerInfo(
        name: 'João Silva',
        email: 'joao@email.com',
        totalBooks: 15,
        booksLent: 3,
        booksAvailable: 12,
      ),
    ),
  ];

  static final List<ExtendedOrganizationModel> organizations = [
    ExtendedOrganizationModel(
      id: 1,
      name: 'Biblioteca Comunitária',
      description: 'Uma biblioteca colaborativa onde membros compartilham seus livros pessoais.',
      memberCount: 15,
      role: 'Admin',
      isActive: true,
      image: 'https://picsum.photos/200',
      createdAt: '2024-01-01',
      rules: OrganizationRules(
        loanDurationDays: 30,
        meetingFrequency: 'weekly',
        meetingDay: 'saturday',
        meetingTime: '14:00',
        requireCompleteUserInfo: false,
      ),
      regulations: [
        'Cada membro pode emprestar até 3 livros simultaneamente',
        'O prazo de empréstimo é de 30 dias',
        'Renovação pode ser solicitada até 3 dias antes do vencimento',
        'Livros atrasados geram multa de R\$ 1,00 por dia',
        'Livros danificados devem ser repostos pelo usuário',
        'Respeite o próximo leitor - mantenha os livros em bom estado',
        'Para dúvidas, entre em contato com a administração'
      ],
      stats: OrganizationStats(
        totalBooks: 45,
        activeLoans: 12,
        totalMembers: 15,
        monthlyLoans: 28,
      ),
    ),
    ExtendedOrganizationModel(
      id: 2,
      name: 'Clube de Leitura',
      description: 'Grupo de discussão sobre livros e literatura.',
      memberCount: 8,
      role: 'Member',
      isActive: true,
      image: 'https://picsum.photos/201',
      createdAt: '2024-01-02',
      rules: OrganizationRules(
        loanDurationDays: 21,
        meetingFrequency: 'weekly',
        meetingDay: 'tuesday',
        meetingTime: '19:00',
        requireCompleteUserInfo: false,
      ),
      regulations: [
        'Reuniões semanais para discussão de livros',
        'Cada membro deve ler pelo menos um livro por mês',
        'Respeite as opiniões dos outros membros',
        'Participe ativamente das discussões',
        'Sugira livros para o próximo mês'
      ],
      stats: OrganizationStats(
        totalBooks: 23,
        activeLoans: 5,
        totalMembers: 8,
        monthlyLoans: 15,
      ),
    ),
  ];
} 