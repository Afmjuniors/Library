import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../core/constants/app_colors.dart';
import '../../core/constants/app_spacing.dart';
import '../../core/constants/app_typography.dart';
import '../../core/constants/app_radius.dart';
import '../../core/constants/app_shadows.dart';
import '../../data/models/book_model.dart';
import '../../data/models/enums.dart';

class BookCard extends StatelessWidget {
  final BookModel book;
  final VoidCallback? onTap;

  const BookCard({
    super.key,
    required this.book,
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
            child: Row(
              children: [
                // Book Image
                Container(
                  width: 60,
                  height: 80,
                  decoration: BoxDecoration(
                    color: AppColors.gray200,
                    borderRadius: BorderRadius.circular(AppRadius.md),
                  ),
                  child: book.image.isNotEmpty
                      ? ClipRRect(
                          borderRadius: BorderRadius.circular(AppRadius.md),
                          child: CachedNetworkImage(
                            imageUrl: book.image,
                            fit: BoxFit.cover,
                            placeholder: (context, url) => const Center(
                              child: Icon(
                                Icons.book,
                                color: AppColors.gray400,
                              ),
                            ),
                            errorWidget: (context, url, error) => const Center(
                              child: Icon(
                                Icons.book,
                                color: AppColors.gray400,
                              ),
                            ),
                          ),
                        )
                      : const Center(
                          child: Icon(
                            Icons.book,
                            color: AppColors.gray400,
                          ),
                        ),
                ),
                
                const SizedBox(width: AppSpacing.md),
                
                // Book Info
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        book.name,
                        style: TextStyle(
                          fontSize: AppTypography.lg,
                          fontWeight: AppTypography.semibold,
                          color: AppColors.textPrimary,
                        ),
                        maxLines: 2,
                        overflow: TextOverflow.ellipsis,
                      ),
                      
                      const SizedBox(height: AppSpacing.xs),
                      
                      Text(
                        'por ${book.author}',
                        style: TextStyle(
                          fontSize: AppTypography.sm,
                          color: AppColors.textSecondary,
                        ),
                      ),
                      
                      const SizedBox(height: AppSpacing.xs),
                      
                      Text(
                        Genre.getText(book.genre),
                        style: TextStyle(
                          fontSize: AppTypography.xs,
                          color: AppColors.primary,
                        ),
                      ),
                      
                      const SizedBox(height: AppSpacing.sm),
                      
                      Row(
                        children: [
                          // Status Badge
                          Container(
                            padding: const EdgeInsets.symmetric(
                              horizontal: AppSpacing.sm,
                              vertical: AppSpacing.xs,
                            ),
                            decoration: BoxDecoration(
                              color: Color(BookStatus.getColor(book.bookStatusId)),
                              borderRadius: BorderRadius.circular(AppRadius.sm),
                            ),
                            child: Text(
                              BookStatus.getText(book.bookStatusId),
                              style: const TextStyle(
                                color: AppColors.white,
                                fontSize: AppTypography.xs,
                                fontWeight: AppTypography.semibold,
                              ),
                            ),
                          ),
                          
                          const Spacer(),
                          
                          // Owner Info
                          Text(
                            'de ${book.ownerInfo?.name ?? 'Usuário'}',
                            style: TextStyle(
                              fontSize: AppTypography.xs,
                              color: AppColors.textSecondary,
                              fontStyle: FontStyle.italic,
                            ),
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
} 