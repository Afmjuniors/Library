import 'package:shared_preferences/shared_preferences.dart';
import '../models/auth_model.dart';
import '../models/user_model.dart';
import '../../core/constants/app_constants.dart';

class AuthService {
  static const String _userKey = AppConstants.userKey;
  static const String _tokenKey = AppConstants.tokenKey;

  // Salvar dados de autenticação
  static Future<void> saveAuthData(AuthDataModel authData) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString(_userKey, authData.user.toJson().toString());
    await prefs.setString(_tokenKey, authData.token);
  }

  // Obter dados de autenticação
  static Future<AuthDataModel?> getAuthData() async {
    final prefs = await SharedPreferences.getInstance();
    final userJson = prefs.getString(_userKey);
    final token = prefs.getString(_tokenKey);

    if (userJson != null && token != null) {
      try {
        final user = UserModel.fromJson(Map<String, dynamic>.from(
          userJson as Map<String, dynamic>
        ));
        return AuthDataModel(user: user, token: token);
      } catch (e) {
        return null;
      }
    }
    return null;
  }

  // Verificar se está autenticado
  static Future<bool> isAuthenticated() async {
    final authData = await getAuthData();
    return authData != null;
  }

  // Obter token
  static Future<String?> getToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString(_tokenKey);
  }

  // Obter usuário atual
  static Future<UserModel?> getCurrentUser() async {
    final authData = await getAuthData();
    return authData?.user;
  }

  // Limpar dados de autenticação
  static Future<void> clearAuthData() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove(_userKey);
    await prefs.remove(_tokenKey);
  }
} 