import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';
import '../constants/app_constants.dart';
import '../providers/books_provider.dart';
import '../providers/auth_provider.dart';
import '../models/book.dart';
import '../models/enums.dart';
import '../widgets/custom_text_field.dart';
import '../widgets/custom_button.dart';

class QuickStatusScreen extends StatefulWidget {
  const QuickStatusScreen({super.key});

  @override
  State<QuickStatusScreen> createState() => _QuickStatusScreenState();
}

class _QuickStatusScreenState extends State<QuickStatusScreen> {
  final _searchController = TextEditingController();
  List<Book> _filteredBooks = [];
  Book? _selectedBook;
  EnumLoanStatus? _selectedStatus;
  DateTime? _selectedDate;
  bool _isUpdating = false;

  @override
  void initState() {
    super.initState();
    _loadUserBooks();
  }

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  void _loadUserBooks() {
    final currentUser = context.read<AuthProvider>().currentUser;
    if (currentUser != null) {
      final booksProvider = context.read<BooksProvider>();
      _filteredBooks = booksProvider.getBooksByOwner(currentUser.userId!);
    }
  }

  void _filterBooks(String query) {
    if (query.isEmpty) {
      _loadUserBooks();
    } else {
      final booksProvider = context.read<BooksProvider>();
      final allBooks = booksProvider.getBooksByOwner(
        context.read<AuthProvider>().currentUser!.userId!,
      );
      _filteredBooks = allBooks.where((book) {
        return book.name.toLowerCase().contains(query.toLowerCase()) ||
               book.authorDisplay.toLowerCase().contains(query.toLowerCase());
      }).toList();
    }
    setState(() {});
  }

