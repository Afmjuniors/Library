import React from 'react';
import { LoginForm } from './src/components/LoginForm';
import { MainScreen } from './src/screens/MainScreen';
import { LoadingScreen } from './src/components/LoadingScreen';
import { useAuth } from './src/hooks/useAuth';

export default function App() {
  const { user, loading, isAuthenticated, login, logout } = useAuth();

  console.log('🔄 App render - user:', user, 'isAuthenticated:', isAuthenticated, 'loading:', loading);

  // Mostrar loading enquanto verifica autenticação
  if (loading) {
    console.log('⏳ Mostrando loading...');
    return <LoadingScreen message="Verificando autenticação..." />;
  }

  // Mostrar tela de login se não estiver logado
  if (!isAuthenticated) {
    console.log('🔐 Mostrando tela de login...');
    return <LoginForm onLogin={login} isLoading={loading} />;
  }

  // Mostrar tela principal se estiver logado
  console.log('🏠 Mostrando tela principal...');
  return <MainScreen user={user!} onLogout={logout} />;
}
