import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:equatable/equatable.dart';
import '../../../data/models/auth_model.dart';
import '../../../data/services/auth_service.dart';
import '../../../data/services/api_service.dart';

// Events
abstract class AuthEvent extends Equatable {
  const AuthEvent();

  @override
  List<Object?> get props => [];
}

class AuthLoginRequested extends AuthEvent {
  final String email;
  final String password;

  const AuthLoginRequested({
    required this.email,
    required this.password,
  });

  @override
  List<Object?> get props => [email, password];
}

class AuthSignupRequested extends AuthEvent {
  final String name;
  final String email;
  final String password;

  const AuthSignupRequested({
    required this.name,
    required this.email,
    required this.password,
  });

  @override
  List<Object?> get props => [name, email, password];
}

class AuthLogoutRequested extends AuthEvent {}

class AuthCheckRequested extends AuthEvent {}

// States
abstract class AuthState extends Equatable {
  const AuthState();

  @override
  List<Object?> get props => [];
}

class AuthInitial extends AuthState {}

class AuthLoading extends AuthState {}

class AuthAuthenticated extends AuthState {
  final AuthDataModel authData;

  const AuthAuthenticated(this.authData);

  @override
  List<Object?> get props => [authData];
}

class AuthUnauthenticated extends AuthState {}

class AuthError extends AuthState {
  final String message;

  const AuthError(this.message);

  @override
  List<Object?> get props => [message];
}

// BLoC
class AuthBloc extends Bloc<AuthEvent, AuthState> {
  final ApiService _apiService;

  AuthBloc(this._apiService) : super(AuthInitial()) {
    on<AuthLoginRequested>(_onLoginRequested);
    on<AuthSignupRequested>(_onSignupRequested);
    on<AuthLogoutRequested>(_onLogoutRequested);
    on<AuthCheckRequested>(_onCheckRequested);
  }

  Future<void> _onLoginRequested(
    AuthLoginRequested event,
    Emitter<AuthState> emit,
  ) async {
    emit(AuthLoading());
    try {
      final result = await _apiService.login(
        LoginRequestModel(
          email: event.email,
          password: event.password,
        ),
      );

      if (result.success && result.data != null) {
        await AuthService.saveAuthData(result.data!);
        emit(AuthAuthenticated(result.data!));
      } else {
        emit(AuthError(result.message ?? 'Erro no login'));
      }
    } catch (e) {
      emit(AuthError('Erro de conexão: $e'));
    }
  }

  Future<void> _onSignupRequested(
    AuthSignupRequested event,
    Emitter<AuthState> emit,
  ) async {
    emit(AuthLoading());
    try {
      final result = await _apiService.signup(
        SignupRequestModel(
          name: event.name,
          email: event.email,
          password: event.password,
        ),
      );

      if (result.success && result.data != null) {
        await AuthService.saveAuthData(result.data!);
        emit(AuthAuthenticated(result.data!));
      } else {
        emit(AuthError(result.message ?? 'Erro no cadastro'));
      }
    } catch (e) {
      emit(AuthError('Erro de conexão: $e'));
    }
  }

  Future<void> _onLogoutRequested(
    AuthLogoutRequested event,
    Emitter<AuthState> emit,
  ) async {
    emit(AuthLoading());
    try {
      await _apiService.logout();
    } catch (e) {
      // Continue mesmo se a API falhar
    }
    await AuthService.clearAuthData();
    emit(AuthUnauthenticated());
  }

  Future<void> _onCheckRequested(
    AuthCheckRequested event,
    Emitter<AuthState> emit,
  ) async {
    emit(AuthLoading());
    try {
      final authData = await AuthService.getAuthData();
      if (authData != null) {
        emit(AuthAuthenticated(authData));
      } else {
        emit(AuthUnauthenticated());
      }
    } catch (e) {
      emit(AuthUnauthenticated());
    }
  }
} 