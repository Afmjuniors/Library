import 'package:flutter/material.dart';
import '../constants/app_constants.dart';

class RegulationScreen extends StatelessWidget {
  const RegulationScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Lending Regulations'),
        backgroundColor: Colors.transparent,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(AppConstants.paddingLarge),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Header
            Container(
              width: double.infinity,
              padding: const EdgeInsets.all(AppConstants.paddingLarge),
              decoration: BoxDecoration(
                gradient: const LinearGradient(
                  colors: [AppConstants.primaryColor, AppConstants.secondaryColor],
                ),
                borderRadius: BorderRadius.circular(AppConstants.borderRadiusLarge),
              ),
              child: Column(
                children: [
                  const Icon(
                    Icons.rule,
                    size: 48,
                    color: Colors.white,
                  ),
                  const SizedBox(height: AppConstants.paddingMedium),
                  Text(
                    'Library Lending Rules',
                    style: AppConstants.headingStyle.copyWith(
                      color: Colors.white,
                      fontSize: 24,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: AppConstants.paddingSmall),
                  Text(
                    'Please read and follow these guidelines',
                    style: AppConstants.captionStyle.copyWith(
                      color: Colors.white70,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ],
              ),
            ),
            const SizedBox(height: AppConstants.paddingXLarge),

            // Rules
            _buildRuleSection(
              title: '1. Lending Period',
              content: 'The default lending period is ${AppConstants.maxLoanDays} days per book. This period may be extended by mutual agreement between the lender and borrower.',
              icon: Icons.schedule,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            _buildRuleSection(
              title: '2. Book Requests',
              content: 'Members can request to borrow books from other members. The book owner has the right to approve or reject loan requests.',
              icon: Icons.book,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            _buildRuleSection(
              title: '3. Book Returns',
              content: 'Borrowers must return books on or before the due date. If a book is returned late, the borrower may be subject to restrictions on future loans.',
              icon: Icons.assignment_return,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            _buildRuleSection(
              title: '4. Book Condition',
              content: 'Borrowers are responsible for maintaining the book in good condition. Any damage to the book should be reported to the owner immediately.',
              icon: Icons.verified,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            _buildRuleSection(
              title: '5. Communication',
              content: 'Lenders and borrowers should communicate openly about loan arrangements, extensions, and any issues that arise.',
              icon: Icons.message,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            _buildRuleSection(
              title: '6. Book Ownership',
              content: 'Book owners retain full ownership of their books. The lending system is designed to facilitate sharing, not transfer ownership.',
              icon: Icons.person,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            _buildRuleSection(
              title: '7. Disputes',
              content: 'In case of disputes between lenders and borrowers, the organization administrators will mediate and make final decisions.',
              icon: Icons.gavel,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            _buildRuleSection(
              title: '8. Organization Membership',
              content: 'Only registered members of the organization can participate in the lending system. Members must maintain active status.',
              icon: Icons.group,
            ),
            const SizedBox(height: AppConstants.paddingLarge),

            // Additional Information
            Container(
              padding: const EdgeInsets.all(AppConstants.paddingLarge),
              decoration: BoxDecoration(
                color: AppConstants.warningColor.withOpacity(0.1),
                borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
                border: Border.all(color: AppConstants.warningColor.withOpacity(0.3)),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    children: [
                      Icon(
                        Icons.info,
                        color: AppConstants.warningColor,
                      ),
                      const SizedBox(width: AppConstants.paddingSmall),
                      Text(
                        'Important Notes',
                        style: AppConstants.subheadingStyle.copyWith(
                          color: AppConstants.warningColor,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: AppConstants.paddingMedium),
                  Text(
                    '• These rules are designed to ensure fair and respectful sharing of books within our community.\n'
                    '• Violation of these rules may result in temporary or permanent suspension from the lending system.\n'
                    '• The organization reserves the right to update these rules as needed.\n'
                    '• For questions or clarifications, please contact the organization administrators.',
                    style: AppConstants.bodyStyle,
                  ),
                ],
              ),
            ),
            const SizedBox(height: AppConstants.paddingXLarge),

            // Contact Information
            Container(
              padding: const EdgeInsets.all(AppConstants.paddingLarge),
              decoration: BoxDecoration(
                color: Colors.grey[100],
                borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Contact Information',
                    style: AppConstants.subheadingStyle,
                  ),
                  const SizedBox(height: AppConstants.paddingMedium),
                  _buildContactItem(
                    icon: Icons.email,
                    label: 'Email',
                    value: 'admin@library.org',
                  ),
                  const SizedBox(height: AppConstants.paddingSmall),
                  _buildContactItem(
                    icon: Icons.phone,
                    label: 'Phone',
                    value: '+1 (555) 123-4567',
                  ),
                  const SizedBox(height: AppConstants.paddingSmall),
                  _buildContactItem(
                    icon: Icons.schedule,
                    label: 'Office Hours',
                    value: 'Monday - Friday, 9:00 AM - 5:00 PM',
                  ),
                ],
              ),
            ),
            const SizedBox(height: AppConstants.paddingXLarge),
          ],
        ),
      ),
    );
  }

  Widget _buildRuleSection({
    required String title,
    required String content,
    required IconData icon,
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
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            padding: const EdgeInsets.all(AppConstants.paddingMedium),
            decoration: BoxDecoration(
              color: AppConstants.primaryColor.withOpacity(0.1),
              borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
            ),
            child: Icon(
              icon,
              color: AppConstants.primaryColor,
              size: 24,
            ),
          ),
          const SizedBox(width: AppConstants.paddingMedium),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  title,
                  style: AppConstants.subheadingStyle.copyWith(fontSize: 16),
                ),
                const SizedBox(height: AppConstants.paddingSmall),
                Text(
                  content,
                  style: AppConstants.bodyStyle,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildContactItem({
    required IconData icon,
    required String label,
    required String value,
  }) {
    return Row(
      children: [
        Icon(
          icon,
          size: 16,
          color: Colors.grey[600],
        ),
        const SizedBox(width: AppConstants.paddingSmall),
        Text(
          '$label: ',
          style: AppConstants.captionStyle.copyWith(
            fontWeight: FontWeight.w500,
          ),
        ),
        Text(
          value,
          style: AppConstants.captionStyle,
        ),
      ],
    );
  }
} 