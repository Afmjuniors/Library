import 'package:flutter/material.dart';
import '../../../core/constants/app_colors.dart';
import '../../../core/constants/app_typography.dart';
import '../../../data/models/user_model.dart';
import 'books_tab.dart';
import 'organizations_tab.dart';
import 'profile_tab.dart';

class MainScreen extends StatefulWidget {
  final UserModel user;

  const MainScreen({
    super.key,
    required this.user,
  });

  @override
  State<MainScreen> createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  int _currentIndex = 0;

  final List<Widget> _tabs = [
    const BooksTab(),
    const OrganizationsTab(),
    const ProfileTab(),
  ];

  final List<BottomNavigationBarItem> _navigationItems = [
    const BottomNavigationBarItem(
      icon: Icon(Icons.book),
      label: 'Livros',
    ),
    const BottomNavigationBarItem(
      icon: Icon(Icons.business),
      label: 'Organizações',
    ),
    const BottomNavigationBarItem(
      icon: Icon(Icons.person),
      label: 'Perfil',
    ),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppColors.background,
      body: IndexedStack(
        index: _currentIndex,
        children: _tabs,
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _currentIndex,
        onTap: (index) {
          setState(() {
            _currentIndex = index;
          });
        },
        type: BottomNavigationBarType.fixed,
        backgroundColor: AppColors.white,
        selectedItemColor: AppColors.primary,
        unselectedItemColor: AppColors.textSecondary,
        selectedLabelStyle: TextStyle(
          fontSize: AppTypography.sm,
          fontWeight: AppTypography.semibold,
        ),
        unselectedLabelStyle: TextStyle(
          fontSize: AppTypography.sm,
          fontWeight: AppTypography.normal,
        ),
        items: _navigationItems,
      ),
    );
  }
} 