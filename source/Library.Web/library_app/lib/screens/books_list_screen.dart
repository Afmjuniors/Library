import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter_staggered_grid_view/flutter_staggered_grid_view.dart';
import '../constants/app_constants.dart';
import '../providers/books_provider.dart';
import '../models/book.dart';
import '../models/enums.dart';
import '../widgets/book_card.dart';
import '../widgets/filter_drawer.dart';

class BooksListScreen extends StatefulWidget {
  const BooksListScreen({super.key});

  @override
  State<BooksListScreen> createState() => _BooksListScreenState();
}

class _BooksListScreenState extends State<BooksListScreen> {
  final _searchController = TextEditingController();
  bool _isGridView = true;
  final ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<BooksProvider>().loadBooks(refresh: true);
    });
    
    _scrollController.addListener(_onScroll);
  }

  @override
  void dispose() {
    _searchController.dispose();
    _scrollController.dispose();
    super.dispose();
  }

  void _onScroll() {
    if (_scrollController.position.pixels >= _scrollController.position.maxScrollExtent - 200) {
      context.read<BooksProvider>().loadBooks();
    }
  }

  void _onSearch(String query) {
    final booksProvider = context.read<BooksProvider>();
    final currentParams = booksProvider.searchParams;
    final newParams = currentParams.copyWith(keyWord: query.isEmpty ? null : query);
    booksProvider.updateSearchParams(newParams);
    booksProvider.loadBooks(refresh: true);
  }

  void _showFilterDrawer() {
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      backgroundColor: Colors.transparent,
      builder: (context) => const FilterDrawer(),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          // Search and Filter Bar
          Container(
            padding: const EdgeInsets.all(AppConstants.paddingMedium),
            decoration: BoxDecoration(
              color: Colors.white,
              boxShadow: [
                BoxShadow(
                  color: Colors.grey.withOpacity(0.1),
                  spreadRadius: 1,
                  blurRadius: 3,
                  offset: const Offset(0, 1),
                ),
              ],
            ),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: _searchController,
                    decoration: InputDecoration(
                      hintText: 'Search books...',
                      prefixIcon: const Icon(Icons.search),
                      suffixIcon: _searchController.text.isNotEmpty
                          ? IconButton(
                              icon: const Icon(Icons.clear),
                              onPressed: () {
                                _searchController.clear();
                                _onSearch('');
                              },
                            )
                          : null,
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
                        borderSide: BorderSide.none,
                      ),
                      filled: true,
                      fillColor: Colors.grey[100],
                    ),
                    onChanged: _onSearch,
                  ),
                ),
                const SizedBox(width: AppConstants.paddingSmall),
                IconButton(
                  onPressed: _showFilterDrawer,
                  icon: const Icon(Icons.filter_list),
                  tooltip: 'Filter',
                ),
                IconButton(
                  onPressed: () {
                    setState(() {
                      _isGridView = !_isGridView;
                    });
                  },
                  icon: Icon(_isGridView ? Icons.view_list : Icons.grid_view),
                  tooltip: _isGridView ? 'List View' : 'Grid View',
                ),
              ],
            ),
          ),

          // Books List
          Expanded(
            child: Consumer<BooksProvider>(
              builder: (context, booksProvider, child) {
                if (booksProvider.isLoading && booksProvider.books.isEmpty) {
                  return const Center(
                    child: CircularProgressIndicator(),
                  );
                }

                if (booksProvider.error != null && booksProvider.books.isEmpty) {
                  return Center(
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Icon(
                          Icons.error_outline,
                          size: 64,
                          color: Colors.grey[400],
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),
                        Text(
                          'Error loading books',
                          style: AppConstants.subheadingStyle,
                        ),
                        const SizedBox(height: AppConstants.paddingSmall),
                        Text(
                          booksProvider.error!,
                          style: AppConstants.captionStyle,
                          textAlign: TextAlign.center,
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),
                        ElevatedButton(
                          onPressed: () {
                            booksProvider.clearError();
                            booksProvider.loadBooks(refresh: true);
                          },
                          child: const Text('Retry'),
                        ),
                      ],
                    ),
                  );
                }

                if (booksProvider.filteredBooks.isEmpty) {
                  return Center(
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Icon(
                          Icons.library_books,
                          size: 64,
                          color: Colors.grey[400],
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),
                        Text(
                          'No books found',
                          style: AppConstants.subheadingStyle,
                        ),
                        const SizedBox(height: AppConstants.paddingSmall),
                        Text(
                          booksProvider.searchParams.hasFilters
                              ? 'Try adjusting your filters'
                              : 'Be the first to add a book!',
                          style: AppConstants.captionStyle,
                        ),
                        if (booksProvider.searchParams.hasFilters) ...[
                          const SizedBox(height: AppConstants.paddingMedium),
                          ElevatedButton(
                            onPressed: () {
                              booksProvider.clearFilters();
                              _searchController.clear();
                            },
                            child: const Text('Clear Filters'),
                          ),
                        ],
                      ],
                    ),
                  );
                }

                return RefreshIndicator(
                  onRefresh: () => booksProvider.loadBooks(refresh: true),
                  child: _isGridView ? _buildGridView(booksProvider) : _buildListView(booksProvider),
                );
              },
            ),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          // TODO: Navigate to add book screen
        },
        child: const Icon(Icons.add),
      ),
    );
  }

  Widget _buildGridView(BooksProvider booksProvider) {
    return MasonryGridView.count(
      controller: _scrollController,
      crossAxisCount: 2,
      mainAxisSpacing: AppConstants.paddingSmall,
      crossAxisSpacing: AppConstants.paddingSmall,
      padding: const EdgeInsets.all(AppConstants.paddingMedium),
      itemCount: booksProvider.filteredBooks.length + (booksProvider.hasMorePages ? 1 : 0),
      itemBuilder: (context, index) {
        if (index == booksProvider.filteredBooks.length) {
          return const Center(
            child: Padding(
              padding: EdgeInsets.all(AppConstants.paddingMedium),
              child: CircularProgressIndicator(),
            ),
          );
        }
        
        final book = booksProvider.filteredBooks[index];
        return BookCard(
          book: book,
          onTap: () => context.go('/book/${book.bookId}'),
        );
      },
    );
  }

  Widget _buildListView(BooksProvider booksProvider) {
    return ListView.builder(
      controller: _scrollController,
      padding: const EdgeInsets.all(AppConstants.paddingMedium),
      itemCount: booksProvider.filteredBooks.length + (booksProvider.hasMorePages ? 1 : 0),
      itemBuilder: (context, index) {
        if (index == booksProvider.filteredBooks.length) {
          return const Center(
            child: Padding(
              padding: EdgeInsets.all(AppConstants.paddingMedium),
              child: CircularProgressIndicator(),
            ),
          );
        }
        
        final book = booksProvider.filteredBooks[index];
        return Padding(
          padding: const EdgeInsets.only(bottom: AppConstants.paddingMedium),
          child: BookCard(
            book: book,
            onTap: () => context.go('/book/${book.bookId}'),
            isListView: true,
          ),
        );
      },
    );
  }
} 