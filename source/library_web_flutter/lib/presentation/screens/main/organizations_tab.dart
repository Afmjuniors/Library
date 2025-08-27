import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../../core/constants/app_colors.dart';
import '../../../core/constants/app_spacing.dart';
import '../../../core/constants/app_typography.dart';
import '../../../core/constants/app_radius.dart';
import '../../../core/constants/app_shadows.dart';
import '../../../data/models/organization_model.dart';
import '../../../data/mock_data.dart';
import '../../widgets/organization_card.dart';

class OrganizationsTab extends StatelessWidget {
  const OrganizationsTab({super.key});

  @override
  Widget build(BuildContext context) {
    final organizations = MockData.organizations;

    return Scaffold(
      backgroundColor: AppColors.background,
      body: SafeArea(
        child: Column(
          children: [
            // Header
            Container(
              padding: const EdgeInsets.all(AppSpacing.lg),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Minhas Organizações',
                    style: TextStyle(
                      fontSize: AppTypography.xxl,
                      fontWeight: AppTypography.bold,
                      color: AppColors.textPrimary,
                    ),
                  ),
                  const SizedBox(height: AppSpacing.sm),
                  Text(
                    'Gerencie suas organizações',
                    style: TextStyle(
                      fontSize: AppTypography.md,
                      color: AppColors.textSecondary,
                    ),
                  ),
                ],
              ),
            ),

            // Organizations List
            Expanded(
              child: organizations.isEmpty
                  ? Center(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Icon(
                            Icons.business,
                            size: 64,
                            color: AppColors.textSecondary,
                          ),
                          const SizedBox(height: AppSpacing.md),
                          Text(
                            'Nenhuma organização encontrada',
                            style: TextStyle(
                              fontSize: AppTypography.lg,
                              color: AppColors.textSecondary,
                            ),
                          ),
                          const SizedBox(height: AppSpacing.md),
                          Text(
                            'Crie sua primeira organização para começar',
                            style: TextStyle(
                              fontSize: AppTypography.md,
                              color: AppColors.textSecondary,
                            ),
                            textAlign: TextAlign.center,
                          ),
                        ],
                      ),
                    )
                  : ListView.builder(
                      padding: const EdgeInsets.symmetric(horizontal: AppSpacing.lg),
                      itemCount: organizations.length,
                      itemBuilder: (context, index) {
                        final organization = organizations[index];
                        return OrganizationCard(
                          organization: organization,
                          onTap: () {
                            ScaffoldMessenger.of(context).showSnackBar(
                              SnackBar(
                                content: Text('Visualizando: ${organization.name}'),
                                backgroundColor: AppColors.primary,
                              ),
                            );
                          },
                        );
                      },
                    ),
            ),
          ],
        ),
      ),
    );
  }
} 