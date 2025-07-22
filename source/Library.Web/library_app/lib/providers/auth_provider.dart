import 'package:flutter/material.dart';
import '../models/user.dart';
import '../models/auth.dart';
import '../services/api_service.dart';
import '../constants/app_constants.dart';

class AuthProvider extends ChangeNotifier {
  final ApiService _apiService = ApiService();
  
  User? _currentUser;
  bool _isLoading = false;
  String? _error;

  User? get currentUser => _currentUser;
  bool get isLoading => _isLoading;
  String? get error => _error;
  bool get isAuthenticated => _currentUser != null && _apiService.isAuthenticated;

  void _setLoading(bool loading) {
    _isLoading = loading;
    notifyListeners();
  }

  void _setError(String? error) {
    _error = error;
    notifyListeners();
  }

  void _setUser(User? user) {
    _currentUser = user;
    notifyListeners();
  }

  Future<bool> login(String email, String password) async {
    _setLoading(true);
    _setError(null);

    try {
      final response = await _apiService.login(email, password);
      
      if (response.success && response.user != null) {
        _setUser(response.user);
        return true;
      } else {
        _setError(response.message ?? AppConstants.unknownError);
        return false;
      }
    } catch (e) {
      _setError(e.toString());
      return false;
    } finally {
      _setLoading(false);
    }
  }

  Future<bool> signup(SignUpRequest request) async {
    _setLoading(true);
    _setError(null);

    try {
      final response = await _apiService.signup(request);
      
      if (response.success && response.user != null) {
        _setUser(response.user);
        return true;
      } else {
        _setError(response.message ?? AppConstants.unknownError);
        return false;
      }
    } catch (e) {
      _setError(e.toString());
      return false;
    } finally {
      _setLoading(false);
    }
  }

  Future<void> logout() async {
    _setLoading(true);
    
    try {
      await _apiService.logout();
    } catch (e) {
      // Even if logout fails, we should clear local state
      print('Logout error: $e');
    } finally {
      _setUser(null);
      _setLoading(false);
    }
  }

  Future<void> updateProfile(User updatedUser) async {
    _setLoading(true);
    _setError(null);

    try {
      final user = await _apiService.updateProfile(updatedUser);
      _setUser(user);
    } catch (e) {
      _setError(e.toString());
    } finally {
      _setLoading(false);
    }
  }

  void clearError() {
    _setError(null);
  }

  // Check if user is still authenticated on app start
  Future<void> checkAuthStatus() async {
    if (_apiService.isAuthenticated && _currentUser == null) {
      // Token exists but no user data, try to get current user
      try {
        // You might need to add a getCurrentUser endpoint to your API
        // For now, we'll just clear the token if no user data
        await logout();
      } catch (e) {
        await logout();
      }
    }
  }
} 