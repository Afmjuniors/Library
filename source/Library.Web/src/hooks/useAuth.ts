import { useState, useEffect } from 'react';
import authService from '../services/authService';
import { User } from '../types';

export const useAuth = () => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    checkAuth();
  }, []);

  const checkAuth = async () => {
    try {
      console.log('🔍 Verificando autenticação...');
      const token = typeof window !== 'undefined' ? localStorage.getItem('authToken') : null;
      console.log('🔑 Token encontrado:', !!token);
      if (token) {
        const currentUser = await authService.getCurrentUser();
        console.log('👤 Usuário atual recuperado:', currentUser);
        // O backend retorna os dados do usuário diretamente
        setUser(currentUser);
      }
    } catch (err) {
      console.error('❌ Auth check failed:', err);
      if (typeof window !== 'undefined') {
        localStorage.removeItem('authToken');
      }
    } finally {
      setLoading(false);
    }
  };

  const login = async (email: string, password: string) => {
    try {
      setError(null);
      console.log('🔐 Iniciando login...');
      const response = await authService.login({ email, password });
      console.log('✅ Login bem-sucedido:', response);
      console.log('🔍 Estrutura da resposta:', {
        hasUser: !!response.user,
        hasToken: !!response.token,
        responseKeys: Object.keys(response),
        userData: response.user || response
      });
      
      if (typeof window !== 'undefined' && response.token) {
        localStorage.setItem('authToken', response.token);
        console.log('🔑 Token salvo:', response.token.substring(0, 20) + '...');
      } else {
        console.warn('⚠️ Nenhum token encontrado na resposta');
      }
      
      // O backend retorna os dados do usuário diretamente, não em response.user
      const userData = response.user || response;
      setUser(userData);
      console.log('👤 Usuário definido:', userData);
      return response;
    } catch (err: any) {
      console.error('❌ Erro no login:', err);
      setError(err.response?.data?.message || 'Login failed');
      throw err;
    }
  };

  const signup = async (name: string, email: string, password: string) => {
    try {
      setError(null);
      const response = await authService.signup({ name, email, password });
      if (typeof window !== 'undefined') {
        localStorage.setItem('authToken', response.token);
      }
      
      // O backend retorna os dados do usuário diretamente, não em response.user
      const userData = response.user || response;
      setUser(userData);
      return response;
    } catch (err: any) {
      setError(err.response?.data?.message || 'Signup failed');
      throw err;
    }
  };

  const logout = async () => {
    try {
      await authService.logout();
    } catch (err) {
      console.error('Logout error:', err);
    } finally {
      if (typeof window !== 'undefined') {
        localStorage.removeItem('authToken');
      }
      setUser(null);
    }
  };

  const updateProfile = async (userData: Partial<User>) => {
    try {
      setError(null);
      const updatedUser = await authService.updateProfile(userData);
      setUser(updatedUser);
      return updatedUser;
    } catch (err: any) {
      setError(err.response?.data?.message || 'Profile update failed');
      throw err;
    }
  };

  return {
    user,
    loading,
    error,
    login,
    signup,
    logout,
    updateProfile,
    isAuthenticated: !!user,
  };
}; 