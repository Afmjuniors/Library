import 'package:flutter/material.dart';
import '../../core/constants/app_colors.dart';
import '../../core/constants/app_spacing.dart';
import '../../core/constants/app_typography.dart';
import '../../core/constants/app_radius.dart';

class CustomFilterChip extends StatelessWidget {
  final Widget label;
  final bool selected;
  final void Function(bool)? onSelected;

  const CustomFilterChip({
    super.key,
    required this.label,
    required this.selected,
    this.onSelected,
  });

  @override
  Widget build(BuildContext context) {
    return FilterChip(
      label: label,
      selected: selected,
      onSelected: onSelected,
      backgroundColor: AppColors.white,
      selectedColor: AppColors.primary.withOpacity(0.2),
      checkmarkColor: AppColors.primary,
      labelStyle: TextStyle(
        color: selected ? AppColors.primary : AppColors.textSecondary,
        fontSize: AppTypography.sm,
        fontWeight: selected ? AppTypography.semibold : AppTypography.normal,
      ),
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(AppRadius.round),
        side: BorderSide(
          color: selected ? AppColors.primary : AppColors.gray300,
        ),
      ),
      padding: const EdgeInsets.symmetric(
        horizontal: AppSpacing.sm,
        vertical: AppSpacing.xs,
      ),
    );
  }
} 