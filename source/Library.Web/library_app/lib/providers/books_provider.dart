import 'package:flutter/material.dart';
import '../models/book.dart';
import '../models/search_params.dart';
import '../services/api_service.dart';
import '../constants/app_constants.dart';

class BooksProvider extends ChangeNotifier {
  final ApiService _apiService = ApiService();
  
  List<Book> _books = [];
  List<Book> _filteredBooks = [];
  Book? _selectedBook;
  bool _isLoading = false;
  String? _error;
  SearchBookParams _searchParams = SearchBookParams();
  int _currentPage = 1;
  bool _hasMorePages = true;

  List<Book> get books => _books;
  List<Book> get filteredBooks => _filteredBooks;
  Book? get selectedBook => _selectedBook;
  bool get isLoading => _isLoading;
  String? get error => _error;
  SearchBookParams get searchParams => _searchParams;
  bool get hasMorePages => _hasMorePages;

  void _setLoading(bool loading) {
    _isLoading = loading;
    notifyListeners();
  }

  void _setError(String? error) {
    _error = error;
    notifyListeners();
  }

  void _setBooks(List<Book> books) {
    _books = books;
    _applyFilters();
    notifyListeners();
  }

  void _addBooks(List<Book> books) {
    _books.addAll(books);
    _applyFilters();
    notifyListeners();
  }

  void _setSelectedBook(Book? book) {
    _selectedBook = book;
    notifyListeners();
  }

  void _applyFilters() {
    _filteredBooks = _books.where((book) {
      // Apply search filters
      if (_searchParams.keyWord?.isNotEmpty == true) {
        final keyword = _searchParams.keyWord!.toLowerCase();
        if (!book.name.toLowerCase().contains(keyword) &&
            !book.authorDisplay.toLowerCase().contains(keyword) &&
            !book.genreDisplay.toLowerCase().contains(keyword)) {
          return false;
        }
      }

      if (_searchParams.ownerId != null && book.ownerId != _searchParams.ownerId) {
        return false;
      }

      if (_searchParams.author?.isNotEmpty == true) {
        if (!book.authorDisplay.toLowerCase().contains(_searchParams.author!.toLowerCase())) {
          return false;
        }
      }

      if (_searchParams.genre != null && book.genre != _searchParams.genre) {
        return false;
      }

      if (_searchParams.bookStatus != null && book.bookStatusId != _searchParams.bookStatus) {
        return false;
      }

      if (_searchParams.loanStatus != null && book.currentLoanStatus != _searchParams.loanStatus) {
        return false;
      }

      return true;
    }).toList();
  }

  Future<void> loadBooks({bool refresh = false}) async {
    if (refresh) {
      _currentPage = 1;
      _hasMorePages = true;
      _books.clear();
    }

    if (!_hasMorePages || _isLoading) return;

    _setLoading(true);
    _setError(null);

    try {
      final books = await _apiService.getBooks(
        params: _searchParams,
        page: _currentPage,
        pageSize: AppConstants.defaultPageSize,
      );

      if (refresh) {
        _setBooks(books);
      } else {
        _addBooks(books);
      }

      _hasMorePages = books.length == AppConstants.defaultPageSize;
      _currentPage++;
    } catch (e) {
      _setError(e.toString());
    } finally {
      _setLoading(false);
    }
  }

  Future<void> loadBook(int bookId) async {
    _setLoading(true);
    _setError(null);

    try {
      final book = await _apiService.getBook(bookId);
      _setSelectedBook(book);
    } catch (e) {
      _setError(e.toString());
    } finally {
      _setLoading(false);
    }
  }

  Future<bool> createBook(Book book) async {
    _setLoading(true);
    _setError(null);

    try {
      final createdBook = await _apiService.createBook(book);
      _books.insert(0, createdBook);
      _applyFilters();
      return true;
    } catch (e) {
      _setError(e.toString());
      return false;
    } finally {
      _setLoading(false);
    }
  }

  Future<bool> updateBook(int bookId, Book book) async {
    _setLoading(true);
    _setError(null);

    try {
      final updatedBook = await _apiService.updateBook(bookId, book);
      
      final index = _books.indexWhere((b) => b.bookId == bookId);
      if (index != -1) {
        _books[index] = updatedBook;
      }
      
      if (_selectedBook?.bookId == bookId) {
        _setSelectedBook(updatedBook);
      }
      
      _applyFilters();
      return true;
    } catch (e) {
      _setError(e.toString());
      return false;
    } finally {
      _setLoading(false);
    }
  }

  Future<bool> deleteBook(int bookId) async {
    _setLoading(true);
    _setError(null);

    try {
      await _apiService.deleteBook(bookId);
      
      _books.removeWhere((book) => book.bookId == bookId);
      if (_selectedBook?.bookId == bookId) {
        _setSelectedBook(null);
      }
      
      _applyFilters();
      return true;
    } catch (e) {
      _setError(e.toString());
      return false;
    } finally {
      _setLoading(false);
    }
  }

  Future<bool> requestLoan(int bookId) async {
    _setLoading(true);
    _setError(null);

    try {
      // Assuming we have the current user's ID from auth provider
      // You'll need to inject the auth provider or pass the user ID
      await _apiService.requestLoan(bookId, 1); // Replace with actual user ID
      return true;
    } catch (e) {
      _setError(e.toString());
      return false;
    } finally {
      _setLoading(false);
    }
  }

  void updateSearchParams(SearchBookParams params) {
    _searchParams = params;
    _currentPage = 1;
    _hasMorePages = true;
    _applyFilters();
    notifyListeners();
  }

  void clearFilters() {
    _searchParams = SearchBookParams();
    _currentPage = 1;
    _hasMorePages = true;
    _applyFilters();
    notifyListeners();
  }

  void clearError() {
    _setError(null);
  }

  List<Book> getBooksByOwner(int ownerId) {
    return _books.where((book) => book.ownerId == ownerId).toList();
  }

  List<Book> getAvailableBooks() {
    return _books.where((book) => book.isAvailable).toList();
  }

  List<Book> getLentBooks() {
    return _books.where((book) => book.isLent).toList();
  }

  List<Book> getOverdueBooks() {
    return _books.where((book) => book.isOverdue).toList();
  }
} 