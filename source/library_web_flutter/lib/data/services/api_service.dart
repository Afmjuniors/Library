import 'package:dio/dio.dart';
// import 'package:retrofit/retrofit.dart';
import '../models/auth_model.dart';
import '../models/book_model.dart';
import '../models/user_model.dart';
import '../models/organization_model.dart';
import '../mock_data.dart';
import '../../core/constants/app_constants.dart';

// part 'api_service.g.dart';

// @RestApi(baseUrl: AppConstants.baseUrl)
abstract class ApiService {
  // factory ApiService(Dio dio, {String baseUrl}) = _ApiService;
  
  // Mock implementation for now
  factory ApiService(Dio dio) = _MockApiService;

  // Auth endpoints
  // @POST('/auth/login')
  Future<LoginResultModel> login(LoginRequestModel request);

  // @POST('/auth/signup')
  Future<LoginResultModel> signup(SignupRequestModel request);

  // @POST('/auth/logout')
  Future<void> logout();

  // User endpoints
  // @GET('/users/profile')
  Future<UserModel> getProfile();

  // @PUT('/users/profile')
  Future<UserModel> updateProfile(UserModel user);

  // Books endpoints
  // @GET('/books')
  Future<List<BookModel>> getBooks({
    String? query,
    int? genre,
    int? status,
    int? organization,
    int? page,
    int? limit,
  });

  // @GET('/books/{id}')
  Future<BookModel> getBook(int id);

  // @POST('/books')
  Future<BookModel> createBook(BookModel book);

  // @PUT('/books/{id}')
  Future<BookModel> updateBook(int id, BookModel book);

  // @DELETE('/books/{id}')
  Future<void> deleteBook(int id);

  // Organizations endpoints
  // @GET('/organizations')
  Future<List<ExtendedOrganizationModel>> getOrganizations();

  // @GET('/organizations/{id}')
  Future<ExtendedOrganizationModel> getOrganization(int id);

  // @POST('/organizations')
  Future<ExtendedOrganizationModel> createOrganization(ExtendedOrganizationModel organization);

  // @PUT('/organizations/{id}')
  Future<ExtendedOrganizationModel> updateOrganization(int id, ExtendedOrganizationModel organization);

  // Loans endpoints
  // @POST('/loans/request')
  Future<void> requestLoan(Map<String, dynamic> request);

  // @POST('/loans/{id}/return')
  Future<void> returnBook(int id);

  // @GET('/loans/history')
  Future<List<Map<String, dynamic>>> getLoanHistory();
}

// Real implementation using mock data
class _MockApiService implements ApiService {
  _MockApiService(Dio dio);

  @override
  Future<LoginResultModel> login(LoginRequestModel request) async {
    // Simulate API delay
    await Future.delayed(const Duration(seconds: 1));
    
    // Validate credentials (simple validation for demo)
    if (request.email.isEmpty || request.password.isEmpty) {
      return LoginResultModel(
        success: false,
        message: 'Email e senha são obrigatórios',
        data: null,
      );
    }

    // Return success with mock user data
    return LoginResultModel(
      success: true,
      message: 'Login realizado com sucesso',
      data: AuthDataModel(
        token: 'mock_token_${DateTime.now().millisecondsSinceEpoch}',
        user: AuthenticatedUserModel(
          userId: 1,
          name: 'João Silva',
          email: request.email,
          phone: '(11) 99999-9999',
          address: 'São Paulo, SP',
          image: 'https://picsum.photos/200',
          createdAt: '2024-01-01',
        ),
      ),
    );
  }

  @override
  Future<LoginResultModel> signup(SignupRequestModel request) async {
    // Simulate API delay
    await Future.delayed(const Duration(seconds: 1));
    
    // Validate required fields
    if (request.name.isEmpty || request.email.isEmpty || request.password.isEmpty) {
      return LoginResultModel(
        success: false,
        message: 'Nome, email e senha são obrigatórios',
        data: null,
      );
    }

    // Return success with new user data
    return LoginResultModel(
      success: true,
      message: 'Cadastro realizado com sucesso',
      data: AuthDataModel(
        token: 'mock_token_${DateTime.now().millisecondsSinceEpoch}',
        user: AuthenticatedUserModel(
          userId: 2,
          name: request.name,
          email: request.email,
          phone: request.phone ?? '',
          address: request.address ?? '',
          image: 'https://picsum.photos/200',
          createdAt: DateTime.now().toIso8601String(),
        ),
      ),
    );
  }

  @override
  Future<void> logout() async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
  }

  @override
  Future<UserModel> getProfile() async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    return UserModel(
      userId: 1,
      name: 'João Silva',
      email: 'joao@email.com',
      phone: '(11) 99999-9999',
      address: 'São Paulo, SP',
      image: 'https://picsum.photos/200',
      createdAt: '2024-01-01',
    );
  }

  @override
  Future<UserModel> updateProfile(UserModel user) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    return user;
  }

  @override
  Future<List<BookModel>> getBooks({
    String? query,
    int? genre,
    int? status,
    int? organization,
    int? page,
    int? limit,
  }) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    
    // Import mock data
    import '../mock_data.dart';
    
    // Filter books based on parameters
    List<BookModel> books = MockData.books;
    
    // Filter by query
    if (query != null && query.isNotEmpty) {
      books = books.where((book) {
        final searchQuery = query.toLowerCase();
        return book.name.toLowerCase().contains(searchQuery) ||
               book.author.toLowerCase().contains(searchQuery);
      }).toList();
    }
    
    // Filter by genre
    if (genre != null) {
      books = books.where((book) => book.genre == genre).toList();
    }
    
    // Filter by status
    if (status != null) {
      books = books.where((book) => book.bookStatusId == status).toList();
    }
    
    return books;
  }

  @override
  Future<BookModel> getBook(int id) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    
    // Import mock data
    import '../mock_data.dart';
    
    // Find book by ID
    final book = MockData.books.firstWhere(
      (book) => book.bookId == id,
      orElse: () => throw Exception('Book not found'),
    );
    
    return book;
  }

  @override
  Future<BookModel> createBook(BookModel book) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    return book;
  }

  @override
  Future<BookModel> updateBook(int id, BookModel book) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    return book;
  }

  @override
  Future<void> deleteBook(int id) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
  }

  @override
  Future<List<ExtendedOrganizationModel>> getOrganizations() async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    
    // Import mock data
    import '../mock_data.dart';
    
    return MockData.organizations;
  }

  @override
  Future<ExtendedOrganizationModel> getOrganization(int id) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    
    // Import mock data
    import '../mock_data.dart';
    
    // Find organization by ID
    final organization = MockData.organizations.firstWhere(
      (org) => org.id == id,
      orElse: () => throw Exception('Organization not found'),
    );
    
    return organization;
  }

  @override
  Future<ExtendedOrganizationModel> createOrganization(ExtendedOrganizationModel organization) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    return organization;
  }

  @override
  Future<ExtendedOrganizationModel> updateOrganization(int id, ExtendedOrganizationModel organization) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    return organization;
  }

  @override
  Future<void> requestLoan(Map<String, dynamic> request) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
  }

  @override
  Future<void> returnBook(int id) async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
  }

  @override
  Future<List<Map<String, dynamic>>> getLoanHistory() async {
    // Simulate API delay
    await Future.delayed(const Duration(milliseconds: 500));
    return [];
  }
} 