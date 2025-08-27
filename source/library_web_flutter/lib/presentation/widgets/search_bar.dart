import 'package:flutter/material.dart';
import '../../core/constants/app_colors.dart';
import '../../core/constants/app_spacing.dart';
import '../../core/constants/app_typography.dart';
import '../../core/constants/app_radius.dart';

class CustomSearchBar extends StatelessWidget {
  final String? hint;
  final void Function(String)? onChanged;
  final TextEditingController? controller;

  const CustomSearchBar({
    super.key,
    this.hint,
    this.onChanged,
    this.controller,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: AppColors.white,
        borderRadius: BorderRadius.circular(AppRadius.md),
        border: Border.all(color: AppColors.gray300),
      ),
      child: TextField(
        controller: controller,
        onChanged: onChanged,
        decoration: InputDecoration(
          hintText: hint ?? 'Pesquisar livros...',
          hintStyle: TextStyle(
            color: AppColors.textSecondary,
            fontSize: AppTypography.md,
          ),
          prefixIcon: const Icon(
            Icons.search,
            color: AppColors.textSecondary,
          ),
          border: InputBorder.none,
          contentPadding: const EdgeInsets.symmetric(
            horizontal: AppSpacing.md,
            vertical: AppSpacing.md,
          ),
        ),
        style: TextStyle(
          fontSize: AppTypography.md,
          color: AppColors.textPrimary,
        ),
      ),
    );
  }
} 