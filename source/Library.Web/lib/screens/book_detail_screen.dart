import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:intl/intl.dart';
import '../constants/app_constants.dart';
import '../providers/books_provider.dart';
import '../providers/auth_provider.dart';
import '../models/book.dart';
import '../models/enums.dart';
import '../widgets/custom_button.dart';

class BookDetailScreen extends StatefulWidget {
  final int bookId;

  const BookDetailScreen({super.key, required this.bookId});

  @override
  State<BookDetailScreen> createState() => _BookDetailScreenState();
}

class _BookDetailScreenState extends State<BookDetailScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<BooksProvider>().loadBook(widget.bookId);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Consumer<BooksProvider>(
        builder: (context, booksProvider, child) {
          final book = booksProvider.selectedBook;

          if (booksProvider.isLoading && book == null) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (booksProvider.error != null && book == null) {
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
                    'Error loading book',
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
                      booksProvider.loadBook(widget.bookId);
                    },
                    child: const Text('Retry'),
                  ),
                ],
              ),
            );
          }

          if (book == null) {
            return const Center(
              child: Text('Book not found'),
            );
          }

          return CustomScrollView(
            slivers: [
              // App Bar with Image
              SliverAppBar(
                expandedHeight: 300,
                pinned: true,
                flexibleSpace: FlexibleSpaceBar(
                  background: book.image?.isNotEmpty == true
                      ? CachedNetworkImage(
                          imageUrl: book.image!,
                          fit: BoxFit.cover,
                          placeholder: (context, url) => Container(
                            color: Colors.grey[200],
                            child: const Center(
                              child: CircularProgressIndicator(),
                            ),
                          ),
                          errorWidget: (context, url, error) => Container(
                            color: Colors.grey[200],
                            child: const Icon(
                              Icons.book,
                              size: 64,
                              color: Colors.grey,
                            ),
                          ),
                        )
                      : Container(
                          color: Colors.grey[200],
                          child: const Icon(
                            Icons.book,
                            size: 64,
                            color: Colors.grey,
                          ),
                        ),
                ),
                actions: [
                  IconButton(
                    icon: const Icon(Icons.share),
                    onPressed: () {
                      // TODO: Implement share functionality
                    },
                  ),
                ],
              ),

              // Book Information
              SliverToBoxAdapter(
                child: Padding(
                  padding: const EdgeInsets.all(AppConstants.paddingLarge),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      // Title and Status
                      Row(
                        children: [
                          Expanded(
                            child: Text(
                              book.name,
                              style: AppConstants.headingStyle,
                            ),
                          ),
                          Container(
                            padding: const EdgeInsets.symmetric(
                              horizontal: AppConstants.paddingMedium,
                              vertical: AppConstants.paddingSmall,
                            ),
                            decoration: BoxDecoration(
                              color: _getStatusColor().withOpacity(0.1),
                              borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
                              border: Border.all(color: _getStatusColor()),
                            ),
                            child: Text(
                              book.statusDisplay,
                              style: TextStyle(
                                color: _getStatusColor(),
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: AppConstants.paddingMedium),

                      // Author
                      _buildInfoRow('Author', book.authorDisplay),
                      const SizedBox(height: AppConstants.paddingSmall),

                      // Genre
                      if (book.genre != null) ...[
                        _buildInfoRow('Genre', book.genreDisplay),
                        const SizedBox(height: AppConstants.paddingSmall),
                      ],

                      // Owner
                      if (book.owner != null) ...[
                        _buildInfoRow('Owner', book.owner!.displayName),
                        const SizedBox(height: AppConstants.paddingSmall),
                      ],

                      // Current Borrower
                      if (book.currentBorrower != null) ...[
                        _buildInfoRow('Currently with', book.currentBorrower!.displayName),
                        const SizedBox(height: AppConstants.paddingSmall),
                      ],

                      // Due Date
                      if (book.dueDate != null) ...[
                        _buildInfoRow(
                          'Due Date',
                          DateFormat(AppConstants.defaultDateFormat).format(book.dueDate!),
                          isOverdue: book.isOverdue,
                        ),
                        const SizedBox(height: AppConstants.paddingSmall),
                      ],

                      // Description
                      if (book.description?.isNotEmpty == true) ...[
                        const SizedBox(height: AppConstants.paddingMedium),
                        Text(
                          'Description',
                          style: AppConstants.subheadingStyle,
                        ),
                        const SizedBox(height: AppConstants.paddingSmall),
                        Text(
                          book.description!,
                          style: AppConstants.bodyStyle,
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),
                      ],

                      // Buy Link
                      if (book.url?.isNotEmpty == true) ...[
                        CustomButton(
                          onPressed: () {
                            // TODO: Open URL
                          },
                          child: const Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: [
                              Icon(Icons.shopping_cart),
                              SizedBox(width: AppConstants.paddingSmall),
                              Text('Buy this book'),
                            ],
                          ),
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),
                      ],

                      // Queue Information
                      if (book.hasQueue) ...[
                        Text(
                          'Queue',
                          style: AppConstants.subheadingStyle,
                        ),
                        const SizedBox(height: AppConstants.paddingSmall),
                        Text(
                          '${book.queueList!.length} people waiting',
                          style: AppConstants.captionStyle,
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),
                      ],

                      // Action Buttons
                      _buildActionButtons(book),
                    ],
                  ),
                ),
              ),
            ],
          );
        },
      ),
    );
  }

  Widget _buildInfoRow(String label, String value, {bool isOverdue = false}) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        SizedBox(
          width: 100,
          child: Text(
            label,
            style: AppConstants.captionStyle.copyWith(
              fontWeight: FontWeight.w500,
            ),
          ),
        ),
        Expanded(
          child: Text(
            value,
            style: AppConstants.bodyStyle.copyWith(
              color: isOverdue ? AppConstants.errorColor : null,
              fontWeight: isOverdue ? FontWeight.bold : null,
            ),
          ),
        ),
      ],
    );
  }

  Widget _buildActionButtons(Book book) {
    final currentUser = context.read<AuthProvider>().currentUser;
    final isOwner = currentUser?.userId == book.ownerId;
    final canRequest = !isOwner && book.isAvailable;

    return Column(
      children: [
        if (canRequest) ...[
          CustomButton(
            onPressed: () => _requestLoan(book),
            child: const Text('Request to Borrow'),
          ),
          const SizedBox(height: AppConstants.paddingMedium),
        ],

        if (isOwner) ...[
          CustomButton(
            onPressed: () {
              // TODO: Navigate to edit book screen
            },
            child: const Text('Edit Book'),
          ),
          const SizedBox(height: AppConstants.paddingMedium),
          CustomOutlinedButton(
            onPressed: () => _deleteBook(book),
            child: const Text('Delete Book'),
          ),
        ],

        if (book.currentBorrower?.userId == currentUser?.userId) ...[
          CustomButton(
            onPressed: () => _returnBook(book),
            child: const Text('Return Book'),
          ),
        ],
      ],
    );
  }

  void _requestLoan(Book book) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Request Loan'),
        content: Text('Request to borrow "${book.name}"?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          ElevatedButton(
            onPressed: () async {
              Navigator.pop(context);
              final success = await context.read<BooksProvider>().requestLoan(book.bookId!);
              if (success && mounted) {
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(
                    content: const Text(AppConstants.loanRequestSuccess),
                    backgroundColor: AppConstants.successColor,
                  ),
                );
              }
            },
            child: const Text('Request'),
          ),
        ],
      ),
    );
  }

  void _returnBook(Book book) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Return Book'),
        content: Text('Mark "${book.name}" as returned?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          ElevatedButton(
            onPressed: () async {
              Navigator.pop(context);
              // TODO: Implement return book functionality
              ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(
                  content: const Text(AppConstants.bookReturnedSuccess),
                  backgroundColor: AppConstants.successColor,
                ),
              );
            },
            child: const Text('Return'),
          ),
        ],
      ),
    );
  }

  void _deleteBook(Book book) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Delete Book'),
        content: Text('Are you sure you want to delete "${book.name}"?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          ElevatedButton(
            onPressed: () async {
              Navigator.pop(context);
              final success = await context.read<BooksProvider>().deleteBook(book.bookId!);
              if (success && mounted) {
                Navigator.pop(context);
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(
                    content: const Text(AppConstants.bookDeletedSuccess),
                    backgroundColor: AppConstants.successColor,
                  ),
                );
              }
            },
            style: ElevatedButton.styleFrom(
              backgroundColor: AppConstants.errorColor,
            ),
            child: const Text('Delete'),
          ),
        ],
      ),
    );
  }

  Color _getStatusColor() {
    final book = context.read<BooksProvider>().selectedBook;
    if (book == null) return Colors.grey;
    
    if (!book.isAvailable) return Colors.grey;
    if (book.isOverdue) return AppConstants.errorColor;
    if (book.isLent) return AppConstants.warningColor;
    return AppConstants.successColor;
  }
} 