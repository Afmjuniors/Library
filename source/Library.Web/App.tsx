import React from 'react';
import { LoginForm } from './src/components/LoginForm';
import { MainScreen } from './src/screens/MainScreen';
import { LoadingScreen } from './src/components/LoadingScreen';
import { useAuth } from './src/hooks/useAuth';

export default function App() {
  const { user, isLoggedIn, isLoading, isCheckingAuth, login, logout } = useAuth();

  // Mostrar loading enquanto verifica autenticação
  if (isCheckingAuth) {
    return <LoadingScreen message="Verificando autenticação..." />;
  }

  // Mostrar tela de login se não estiver logado
  if (!isLoggedIn) {
    return <LoginForm onLogin={login} isLoading={isLoading} />;
  }

  // Mostrar tela principal se estiver logado
  return <MainScreen user={user!} onLogout={logout} />;
}
