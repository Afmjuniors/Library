export 'app_colors.dart';
export 'app_spacing.dart';
export 'app_typography.dart';
export 'app_radius.dart';
export 'app_shadows.dart';

class AppConstants {
  // API Configuration
  static const String baseUrl = 'https://localhost:53735/api/v1';
  static const int timeout = 10000; // 10 seconds

  // Storage Keys
  static const String userKey = 'library_user';
  static const String tokenKey = 'library_token';

  // Validation
  static const int minPasswordLength = 6;
  static const int minNameLength = 2;

  // Pagination
  static const int defaultPageSize = 20;
  static const int maxPageSize = 100;

  // Error Messages
  static const String networkError = 'Erro de conexão. Verifique sua internet.';
  static const String serverError = 'Erro no servidor. Tente novamente mais tarde.';
  static const String unknownError = 'Erro inesperado. Tente novamente.';
  static const String invalidCredentials = 'Email ou senha incorretos';
  static const String userNotFound = 'Usuário não encontrado';
  static const String accountDisabled = 'Conta desabilitada';
} 