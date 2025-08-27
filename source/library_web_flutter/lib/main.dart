import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:dio/dio.dart';
import 'package:go_router/go_router.dart';
import 'core/constants/app_colors.dart';
import 'core/constants/app_typography.dart';
import 'data/services/api_service.dart';
import 'presentation/blocs/auth/auth_bloc.dart';
import 'presentation/screens/auth/login_screen.dart';
import 'presentation/screens/main/main_screen.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [
        BlocProvider<AuthBloc>(
          create: (context) => AuthBloc(_createApiService())
            ..add(AuthCheckRequested()),
        ),
      ],
      child: MaterialApp.router(
        title: 'Library App',
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(
            seedColor: AppColors.primary,
            brightness: Brightness.light,
          ),
          useMaterial3: true,
          textTheme: TextTheme(
            headlineLarge: TextStyle(
              fontSize: AppTypography.xxl,
              fontWeight: AppTypography.bold,
              color: AppColors.textPrimary,
            ),
            headlineMedium: TextStyle(
              fontSize: AppTypography.xl,
              fontWeight: AppTypography.bold,
              color: AppColors.textPrimary,
            ),
            titleLarge: TextStyle(
              fontSize: AppTypography.lg,
              fontWeight: AppTypography.semibold,
              color: AppColors.textPrimary,
            ),
            bodyLarge: TextStyle(
              fontSize: AppTypography.md,
              color: AppColors.textPrimary,
            ),
            bodyMedium: TextStyle(
              fontSize: AppTypography.sm,
              color: AppColors.textSecondary,
            ),
          ),
        ),
        routerConfig: _router,
      ),
    );
  }

  ApiService _createApiService() {
    final dio = Dio();
    return ApiService(dio);
  }
}

final _router = GoRouter(
  initialLocation: '/',
  routes: [
    GoRoute(
      path: '/',
      builder: (context, state) => BlocBuilder<AuthBloc, AuthState>(
        builder: (context, state) {
          if (state is AuthLoading) {
            return const Scaffold(
              body: Center(
                child: CircularProgressIndicator(),
              ),
            );
          }
          
          if (state is AuthAuthenticated) {
            return MainScreen(user: state.authData.user);
          }
          
          return const LoginScreen();
        },
      ),
    ),
  ],
);
