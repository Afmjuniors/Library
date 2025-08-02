import { useState, useEffect, useCallback } from 'react';
import { Alert } from 'react-native';
import * as SecureStore from 'expo-secure-store';
import { User, AuthData, LoginResult } from '../types';
import { authService } from '../services/api';
import { STORAGE_KEYS, ERROR_MESSAGES } from '../constants';

export const useAuth = () => {
  const [user, setUser] = useState<User | null>(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [isCheckingAuth, setIsCheckingAuth] = useState(true);

  // Funções para persistência de dados com fallback para web
  const saveAuthData = useCallback(async (user: User, token: string) => {
    try {
      console.log('Salvando dados de autenticação...');
      
      // Verificar se estamos no web
      if (typeof window !== 'undefined' && window.localStorage) {
        console.log('Usando localStorage (web)');
        localStorage.setItem(STORAGE_KEYS.USER, JSON.stringify(user));
        localStorage.setItem(STORAGE_KEYS.TOKEN, token);
      } else {
        console.log('Usando SecureStore (mobile)');
        await SecureStore.setItemAsync(STORAGE_KEYS.USER, JSON.stringify(user));
        await SecureStore.setItemAsync(STORAGE_KEYS.TOKEN, token);
      }
      
      console.log('Dados salvos com sucesso!');
    } catch (error) {
      console.error('Erro ao salvar dados de autenticação:', error);
      throw error;
    }
  }, []);

  const getAuthData = useCallback(async (): Promise<AuthData | null> => {
    try {
      console.log('Recuperando dados de autenticação...');
      
      let user = null;
      let token = null;
      
      // Verificar se estamos no web
      if (typeof window !== 'undefined' && window.localStorage) {
        console.log('Usando localStorage (web)');
        user = localStorage.getItem(STORAGE_KEYS.USER);
        token = localStorage.getItem(STORAGE_KEYS.TOKEN);
      } else {
        console.log('Usando SecureStore (mobile)');
        user = await SecureStore.getItemAsync(STORAGE_KEYS.USER);
        token = await SecureStore.getItemAsync(STORAGE_KEYS.TOKEN);
      }
      
      if (user && token) {
        const parsedUser = JSON.parse(user);
        console.log('Usuário recuperado com sucesso');
        return {
          user: parsedUser,
          token: token,
        };
      }
      
      console.log('Nenhum dado de autenticação encontrado');
      return null;
    } catch (error) {
      console.error('Erro ao recuperar dados de autenticação:', error);
      return null;
    }
  }, []);

  const clearAuthData = useCallback(async () => {
    try {
      console.log('Removendo dados de autenticação...');
      
      // Verificar se estamos no web
      if (typeof window !== 'undefined' && window.localStorage) {
        console.log('Usando localStorage (web)');
        localStorage.removeItem(STORAGE_KEYS.USER);
        localStorage.removeItem(STORAGE_KEYS.TOKEN);
      } else {
        console.log('Usando SecureStore (mobile)');
        await SecureStore.deleteItemAsync(STORAGE_KEYS.USER);
        await SecureStore.deleteItemAsync(STORAGE_KEYS.TOKEN);
      }
      
      console.log('Dados de autenticação removidos com sucesso');
    } catch (error) {
      console.error('Erro ao remover dados de autenticação:', error);
      throw error;
    }
  }, []);

  // Função para fazer login
  const login = useCallback(async (email: string, password: string) => {
    if (isLoading) return; // Prevenir múltiplas chamadas

    setIsLoading(true);
    console.log('=== INICIANDO LOGIN ===');
    
    try {
      const result = await authService.login(email, password);
      console.log('Resultado do login:', result);
      
      if (result.success && result.data) {
        console.log('Login bem-sucedido, salvando dados...');
        setUser(result.data.user);
        setIsLoggedIn(true);
        
        // Salvar dados de autenticação
        await saveAuthData(result.data.user, result.data.token);
        console.log('Dados salvos, login finalizado!');
      } else {
        console.log('Login falhou:', result.message);
        Alert.alert('Erro', result.message || ERROR_MESSAGES.auth.invalidCredentials);
      }
    } catch (error) {
      console.error('Erro no login:', error);
      Alert.alert('Erro', ERROR_MESSAGES.unknown);
    } finally {
      setIsLoading(false);
    }
  }, [isLoading, saveAuthData]);

  // Função para fazer logout
  const logout = useCallback(async () => {
    try {
      await clearAuthData();
      setUser(null);
      setIsLoggedIn(false);
      console.log('Logout realizado com sucesso');
    } catch (error) {
      console.error('Erro no logout:', error);
      // Mesmo com erro, limpar o estado local
      setUser(null);
      setIsLoggedIn(false);
    }
  }, [clearAuthData]);

  // Verificar dados de autenticação salvos ao iniciar
  useEffect(() => {
    const checkAuthData = async () => {
      console.log('=== INICIANDO VERIFICAÇÃO DE AUTENTICAÇÃO ===');
      try {
        const authData = await getAuthData();
        
        if (authData && authData.user && authData.token) {
          console.log('Login automático sendo realizado...');
          setUser(authData.user);
          setIsLoggedIn(true);
          console.log('Login automático realizado com sucesso!');
        } else {
          console.log('Nenhum dado de autenticação válido encontrado');
        }
      } catch (error) {
        console.error('Erro ao verificar dados de autenticação:', error);
      } finally {
        console.log('Finalizando verificação de autenticação');
        setIsCheckingAuth(false);
      }
    };

    checkAuthData();
  }, [getAuthData]);

  return {
    user,
    isLoggedIn,
    isLoading,
    isCheckingAuth,
    login,
    logout,
  };
}; 