  void _showUpdateDialog(Book book) {
    _selectedBook = book;
    _selectedStatus = book.currentLoanStatus;
    _selectedDate = book.dueDate;

    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Update Book Status'),
        content: SingleChildScrollView(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Text(
                book.name,
                style: AppConstants.subheadingStyle,
                textAlign: TextAlign.center,
              ),
              const SizedBox(height: AppConstants.paddingMedium),
              
              // Status Selection
              DropdownButtonFormField<EnumLoanStatus?>(
                value: _selectedStatus,
                decoration: const InputDecoration(
                  labelText: 'Current Status',
                  border: OutlineInputBorder(),
                ),
                items: [
                  const DropdownMenuItem(
                    value: null,
                    child: Text('Available'),
                  ),
                  ...EnumLoanStatus.values.map((status) => DropdownMenuItem(
                    value: status,
                    child: Text(status.displayName),
                  )),
                ],
                onChanged: (value) {
                  setState(() {
                    _selectedStatus = value;
                  });
                },
              ),
              const SizedBox(height: AppConstants.paddingMedium),

              // Date Selection
              if (_selectedStatus == EnumLoanStatus.received) ...[
                InkWell(
                  onTap: () async {
                    final date = await showDatePicker(
                      context: context,
                      initialDate: _selectedDate ?? DateTime.now().add(const Duration(days: 30)),
                      firstDate: DateTime.now(),
                      lastDate: DateTime.now().add(const Duration(days: 365)),
                    );
                    if (date != null) {
                      setState(() {
                        _selectedDate = date;
                      });
                    }
                  },
                  child: Container(
                    padding: const EdgeInsets.symmetric(
                      horizontal: AppConstants.paddingMedium,
                      vertical: AppConstants.paddingMedium,
                    ),
                    decoration: BoxDecoration(
                      border: Border.all(color: Colors.grey),
                      borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
                    ),
                    child: Row(
                      children: [
                        const Icon(Icons.calendar_today, color: Colors.grey),
                        const SizedBox(width: AppConstants.paddingMedium),
                        Expanded(
                          child: Text(
                            _selectedDate != null
                                ? DateFormat(AppConstants.defaultDateFormat).format(_selectedDate!)
                                : 'Select Due Date',
                            style: TextStyle(
                              color: _selectedDate != null ? Colors.black : Colors.grey,
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
                const SizedBox(height: AppConstants.paddingMedium),
              ],

              // Notes
              CustomTextField(
                labelText: 'Notes (Optional)',
                maxLines: 3,
                hintText: 'Add any additional notes about this status update...',
              ),
            ],
          ),
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          ElevatedButton(
            onPressed: _isUpdating ? null : _updateBookStatus,
            child: _isUpdating
                ? const SizedBox(
                    width: 16,
                    height: 16,
                    child: CircularProgressIndicator(strokeWidth: 2),
                  )
                : const Text('Update'),
          ),
        ],
      ),
    );
  }

  Future<void> _updateBookStatus() async {
    if (_selectedBook == null) return;

    setState(() {
      _isUpdating = true;
    });

    try {
      // TODO: Implement status update
      await Future.delayed(const Duration(seconds: 2)); // Simulate API call
      
      if (mounted) {
        Navigator.pop(context);
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('Book status updated successfully!'),
            backgroundColor: AppConstants.successColor,
          ),
        );
        
        // Refresh the books list
        _loadUserBooks();
        setState(() {});
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Error updating status: $e'),
            backgroundColor: AppConstants.errorColor,
          ),
        );
      }
    } finally {
      if (mounted) {
        setState(() {
          _isUpdating = false;
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Quick Status Update'),
        backgroundColor: Colors.transparent,
        elevation: 0,
      ),
      body: Column(
        children: [
          // Header
          Container(
            width: double.infinity,
            padding: const EdgeInsets.all(AppConstants.paddingLarge),
            decoration: BoxDecoration(
              gradient: const LinearGradient(
                colors: [AppConstants.primaryColor, AppConstants.secondaryColor],
              ),
            ),
            child: Column(
              children: [
                const Icon(
                  Icons.speed,
                  size: 48,
                  color: Colors.white,
                ),
                const SizedBox(height: AppConstants.paddingMedium),
                Text(
                  'Quick Status Update',
                  style: AppConstants.headingStyle.copyWith(
                    color: Colors.white,
                    fontSize: 24,
                  ),
                  textAlign: TextAlign.center,
                ),
                const SizedBox(height: AppConstants.paddingSmall),
                Text(
                  'Update book status when email notifications fail',
                  style: AppConstants.captionStyle.copyWith(
                    color: Colors.white70,
                  ),
                  textAlign: TextAlign.center,
                ),
              ],
            ),
          ),

          // Search Bar
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
            child: TextField(
              controller: _searchController,
              decoration: InputDecoration(
                hintText: 'Search your books...',
                prefixIcon: const Icon(Icons.search),
                suffixIcon: _searchController.text.isNotEmpty
                    ? IconButton(
                        icon: const Icon(Icons.clear),
                        onPressed: () {
                          _searchController.clear();
                          _filterBooks('');
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
              onChanged: _filterBooks,
            ),
          ),

          // Books List
          Expanded(
            child: _filteredBooks.isEmpty
                ? Center(
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
                          'Add some books to your collection first',
                          style: AppConstants.captionStyle,
                        ),
                      ],
                    ),
                  )
                : ListView.builder(
                    padding: const EdgeInsets.all(AppConstants.paddingMedium),
                    itemCount: _filteredBooks.length,
                    itemBuilder: (context, index) {
                      final book = _filteredBooks[index];
                      return Card(
                        margin: const EdgeInsets.only(bottom: AppConstants.paddingMedium),
                        child: ListTile(
                          leading: CircleAvatar(
                            backgroundColor: _getStatusColor(book).withOpacity(0.1),
                            child: Icon(
                              Icons.book,
                              color: _getStatusColor(book),
                            ),
                          ),
                          title: Text(
                            book.name,
                            style: AppConstants.subheadingStyle.copyWith(fontSize: 16),
                          ),
                          subtitle: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(book.authorDisplay),
                              const SizedBox(height: AppConstants.paddingSmall),
                              Row(
                                children: [
                                  Container(
                                    width: 8,
                                    height: 8,
                                    decoration: BoxDecoration(
                                      color: _getStatusColor(book),
                                      shape: BoxShape.circle,
                                    ),
                                  ),
                                  const SizedBox(width: AppConstants.paddingSmall),
                                  Text(
                                    book.statusDisplay,
                                    style: AppConstants.captionStyle.copyWith(
                                      color: _getStatusColor(book),
                                      fontWeight: FontWeight.w500,
                                    ),
                                  ),
                                ],
                              ),
                              if (book.dueDate != null) ...[
                                const SizedBox(height: AppConstants.paddingSmall),
                                Text(
                                  'Due: ${DateFormat(AppConstants.defaultDateFormat).format(book.dueDate!)}',
                                  style: AppConstants.captionStyle.copyWith(
                                    color: book.isOverdue ? AppConstants.errorColor : null,
                                    fontWeight: book.isOverdue ? FontWeight.bold : null,
                                  ),
                                ),
                              ],
                            ],
                          ),
                          trailing: IconButton(
                            icon: const Icon(Icons.edit),
                            onPressed: () => _showUpdateDialog(book),
                          ),
                        ),
                      );
                    },
                  ),
          ),
        ],
      ),
    );
  }

  Color _getStatusColor(Book book) {
    if (!book.isAvailable) return Colors.grey;
    if (book.isOverdue) return AppConstants.errorColor;
    if (book.isLent) return AppConstants.warningColor;
    return AppConstants.successColor;
  }
} 