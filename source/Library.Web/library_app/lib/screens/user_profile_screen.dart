import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:intl/intl.dart';
import '../constants/app_constants.dart';
import '../providers/auth_provider.dart';
import '../providers/books_provider.dart';
import '../models/user.dart';
import '../models/book.dart';
import '../models/enums.dart';
import '../widgets/book_card.dart';

class UserProfileScreen extends StatefulWidget {
  final int userId;

  const UserProfileScreen({super.key, required this.userId});

  @override
  State<UserProfileScreen> createState() => _UserProfileScreenState();
}

class _UserProfileScreenState extends State<UserProfileScreen>
    with SingleTickerProviderStateMixin {
  late TabController _tabController;
  User? _user;
  List<Book> _lentBooks = [];
  List<Book> _borrowedBooks = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 3, vsync: this);
    _loadUserData();
  }

  @override
  void dispose() {
    _tabController.dispose();
    super.dispose();
  }

  Future<void> _loadUserData() async {
    setState(() {
      _isLoading = true;
    });

    try {
      // TODO: Load user data from API
      // For now, we'll use mock data
      await Future.delayed(const Duration(seconds: 1));
      
      // Mock user data
      _user = User(
        userId: widget.userId,
        name: 'John Doe',
        email: 'john.doe@example.com',
        phone: '+1 (555) 123-4567',
        address: '123 Main St, City, State 12345',
        additionalInfo: 'Book enthusiast and avid reader',
        birthDay: DateTime(1990, 5, 15),
        role: EnumUserRole.normal,
      );

      // Load user's books
      final booksProvider = context.read<BooksProvider>();
      _lentBooks = booksProvider.getBooksByOwner(widget.userId);
      _borrowedBooks = []; // TODO: Load borrowed books from API

      setState(() {
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _isLoading = false;
      });
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Error loading user data: $e'),
            backgroundColor: AppConstants.errorColor,
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final currentUser = context.read<AuthProvider>().currentUser;
    final isOwnProfile = currentUser?.userId == widget.userId;
    final canViewPrivateInfo = isOwnProfile || 
                              currentUser?.isAdmin == true || 
                              currentUser?.isLeader == true;

    return Scaffold(
      appBar: AppBar(
        title: Text(isOwnProfile ? 'My Profile' : 'User Profile'),
        backgroundColor: Colors.transparent,
        elevation: 0,
        actions: [
          if (isOwnProfile)
            IconButton(
              icon: const Icon(Icons.edit),
              onPressed: () {
                // TODO: Navigate to edit profile screen
              },
            ),
        ],
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _user == null
              ? const Center(child: Text('User not found'))
              : CustomScrollView(
                  slivers: [
                    // User Header
                    SliverToBoxAdapter(
                      child: Container(
                        padding: const EdgeInsets.all(AppConstants.paddingLarge),
                        decoration: BoxDecoration(
                          gradient: const LinearGradient(
                            colors: [AppConstants.primaryColor, AppConstants.secondaryColor],
                          ),
                        ),
                        child: Column(
                          children: [
                            // Profile Image
                            CircleAvatar(
                              radius: 50,
                              backgroundColor: Colors.white,
                              child: _user!.image?.isNotEmpty == true
                                  ? ClipOval(
                                      child:                                       CachedNetworkImage(
                                        imageUrl: _user!.image!,
                                        width: 100,
                                        height: 100,
                                        fit: BoxFit.cover,
                                      ),
                                    )
                                  : Text(
                                      _user!.initials,
                                      style: const TextStyle(
                                        fontSize: 32,
                                        fontWeight: FontWeight.bold,
                                        color: AppConstants.primaryColor,
                                      ),
                                    ),
                            ),
                            const SizedBox(height: AppConstants.paddingMedium),

                            // User Name
                            Text(
                              _user!.displayName,
                              style: AppConstants.headingStyle.copyWith(
                                color: Colors.white,
                                fontSize: 24,
                              ),
                              textAlign: TextAlign.center,
                            ),
                            const SizedBox(height: AppConstants.paddingSmall),

                            // Role Badge
                            Container(
                              padding: const EdgeInsets.symmetric(
                                horizontal: AppConstants.paddingMedium,
                                vertical: AppConstants.paddingSmall,
                              ),
                              decoration: BoxDecoration(
                                color: Colors.white.withOpacity(0.2),
                                borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
                              ),
                              child: Text(
                                _user!.role?.displayName ?? 'Member',
                                style: AppConstants.captionStyle.copyWith(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ),

                    // User Information
                    SliverToBoxAdapter(
                      child: Padding(
                        padding: const EdgeInsets.all(AppConstants.paddingLarge),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            // Public Information
                            _buildInfoSection(
                              title: 'Public Information',
                              items: [
                                _buildInfoItem('Name', _user!.displayName),
                                _buildInfoItem('Email', _user!.email),
                              ],
                            ),
                            const SizedBox(height: AppConstants.paddingLarge),

                            // Private Information (if allowed)
                            if (canViewPrivateInfo) ...[
                              _buildInfoSection(
                                title: 'Contact Information',
                                items: [
                                  if (_user!.phone?.isNotEmpty == true)
                                    _buildInfoItem('Phone', _user!.phone!),
                                  if (_user!.address?.isNotEmpty == true)
                                    _buildInfoItem('Address', _user!.address!),
                                  if (_user!.birthDay != null)
                                    _buildInfoItem(
                                      'Birth Date',
                                      DateFormat(AppConstants.defaultDateFormat).format(_user!.birthDay!),
                                    ),
                                ],
                              ),
                              const SizedBox(height: AppConstants.paddingLarge),

                              if (_user!.additionalInfo?.isNotEmpty == true) ...[
                                _buildInfoSection(
                                  title: 'Additional Information',
                                  items: [
                                    _buildInfoItem('About', _user!.additionalInfo!),
                                  ],
                                ),
                                const SizedBox(height: AppConstants.paddingLarge),
                              ],
                            ],

                            // Statistics
                            _buildInfoSection(
                              title: 'Statistics',
                              items: [
                                _buildInfoItem('Books Lent', _lentBooks.length.toString()),
                                _buildInfoItem('Books Borrowed', _borrowedBooks.length.toString()),
                                _buildInfoItem('Member Since', _user!.createdAt != null
                                    ? DateFormat(AppConstants.defaultDateFormat).format(_user!.createdAt!)
                                    : 'Unknown'),
                              ],
                            ),
                          ],
                        ),
                      ),
                    ),

                    // Tabs
                    SliverPersistentHeader(
                      pinned: true,
                      delegate: _SliverAppBarDelegate(
                        TabBar(
                          controller: _tabController,
                          labelColor: AppConstants.primaryColor,
                          unselectedLabelColor: Colors.grey,
                          indicatorColor: AppConstants.primaryColor,
                          tabs: const [
                            Tab(text: 'Books Lent'),
                            Tab(text: 'Books Borrowed'),
                            Tab(text: 'Queue'),
                          ],
                        ),
                      ),
                    ),

                    // Tab Content
                    SliverFillRemaining(
                      child: TabBarView(
                        controller: _tabController,
                        children: [
                          _buildBooksList(_lentBooks, 'No books lent yet'),
                          _buildBooksList(_borrowedBooks, 'No books borrowed yet'),
                          _buildQueueList(),
                        ],
                      ),
                    ),
                  ],
                ),
    );
  }

  Widget _buildInfoSection({
    required String title,
    required List<Widget> items,
  }) {
    return Container(
      padding: const EdgeInsets.all(AppConstants.paddingLarge),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.1),
            spreadRadius: 1,
            blurRadius: 3,
            offset: const Offset(0, 1),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            title,
            style: AppConstants.subheadingStyle,
          ),
          const SizedBox(height: AppConstants.paddingMedium),
          ...items,
        ],
      ),
    );
  }

  Widget _buildInfoItem(String label, String value) {
    return Padding(
      padding: const EdgeInsets.only(bottom: AppConstants.paddingSmall),
      child: Row(
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
              style: AppConstants.bodyStyle,
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildBooksList(List<Book> books, String emptyMessage) {
    if (books.isEmpty) {
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
              emptyMessage,
              style: AppConstants.subheadingStyle,
            ),
          ],
        ),
      );
    }

    return ListView.builder(
      padding: const EdgeInsets.all(AppConstants.paddingMedium),
      itemCount: books.length,
      itemBuilder: (context, index) {
        final book = books[index];
        return Padding(
          padding: const EdgeInsets.only(bottom: AppConstants.paddingMedium),
          child: BookCard(
            book: book,
            isListView: true,
          ),
        );
      },
    );
  }

  Widget _buildQueueList() {
    // TODO: Implement queue list
    return const Center(
      child: Text('Queue functionality coming soon'),
    );
  }
}

class _SliverAppBarDelegate extends SliverPersistentHeaderDelegate {
  final TabBar _tabBar;

  _SliverAppBarDelegate(this._tabBar);

  @override
  double get minExtent => _tabBar.preferredSize.height;

  @override
  double get maxExtent => _tabBar.preferredSize.height;

  @override
  Widget build(BuildContext context, double shrinkOffset, bool overlapsContent) {
    return Container(
      color: Colors.white,
      child: _tabBar,
    );
  }

  @override
  bool shouldRebuild(_SliverAppBarDelegate oldDelegate) {
    return false;
  }
} 