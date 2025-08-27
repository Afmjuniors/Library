import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../../core/constants/app_colors.dart';
import '../../../core/constants/app_spacing.dart';
import '../../../core/constants/app_typography.dart';
import '../../../core/constants/app_radius.dart';
import '../../../core/constants/app_shadows.dart';
import '../../../data/models/book_model.dart';
import '../../../data/models/enums.dart';
import '../../../data/mock_data.dart';
import '../../blocs/auth/auth_bloc.dart';
import '../../widgets/book_card.dart';
import '../../widgets/search_bar.dart';
import '../../widgets/filter_chip.dart' as custom;

class BooksTab extends StatefulWidget {
  const BooksTab({super.key});

  @override
  State<BooksTab> createState() => _BooksTabState();
}

class _BooksTabState extends State<BooksTab> {
  String _searchQuery = '';
  List<int> _selectedGenres = [];
  List<int> _selectedStatuses = [1]; // Default: available only
  List<int> _selectedOrganizations = [];

  List<BookModel> _books = [];
  bool _isLoading = true;
  String? _error;

  @override
  void initState() {
    super.initState();
    _loadBooks();
  }

  Future<void> _loadBooks() async {
    try {
      setState(() {
        _isLoading = true;
        _error = null;
      });

      // Get API service from context
      final apiService = context.read<AuthBloc>().apiService;
      final books = await apiService.getBooks(
        query: _searchQuery.isNotEmpty ? _searchQuery : null,
        status: _selectedStatuses.isNotEmpty ? _selectedStatuses.first : null,
      );

      setState(() {
        _books = books;
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _error = 'Erro ao carregar livros: $e';
        _isLoading = false;
      });
    }
  }

  Future<void> _refreshBooks() async {
    await _loadBooks();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppColors.background,
      body: SafeArea(
        child: Column(
          children: [
            // Header
            Container(
              padding: const EdgeInsets.all(AppSpacing.lg),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Biblioteca',
                    style: TextStyle(
                      fontSize: AppTypography.xxl,
                      fontWeight: AppTypography.bold,
                      color: AppColors.textPrimary,
                    ),
                  ),
                  const SizedBox(height: AppSpacing.sm),
                  Text(
                    'Livros disponíveis de outros usuários',
                    style: TextStyle(
                      fontSize: AppTypography.md,
                      color: AppColors.textSecondary,
                    ),
                  ),
                ],
              ),
            ),

            // Search and Filters
            Container(
              padding: const EdgeInsets.symmetric(horizontal: AppSpacing.lg),
              child: Column(
                children: [
                                     // Search Bar
                   CustomSearchBar(
                     onChanged: (value) {
                       setState(() {
                         _searchQuery = value;
                       });
                       // Debounce search
                       Future.delayed(const Duration(milliseconds: 500), () {
                         if (mounted) {
                           _loadBooks();
                         }
                       });
                     },
                   ),
                  
                  const SizedBox(height: AppSpacing.md),
                  
                  // Filters
                  SingleChildScrollView(
                    scrollDirection: Axis.horizontal,
                    child: Row(
                      children: [
                                                 custom.CustomFilterChip(
                           label: const Text('Disponível'),
                           selected: _selectedStatuses.contains(1),
                           onSelected: (selected) {
                             setState(() {
                               if (selected) {
                                 _selectedStatuses.add(1);
                               } else {
                                 _selectedStatuses.remove(1);
                               }
                             });
                             _loadBooks();
                           },
                         ),
                        const SizedBox(width: AppSpacing.sm),
                                                 custom.CustomFilterChip(
                           label: const Text('Emprestado'),
                           selected: _selectedStatuses.contains(2),
                           onSelected: (selected) {
                             setState(() {
                               if (selected) {
                                 _selectedStatuses.add(2);
                               } else {
                                 _selectedStatuses.remove(2);
                               }
                             });
                             _loadBooks();
                           },
                         ),
                        const SizedBox(width: AppSpacing.sm),
                                                 custom.CustomFilterChip(
                           label: const Text('Reservado'),
                           selected: _selectedStatuses.contains(3),
                           onSelected: (selected) {
                             setState(() {
                               if (selected) {
                                 _selectedStatuses.add(3);
                               } else {
                                 _selectedStatuses.remove(3);
                               }
                             });
                             _loadBooks();
                           },
                         ),
                      ],
                    ),
                  ),
                ],
              ),
            ),

            const SizedBox(height: AppSpacing.md),

            // Books List
            Expanded(
              child: _isLoading
                  ? const Center(
                      child: CircularProgressIndicator(),
                    )
                  : _error != null
                      ? Center(
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: [
                              Icon(
                                Icons.error_outline,
                                size: 64,
                                color: AppColors.error,
                              ),
                              const SizedBox(height: AppSpacing.md),
                              Text(
                                _error!,
                                style: TextStyle(
                                  fontSize: AppTypography.lg,
                                  color: AppColors.error,
                                ),
                                textAlign: TextAlign.center,
                              ),
                              const SizedBox(height: AppSpacing.md),
                              ElevatedButton(
                                onPressed: _loadBooks,
                                child: const Text('Tentar novamente'),
                              ),
                            ],
                          ),
                        )
                      : _books.isEmpty
                          ? Center(
                              child: Column(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: [
                                  Icon(
                                    Icons.search_off,
                                    size: 64,
                                    color: AppColors.textSecondary,
                                  ),
                                  const SizedBox(height: AppSpacing.md),
                                  Text(
                                    'Nenhum livro encontrado',
                                    style: TextStyle(
                                      fontSize: AppTypography.lg,
                                      color: AppColors.textSecondary,
                                    ),
                                  ),
                                ],
                              ),
                            )
                          : RefreshIndicator(
                              onRefresh: _refreshBooks,
                              child: ListView.builder(
                                padding: const EdgeInsets.symmetric(horizontal: AppSpacing.lg),
                                itemCount: _books.length,
                                itemBuilder: (context, index) {
                                  final book = _books[index];
                                  return BookCard(
                                    book: book,
                                    onTap: () {
                                      // Navigate to book details
                                      ScaffoldMessenger.of(context).showSnackBar(
                                        SnackBar(
                                          content: Text('Visualizando: ${book.name}'),
                                          backgroundColor: AppColors.primary,
                                        ),
                                      );
                                    },
                                  );
                                },
                              ),
                            ),
            ),
          ],
        ),
      ),
    );
  }
} 