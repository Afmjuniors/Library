import { User, LoginResult } from '../types';
import { API_CONFIG } from '../constants';

// Interface para resposta da API de autenticação
interface AuthApiResponse {
  userId: number;
  name: string;
  email: string;
  phone?: string;
  address?: string;
  additionalInfo?: string;
  image?: string;
  birthDay?: string;
  createdAt?: string;
  cultureInfo?: string;
  token?: string;
}

// Headers padrão
const getDefaultHeaders = () => ({
  'Content-Type': 'application/json',
});

// Cliente HTTP customizado
class ApiClient {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {
    const url = `${this.baseURL}${endpoint}`;
    
    const config: RequestInit = {
      headers: {
        ...getDefaultHeaders(),
        ...options.headers,
      },
      ...options,
    };

    try {
      const controller = new AbortController();
      const timeoutId = setTimeout(() => controller.abort(), API_CONFIG.TIMEOUT);

      const response = await fetch(url, {
        ...config,
        signal: controller.signal,
      });

      clearTimeout(timeoutId);
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      return await response.json();
    } catch (error) {
      console.error(`API Error (${endpoint}):`, error);
      if (error instanceof Error && error.name === 'AbortError') {
        throw new Error('Request timeout');
      }
      throw error;
    }
  }

  // Métodos HTTP
  async get<T>(endpoint: string, headers?: Record<string, string>): Promise<T> {
    return this.request<T>(endpoint, { method: 'GET', headers });
  }

  async post<T>(
    endpoint: string, 
    data?: any, 
    headers?: Record<string, string>
  ): Promise<T> {
    return this.request<T>(endpoint, {
      method: 'POST',
      headers,
      body: data ? JSON.stringify(data) : undefined,
    });
  }

  async put<T>(
    endpoint: string, 
    data?: any, 
    headers?: Record<string, string>
  ): Promise<T> {
    return this.request<T>(endpoint, {
      method: 'PUT',
      headers,
      body: data ? JSON.stringify(data) : undefined,
    });
  }

  async delete<T>(endpoint: string, headers?: Record<string, string>): Promise<T> {
    return this.request<T>(endpoint, { method: 'DELETE', headers });
  }
}

// Instância do cliente API
const apiClient = new ApiClient(API_CONFIG.BASE_URL);

// Serviços específicos
export const authService = {
  async login(email: string, password: string): Promise<LoginResult> {
    try {
      console.log('🔗 Iniciando chamada para API...');
      console.log('📧 Email:', email);

      const data = await apiClient.post<AuthApiResponse>('/AccessControl/Authenticate', {
        email,
        password,
      });

      console.log('📦 Dados da resposta:', data);

      if (data && data.userId && data.name && data.email) {
        const userData: User = {
          userId: data.userId,
          name: data.name,
          email: data.email,
          phone: data.phone,
          address: data.address,
          additionalInfo: data.additionalInfo,
          image: data.image,
          birthDay: data.birthDay,
          createdAt: data.createdAt,
          cultureInfo: data.cultureInfo,
        };

        return {
          success: true,
          data: {
            user: userData,
            token: data.token || 'dummy-token',
          },
        };
      } else {
        console.log('❌ Login falhou na API: Sem mensagem de erro');
        return {
          success: false,
          message: 'Login falhou',
        };
      }
    } catch (error) {
      console.error('❌ Erro na API:', error);
      return {
        success: false,
        message: error instanceof Error ? error.message : 'Erro inesperado',
      };
    }
  },

  async signup(userData: any): Promise<LoginResult> {
    try {
      const data = await apiClient.post<AuthApiResponse>('/AccessControl/Register', userData);
      
      if (data && data.userId && data.name && data.email) {
        const user: User = {
          userId: data.userId,
          name: data.name,
          email: data.email,
          phone: data.phone,
          address: data.address,
          additionalInfo: data.additionalInfo,
          image: data.image,
          birthDay: data.birthDay,
          createdAt: data.createdAt,
          cultureInfo: data.cultureInfo,
        };

        return {
          success: true,
          data: {
            user,
            token: data.token || 'dummy-token',
          },
        };
      } else {
        return {
          success: false,
          message: 'Signup failed',
        };
      }
    } catch (error) {
      return {
        success: false,
        message: error instanceof Error ? error.message : 'Erro inesperado',
      };
    }
  },

  async logout(): Promise<void> {
    // Implementar logout se necessário
    console.log('Logout realizado');
  },
};

export const bookService = {
  async getBooks() {
    return apiClient.get('/books');
  },

  async getMyBooks(userId: number) {
    return apiClient.get(`/books/user/${userId}`);
  },

  async addBook(bookData: any) {
    return apiClient.post('/books', bookData);
  },

  async updateBook(bookId: number, bookData: any) {
    return apiClient.put(`/books/${bookId}`, bookData);
  },

  async deleteBook(bookId: number) {
    return apiClient.delete(`/books/${bookId}`);
  },
};

export const userService = {
  async getUserProfile(userId: number) {
    return apiClient.get(`/users/${userId}`);
  },

  async updateUserProfile(userId: number, userData: any) {
    return apiClient.put(`/users/${userId}`, userData);
  },
};

// Exportar o cliente para uso direto se necessário
export { apiClient };

// Exportar como apiService para compatibilidade
export const apiService = {
  auth: authService,
  books: bookService,
  users: userService,
  client: apiClient,
}; 