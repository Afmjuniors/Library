# Library App Flutter

Uma aplicação Flutter que replica as funcionalidades do Library.Web, implementando um sistema de biblioteca comunitária com gerenciamento de livros, organizações e usuários.

## 🚀 Funcionalidades

### Autenticação
- ✅ Login com email e senha
- ✅ Verificação de autenticação persistente
- ✅ Logout seguro
- ⏳ Cadastro de novos usuários

### Biblioteca
- ✅ Listagem de livros disponíveis
- ✅ Filtros por status (Disponível, Emprestado, Reservado)
- ✅ Busca por título, autor ou gênero
- ✅ Visualização de detalhes do livro
- ⏳ Solicitação de empréstimo
- ⏳ Adição de novos livros

### Organizações
- ✅ Listagem de organizações
- ✅ Visualização de estatísticas
- ✅ Informações sobre regras e regulamentos
- ⏳ Criação de novas organizações
- ⏳ Gerenciamento de membros

### Perfil do Usuário
- ✅ Visualização de informações do usuário
- ✅ Logout da aplicação
- ⏳ Edição de perfil
- ⏳ Histórico de empréstimos

## 🏗️ Arquitetura

O projeto segue as melhores práticas do Flutter com uma arquitetura limpa:

```
lib/
├── core/
│   └── constants/          # Constantes da aplicação
├── data/
│   ├── models/            # Modelos de dados
│   ├── services/          # Serviços de API e local storage
│   └── mock_data.dart     # Dados mock para desenvolvimento
├── presentation/
│   ├── blocs/            # Gerenciamento de estado (BLoC)
│   ├── screens/          # Telas da aplicação
│   └── widgets/          # Widgets reutilizáveis
└── main.dart             # Ponto de entrada da aplicação
```

## 🛠️ Tecnologias Utilizadas

- **Flutter**: Framework principal
- **flutter_bloc**: Gerenciamento de estado
- **go_router**: Navegação
- **dio**: Cliente HTTP
- **retrofit**: Geração de código para APIs
- **shared_preferences**: Armazenamento local
- **cached_network_image**: Cache de imagens
- **json_annotation**: Serialização JSON

## 📱 Telas Implementadas

### 1. Tela de Login
- Interface limpa e moderna
- Validação de campos
- Indicador de loading
- Tratamento de erros

### 2. Tela Principal (MainScreen)
- Navegação por abas
- Aba de Livros com filtros
- Aba de Organizações
- Aba de Perfil

### 3. Aba de Livros
- Listagem com cards informativos
- Filtros por status
- Barra de pesquisa
- Imagens com cache

### 4. Aba de Organizações
- Cards com estatísticas
- Informações sobre regras
- Visualização de membros

## 🎨 Design System

O projeto utiliza um design system consistente:

### Cores
- **Primary**: #007AFF (Azul)
- **Success**: #4CAF50 (Verde)
- **Warning**: #FF9800 (Laranja)
- **Error**: #F44336 (Vermelho)
- **Info**: #2196F3 (Azul claro)

### Tipografia
- **Headline**: 24px, Bold
- **Title**: 18px, Semibold
- **Body**: 16px, Normal
- **Caption**: 14px, Normal

### Espaçamentos
- **xs**: 4px
- **sm**: 8px
- **md**: 16px
- **lg**: 24px
- **xl**: 32px
- **xxl**: 48px

## 🚀 Como Executar

1. **Clone o repositório**
   ```bash
   git clone <repository-url>
   cd library_web_flutter
   ```

2. **Instale as dependências**
   ```bash
   flutter pub get
   ```

3. **Execute a aplicação**
   ```bash
   flutter run
   ```

## 📦 Estrutura de Dados

### UserModel
```dart
class UserModel {
  final int userId;
  final String name;
  final String email;
  final String? phone;
  final String? address;
  final String? image;
  // ...
}
```

### BookModel
```dart
class BookModel {
  final int bookId;
  final String name;
  final String author;
  final int genre;
  final int bookStatusId;
  final int ownerId;
  final BookOwnerInfo? ownerInfo;
  // ...
}
```

### OrganizationModel
```dart
class ExtendedOrganizationModel {
  final int id;
  final String name;
  final String description;
  final int memberCount;
  final String role;
  final OrganizationStats stats;
  // ...
}
```

## 🔧 Configuração da API

A aplicação está configurada para conectar com a API do Library.Web:

```dart
// lib/core/constants/app_constants.dart
static const String baseUrl = 'https://localhost:53735/api/v1';
```

## 📱 Funcionalidades Futuras

- [ ] Cadastro de usuários
- [ ] Adição de livros
- [ ] Solicitação de empréstimos
- [ ] Criação de organizações
- [ ] Notificações push
- [ ] Modo offline
- [ ] Testes unitários e de widget
- [ ] Internacionalização (i18n)

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 👨‍💻 Desenvolvido por

Replicação do Library.Web em Flutter seguindo as melhores práticas de desenvolvimento mobile.
