import { VALIDATION } from '../constants';

export interface ValidationResult {
  isValid: boolean;
  message?: string;
}

export const validation = {
  // Validação de email
  email: (email: string): ValidationResult => {
    if (!email.trim()) {
      return {
        isValid: false,
        message: 'Email é obrigatório',
      };
    }

    if (!VALIDATION.email.pattern.test(email)) {
      return {
        isValid: false,
        message: VALIDATION.email.message,
      };
    }

    return { isValid: true };
  },

  // Validação de senha
  password: (password: string): ValidationResult => {
    if (!password.trim()) {
      return {
        isValid: false,
        message: 'Senha é obrigatória',
      };
    }

    if (password.length < VALIDATION.password.minLength) {
      return {
        isValid: false,
        message: VALIDATION.password.message,
      };
    }

    return { isValid: true };
  },

  // Validação de nome
  name: (name: string): ValidationResult => {
    if (!name.trim()) {
      return {
        isValid: false,
        message: 'Nome é obrigatório',
      };
    }

    if (name.trim().length < VALIDATION.name.minLength) {
      return {
        isValid: false,
        message: VALIDATION.name.message,
      };
    }

    return { isValid: true };
  },

  // Validação de campos obrigatórios
  required: (value: string, fieldName: string): ValidationResult => {
    if (!value || !value.trim()) {
      return {
        isValid: false,
        message: `${fieldName} é obrigatório`,
      };
    }

    return { isValid: true };
  },

  // Validação de URL
  url: (url: string): ValidationResult => {
    if (!url.trim()) {
      return { isValid: true }; // URL é opcional
    }

    try {
      new URL(url);
      return { isValid: true };
    } catch {
      return {
        isValid: false,
        message: 'Digite uma URL válida',
      };
    }
  },

  // Validação de telefone (formato brasileiro)
  phone: (phone: string): ValidationResult => {
    if (!phone.trim()) {
      return { isValid: true }; // Telefone é opcional
    }

    const phoneRegex = /^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$/;
    
    if (!phoneRegex.test(phone.replace(/\s/g, ''))) {
      return {
        isValid: false,
        message: 'Digite um telefone válido',
      };
    }

    return { isValid: true };
  },

  // Validação de data de nascimento
  birthDate: (date: string): ValidationResult => {
    if (!date.trim()) {
      return { isValid: true }; // Data é opcional
    }

    const birthDate = new Date(date);
    const today = new Date();
    const age = today.getFullYear() - birthDate.getFullYear();

    if (isNaN(birthDate.getTime())) {
      return {
        isValid: false,
        message: 'Digite uma data válida',
      };
    }

    if (age < 0 || age > 120) {
      return {
        isValid: false,
        message: 'Digite uma data de nascimento válida',
      };
    }

    return { isValid: true };
  },

  // Validação de livro
  book: {
    name: (name: string): ValidationResult => {
      return validation.required(name, 'Nome do livro');
    },

    author: (author: string): ValidationResult => {
      return validation.required(author, 'Autor');
    },

    genre: (genre: number): ValidationResult => {
      if (!genre || genre <= 0) {
        return {
          isValid: false,
          message: 'Selecione um gênero',
        };
      }

      return { isValid: true };
    },

    description: (description: string): ValidationResult => {
      if (!description.trim()) {
        return { isValid: true }; // Descrição é opcional
      }

      if (description.length > 1000) {
        return {
          isValid: false,
          message: 'Descrição deve ter no máximo 1000 caracteres',
        };
      }

      return { isValid: true };
    },
  },
};

// Função para validar múltiplos campos
export const validateForm = (validations: ValidationResult[]): ValidationResult => {
  const firstError = validations.find(validation => !validation.isValid);
  
  if (firstError) {
    return firstError;
  }

  return { isValid: true };
}; 