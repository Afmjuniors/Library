import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../constants/app_constants.dart';
import '../providers/books_provider.dart';
import '../models/search_params.dart';
import '../models/enums.dart';

class FilterDrawer extends StatefulWidget {
  const FilterDrawer({super.key});

  @override
  State<FilterDrawer> createState() => _FilterDrawerState();
}

class _FilterDrawerState extends State<FilterDrawer> {
  late SearchBookParams _filters;
  EnumGenre? _selectedGenre;
  EnumBookStatus? _selectedBookStatus;
  EnumLoanStatus? _selectedLoanStatus;

  @override
  void initState() {
    super.initState();
    final booksProvider = context.read<BooksProvider>();
    _filters = booksProvider.searchParams;
    _selectedGenre = _filters.genre;
    _selectedBookStatus = _filters.bookStatus;
    _selectedLoanStatus = _filters.loanStatus;
  }

  void _applyFilters() {
    final booksProvider = context.read<BooksProvider>();
    final newFilters = _filters.copyWith(
      genre: _selectedGenre,
      bookStatus: _selectedBookStatus,
      loanStatus: _selectedLoanStatus,
    );
    booksProvider.updateSearchParams(newFilters);
    booksProvider.loadBooks(refresh: true);
    Navigator.pop(context);
  }

  void _clearFilters() {
    setState(() {
      _selectedGenre = null;
      _selectedBookStatus = null;
      _selectedLoanStatus = null;
    });
    
    final booksProvider = context.read<BooksProvider>();
    booksProvider.clearFilters();
    Navigator.pop(context);
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      height: MediaQuery.of(context).size.height * 0.8,
      decoration: const BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppConstants.borderRadiusLarge),
          topRight: Radius.circular(AppConstants.borderRadiusLarge),
        ),
      ),
      child: Column(
        children: [
          // Handle
          Container(
            margin: const EdgeInsets.symmetric(vertical: AppConstants.paddingMedium),
            width: 40,
            height: 4,
            decoration: BoxDecoration(
              color: Colors.grey[300],
              borderRadius: BorderRadius.circular(2),
            ),
          ),

          // Header
          Padding(
            padding: const EdgeInsets.all(AppConstants.paddingMedium),
            child: Row(
              children: [
                const Icon(Icons.filter_list),
                const SizedBox(width: AppConstants.paddingSmall),
                Text(
                  'Filter Books',
                  style: AppConstants.subheadingStyle,
                ),
                const Spacer(),
                TextButton(
                  onPressed: _clearFilters,
                  child: const Text('Clear All'),
                ),
              ],
            ),
          ),

          const Divider(),

          // Filter Options
          Expanded(
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(AppConstants.paddingMedium),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  // Genre Filter
                  _buildFilterSection(
                    title: 'Genre',
                    child: Wrap(
                      spacing: AppConstants.paddingSmall,
                      runSpacing: AppConstants.paddingSmall,
                      children: EnumGenre.values.map((genre) {
                        final isSelected = _selectedGenre == genre;
                        return FilterChip(
                          label: Text(genre.displayName),
                          selected: isSelected,
                          onSelected: (selected) {
                            setState(() {
                              _selectedGenre = selected ? genre : null;
                            });
                          },
                          selectedColor: AppConstants.primaryColor.withOpacity(0.2),
                          checkmarkColor: AppConstants.primaryColor,
                        );
                      }).toList(),
                    ),
                  ),

                  const SizedBox(height: AppConstants.paddingLarge),

                  // Book Status Filter
                  _buildFilterSection(
                    title: 'Book Status',
                    child: Column(
                      children: [
                        _buildRadioTile(
                          title: 'Available',
                          value: EnumBookStatus.available,
                          groupValue: _selectedBookStatus,
                          onChanged: (value) {
                            setState(() {
                              _selectedBookStatus = value;
                            });
                          },
                        ),
                        _buildRadioTile(
                          title: 'Inactive',
                          value: EnumBookStatus.inactive,
                          groupValue: _selectedBookStatus,
                          onChanged: (value) {
                            setState(() {
                              _selectedBookStatus = value;
                            });
                          },
                        ),
                      ],
                    ),
                  ),

                  const SizedBox(height: AppConstants.paddingLarge),

                  // Loan Status Filter
                  _buildFilterSection(
                    title: 'Loan Status',
                    child: Column(
                      children: [
                        _buildRadioTile(
                          title: 'Available',
                          value: null,
                          groupValue: _selectedLoanStatus,
                          onChanged: (value) {
                            setState(() {
                              _selectedLoanStatus = value;
                            });
                          },
                        ),
                        _buildRadioTile(
                          title: 'Lent',
                          value: EnumLoanStatus.received,
                          groupValue: _selectedLoanStatus,
                          onChanged: (value) {
                            setState(() {
                              _selectedLoanStatus = value;
                            });
                          },
                        ),
                        _buildRadioTile(
                          title: 'Overdue',
                          value: EnumLoanStatus.overdue,
                          groupValue: _selectedLoanStatus,
                          onChanged: (value) {
                            setState(() {
                              _selectedLoanStatus = value;
                            });
                          },
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ),

          // Apply Button
          Container(
            padding: const EdgeInsets.all(AppConstants.paddingMedium),
            decoration: BoxDecoration(
              color: Colors.white,
              boxShadow: [
                BoxShadow(
                  color: Colors.grey.withOpacity(0.1),
                  spreadRadius: 1,
                  blurRadius: 3,
                  offset: const Offset(0, -1),
                ),
              ],
            ),
            child: SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: _applyFilters,
                child: const Text('Apply Filters'),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildFilterSection({
    required String title,
    required Widget child,
  }) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: AppConstants.subheadingStyle.copyWith(fontSize: 16),
        ),
        const SizedBox(height: AppConstants.paddingMedium),
        child,
      ],
    );
  }

  Widget _buildRadioTile<T>({
    required String title,
    required T? value,
    required T? groupValue,
    required ValueChanged<T?> onChanged,
  }) {
    return RadioListTile<T?>(
      title: Text(title),
      value: value,
      groupValue: groupValue,
      onChanged: onChanged,
      contentPadding: EdgeInsets.zero,
      dense: true,
    );
  }
} 