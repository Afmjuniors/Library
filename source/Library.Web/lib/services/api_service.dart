import 'dart:convert';
import 'package:dio/dio.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../constants/app_constants.dart';
import '../models/auth.dart';
import '../models/book.dart';
import '../models/user.dart';
import '../models/search_params.dart';

class ApiService {
  static final ApiService _instance = ApiService._internal();
  factory ApiService() => _instance;
  ApiService._internal();

  late Dio _dio;
  String? _token;

  void initialize() {
    _dio = Dio(BaseOptions(
      baseUrl: AppConstants.baseUrl,
      connectTimeout: const Duration(seconds: 30),
      receiveTimeout: const Duration(seconds: 30),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
    ));

    _dio.interceptors.add(InterceptorsWrapper(
      onRequest: (options, handler) async {
        if (_token != null) {
          options.headers['Authorization'] = 'Bearer $_token';
        }
        handler.next(options);
      },
      onError: (error, handler) {
        if (error.response?.statusCode == 401) {
          // Handle unauthorized access
          _clearToken();
        }
        handler.next(error);
      },
    ));

    _loadToken();
  }

  Future<void> _loadToken() async {
    final prefs = await SharedPreferences.getInstance();
    _token = prefs.getString(AppConstants.tokenKey);
  }

  Future<void> _saveToken(String token) async {
    _token = token;
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString(AppConstants.tokenKey, token);
  }

  Future<void> _clearToken() async {
    _token = null;
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove(AppConstants.tokenKey);
  }

  // Authentication Methods
  Future<AuthResponse> login(String email, String password) async {
    try {
      final response = await _dio.post(
        AppConstants.loginEndpoint,
        data: UserAuth(username: email, password: password).toJson(),
      );

      final authResponse = AuthResponse.fromJson(response.data);
      if (authResponse.success && authResponse.token != null) {
        await _saveToken(authResponse.token!);
      }
      return authResponse;
    } on DioException catch (e) {
      return _handleAuthError(e);
    }
  }

  Future<AuthResponse> signup(SignUpRequest request) async {
    try {
      final response = await _dio.post(
        AppConstants.signupEndpoint,
        data: request.toJson(),
      );

      final authResponse = AuthResponse.fromJson(response.data);
      if (authResponse.success && authResponse.token != null) {
        await _saveToken(authResponse.token!);
      }
      return authResponse;
    } on DioException catch (e) {
      return _handleAuthError(e);
    }
  }

  Future<void> logout() async {
    await _clearToken();
  }

  AuthResponse _handleAuthError(DioException e) {
    if (e.response?.statusCode == 401) {
      return AuthResponse(
        success: false,
        message: AppConstants.unauthorizedError,
      );
    } else if (e.type == DioExceptionType.connectionTimeout ||
               e.type == DioExceptionType.receiveTimeout) {
      return AuthResponse(
        success: false,
        message: AppConstants.networkError,
      );
    } else {
      return AuthResponse(
        success: false,
        message: e.response?.data?['message'] ?? AppConstants.unknownError,
      );
    }
  }

  // Books Methods
  Future<List<Book>> getBooks({SearchBookParams? params, int page = 1, int pageSize = 20}) async {
    try {
      final queryParams = <String, dynamic>{
        'page': page,
        'pageSize': pageSize,
      };

      if (params != null) {
        queryParams.addAll(params.toJson());
      }

      final response = await _dio.get(
        AppConstants.booksEndpoint,
        queryParameters: queryParams,
      );

      final List<dynamic> booksData = response.data['data'] ?? response.data;
      return booksData.map((json) => Book.fromJson(json)).toList();
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<Book> getBook(int bookId) async {
    try {
      final response = await _dio.get('${AppConstants.booksEndpoint}/$bookId');
      return Book.fromJson(response.data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<Book> createBook(Book book) async {
    try {
      final response = await _dio.post(
        AppConstants.booksEndpoint,
        data: book.toJson(),
      );
      return Book.fromJson(response.data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<Book> updateBook(int bookId, Book book) async {
    try {
      final response = await _dio.put(
        '${AppConstants.booksEndpoint}/$bookId',
        data: book.toJson(),
      );
      return Book.fromJson(response.data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<void> deleteBook(int bookId) async {
    try {
      await _dio.delete('${AppConstants.booksEndpoint}/$bookId');
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  // Users Methods
  Future<List<User>> getUsers() async {
    try {
      final response = await _dio.get(AppConstants.usersEndpoint);
      final List<dynamic> usersData = response.data['data'] ?? response.data;
      return usersData.map((json) => User.fromJson(json)).toList();
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<User> getUser(int userId) async {
    try {
      final response = await _dio.get('${AppConstants.usersEndpoint}/$userId');
      return User.fromJson(response.data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<User> updateProfile(User user) async {
    try {
      final response = await _dio.put(
        '${AppConstants.usersEndpoint}/${user.userId}',
        data: user.toJson(),
      );
      return User.fromJson(response.data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  // Loans Methods
  Future<void> requestLoan(int bookId, int borrowerId) async {
    try {
      await _dio.post(
        AppConstants.loansEndpoint,
        data: {
          'bookId': bookId,
          'borrowerId': borrowerId,
        },
      );
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<void> approveLoan(int loanId) async {
    try {
      await _dio.put('${AppConstants.loansEndpoint}/$loanId/approve');
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<void> rejectLoan(int loanId) async {
    try {
      await _dio.put('${AppConstants.loansEndpoint}/$loanId/reject');
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<void> markAsReceived(int loanId) async {
    try {
      await _dio.put('${AppConstants.loansEndpoint}/$loanId/received');
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<void> returnBook(int loanId) async {
    try {
      await _dio.put('${AppConstants.loansEndpoint}/$loanId/return');
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  // Organizations Methods
  Future<List<Map<String, dynamic>>> getOrganizations() async {
    try {
      final response = await _dio.get(AppConstants.organizationsEndpoint);
      final List<dynamic> orgsData = response.data['data'] ?? response.data;
      return orgsData.cast<Map<String, dynamic>>();
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<Map<String, dynamic>> createOrganization(String name, String description) async {
    try {
      final response = await _dio.post(
        AppConstants.organizationsEndpoint,
        data: {
          'name': name,
          'description': description,
        },
      );
      return response.data;
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<void> inviteMember(int organizationId, String email) async {
    try {
      await _dio.post(
        '${AppConstants.organizationsEndpoint}/$organizationId/invite',
        data: {'email': email},
      );
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  // Error Handling
  String _handleError(DioException e) {
    if (e.response?.statusCode == 401) {
      return AppConstants.unauthorizedError;
    } else if (e.response?.statusCode == 404) {
      return 'Resource not found';
    } else if (e.response?.statusCode == 422) {
      return AppConstants.validationError;
    } else if (e.response?.statusCode == 500) {
      return AppConstants.serverError;
    } else if (e.type == DioExceptionType.connectionTimeout ||
               e.type == DioExceptionType.receiveTimeout) {
      return AppConstants.networkError;
    } else {
      return e.response?.data?['message'] ?? AppConstants.unknownError;
    }
  }

  bool get isAuthenticated => _token != null;
  String? get token => _token;
} 