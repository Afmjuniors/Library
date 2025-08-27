import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../core/constants/app_colors.dart';
import '../../core/constants/app_spacing.dart';
import '../../core/constants/app_typography.dart';
import '../../core/constants/app_radius.dart';
import '../../core/constants/app_shadows.dart';
import '../../data/models/organization_model.dart';

class OrganizationCard extends StatelessWidget {
  final ExtendedOrganizationModel organization;
  final VoidCallback? onTap;

  const OrganizationCard({
    super.key,
    required this.organization,
    this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(bottom: AppSpacing.md),
      decoration: BoxDecoration(
        color: AppColors.white,
        borderRadius: BorderRadius.circular(AppRadius.lg),
        boxShadow: [AppShadows.small],
      ),
      child: Material(
        color: Colors.transparent,
        child: InkWell(
          onTap: onTap,
          borderRadius: BorderRadius.circular(AppRadius.lg),
          child: Padding(
            padding: const EdgeInsets.all(AppSpacing.md),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Header
                Row(
                  children: [
                    // Organization Image
                    Container(
                      width: 60,
                      height: 60,
                      decoration: BoxDecoration(
                        color: AppColors.gray200,
                        borderRadius: BorderRadius.circular(AppRadius.md),
                      ),
                      child: organization.image.isNotEmpty
                          ? ClipRRect(
                              borderRadius: BorderRadius.circular(AppRadius.md),
                              child: CachedNetworkImage(
                                imageUrl: organization.image,
                                fit: BoxFit.cover,
                                placeholder: (context, url) => const Center(
                                  child: Icon(
                                    Icons.business,
                                    color: AppColors.gray400,
                                  ),
                                ),
                                errorWidget: (context, url, error) => const Center(
                                  child: Icon(
                                    Icons.business,
                                    color: AppColors.gray400,
                                  ),
                                ),
                              ),
                            )
                          : const Center(
                              child: Icon(
                                Icons.business,
                                color: AppColors.gray400,
                              ),
                            ),
                    ),
                    
                    const SizedBox(width: AppSpacing.md),
                    
                    // Organization Info
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            organization.name,
                            style: TextStyle(
                              fontSize: AppTypography.lg,
                              fontWeight: AppTypography.bold,
                              color: AppColors.textPrimary,
                            ),
                          ),
                          const SizedBox(height: AppSpacing.xs),
                          Text(
                            organization.description,
                            style: TextStyle(
                              fontSize: AppTypography.sm,
                              color: AppColors.textSecondary,
                            ),
                            maxLines: 2,
                            overflow: TextOverflow.ellipsis,
                          ),
                          const SizedBox(height: AppSpacing.sm),
                          Row(
                            children: [
                              Container(
                                padding: const EdgeInsets.symmetric(
                                  horizontal: AppSpacing.sm,
                                  vertical: AppSpacing.xs,
                                ),
                                decoration: BoxDecoration(
                                  color: AppColors.primary.withOpacity(0.1),
                                  borderRadius: BorderRadius.circular(AppRadius.sm),
                                ),
                                child: Text(
                                  organization.role,
                                  style: TextStyle(
                                    fontSize: AppTypography.xs,
                                    color: AppColors.primary,
                                    fontWeight: AppTypography.semibold,
                                  ),
                                ),
                              ),
                              const SizedBox(width: AppSpacing.sm),
                              Text(
                                '${organization.memberCount} membros',
                                style: TextStyle(
                                  fontSize: AppTypography.xs,
                                  color: AppColors.textSecondary,
                                ),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
                
                const SizedBox(height: AppSpacing.md),
                
                // Stats
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceAround,
                  children: [
                    _buildStatItem(
                      '${organization.stats.totalBooks}',
                      'Livros',
                    ),
                    Container(
                      width: 1,
                      height: 30,
                      color: AppColors.gray300,
                    ),
                    _buildStatItem(
                      '${organization.stats.activeLoans}',
                      'Empréstimos',
                    ),
                    Container(
                      width: 1,
                      height: 30,
                      color: AppColors.gray300,
                    ),
                    _buildStatItem(
                      '${organization.stats.monthlyLoans}',
                      'Este Mês',
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildStatItem(String value, String label) {
    return Column(
      children: [
        Text(
          value,
          style: TextStyle(
            fontSize: AppTypography.lg,
            fontWeight: AppTypography.bold,
            color: AppColors.primary,
          ),
        ),
        const SizedBox(height: AppSpacing.xs),
        Text(
          label,
          style: TextStyle(
            fontSize: AppTypography.xs,
            color: AppColors.textSecondary,
          ),
        ),
      ],
    );
  }
} 