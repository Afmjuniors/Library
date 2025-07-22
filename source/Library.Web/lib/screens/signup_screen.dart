import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import '../constants/app_constants.dart';
import '../models/auth.dart';
import '../providers/auth_provider.dart';
import '../widgets/custom_text_field.dart';
import '../widgets/custom_button.dart';

class SignUpScreen extends StatefulWidget {
  const SignUpScreen({super.key});

  @override
  State<SignUpScreen> createState() => _SignUpScreenState();
}

class _SignUpScreenState extends State<SignUpScreen> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _confirmPasswordController = TextEditingController();
  final _phoneController = TextEditingController();
  final _addressController = TextEditingController();
  final _additionalInfoController = TextEditingController();
  
  bool _obscurePassword = true;
  bool _obscureConfirmPassword = true;
  DateTime? _selectedBirthDate;
  bool _showOptionalFields = false;

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    _phoneController.dispose();
    _addressController.dispose();
    _additionalInfoController.dispose();
    super.dispose();
  }

  Future<void> _selectBirthDate() async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: DateTime.now().subtract(const Duration(days: 6570)), // 18 years ago
      firstDate: DateTime.now().subtract(const Duration(days: 36500)), // 100 years ago
      lastDate: DateTime.now().subtract(const Duration(days: 6570)), // 18 years ago
    );
    if (picked != null && picked != _selectedBirthDate) {
      setState(() {
        _selectedBirthDate = picked;
      });
    }
  }

  Future<void> _signup() async {
    if (!_formKey.currentState!.validate()) return;

    if (_passwordController.text != _confirmPasswordController.text) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Passwords do not match'),
          backgroundColor: AppConstants.errorColor,
        ),
      );
      return;
    }

    final authProvider = context.read<AuthProvider>();
    final request = SignUpRequest(
      name: _nameController.text.trim(),
      email: _emailController.text.trim(),
      password: _passwordController.text,
      birthDay: _selectedBirthDate,
      phone: _phoneController.text.trim().isEmpty ? null : _phoneController.text.trim(),
      address: _addressController.text.trim().isEmpty ? null : _addressController.text.trim(),
      additionalInfo: _additionalInfoController.text.trim().isEmpty ? null : _additionalInfoController.text.trim(),
    );

    final success = await authProvider.signup(request);

    if (success && mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: const Text(AppConstants.signupSuccess),
          backgroundColor: AppConstants.successColor,
        ),
      );
    } else if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(authProvider.error ?? AppConstants.unknownError),
          backgroundColor: AppConstants.errorColor,
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Sign Up'),
        backgroundColor: Colors.transparent,
        elevation: 0,
      ),
      body: Container(
        decoration: const BoxDecoration(
          gradient: LinearGradient(
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
            colors: [
              AppConstants.primaryColor,
              AppConstants.secondaryColor,
            ],
          ),
        ),
        child: SafeArea(
          child: Center(
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(AppConstants.paddingLarge),
              child: Card(
                elevation: 8,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(AppConstants.borderRadiusLarge),
                ),
                child: Padding(
                  padding: const EdgeInsets.all(AppConstants.paddingXLarge),
                  child: Form(
                    key: _formKey,
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        // App Logo/Icon
                        Container(
                          width: 80,
                          height: 80,
                          decoration: BoxDecoration(
                            color: AppConstants.primaryColor,
                            borderRadius: BorderRadius.circular(40),
                          ),
                          child: const Icon(
                            Icons.person_add,
                            size: 40,
                            color: Colors.white,
                          ),
                        ),
                        const SizedBox(height: AppConstants.paddingLarge),
                        
                        // Title
                        Text(
                          'Create Account',
                          style: AppConstants.headingStyle.copyWith(
                            color: AppConstants.primaryColor,
                          ),
                        ),
                        const SizedBox(height: AppConstants.paddingSmall),
                        Text(
                          'Join our library community',
                          style: AppConstants.captionStyle,
                        ),
                        const SizedBox(height: AppConstants.paddingXLarge),

                        // Required Fields
                        CustomTextField(
                          controller: _nameController,
                          labelText: 'Full Name *',
                          prefixIcon: Icons.person,
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return AppConstants.nameRequired;
                            }
                            if (value.length < 2) {
                              return AppConstants.nameMinLength;
                            }
                            return null;
                          },
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),

                        CustomTextField(
                          controller: _emailController,
                          labelText: 'Email *',
                          prefixIcon: Icons.email,
                          keyboardType: TextInputType.emailAddress,
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return AppConstants.emailRequired;
                            }
                            if (!RegExp(r'^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$').hasMatch(value)) {
                              return AppConstants.emailInvalid;
                            }
                            return null;
                          },
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),

                        // Birth Date Field
                        InkWell(
                          onTap: _selectBirthDate,
                          child: Container(
                            padding: const EdgeInsets.symmetric(
                              horizontal: AppConstants.paddingMedium,
                              vertical: AppConstants.paddingMedium,
                            ),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.grey),
                              borderRadius: BorderRadius.circular(AppConstants.borderRadiusMedium),
                              color: Colors.white,
                            ),
                            child: Row(
                              children: [
                                const Icon(Icons.calendar_today, color: Colors.grey),
                                const SizedBox(width: AppConstants.paddingMedium),
                                Expanded(
                                  child: Text(
                                    _selectedBirthDate != null
                                        ? DateFormat('MMM dd, yyyy').format(_selectedBirthDate!)
                                        : 'Birth Date *',
                                    style: TextStyle(
                                      color: _selectedBirthDate != null ? Colors.black : Colors.grey,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),

                        CustomTextField(
                          controller: _passwordController,
                          labelText: 'Password *',
                          prefixIcon: Icons.lock,
                          obscureText: _obscurePassword,
                          suffixIcon: IconButton(
                            icon: Icon(
                              _obscurePassword ? Icons.visibility : Icons.visibility_off,
                            ),
                            onPressed: () {
                              setState(() {
                                _obscurePassword = !_obscurePassword;
                              });
                            },
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return AppConstants.passwordRequired;
                            }
                            if (value.length < 6) {
                              return AppConstants.passwordMinLength;
                            }
                            return null;
                          },
                        ),
                        const SizedBox(height: AppConstants.paddingMedium),

                        CustomTextField(
                          controller: _confirmPasswordController,
                          labelText: 'Confirm Password *',
                          prefixIcon: Icons.lock,
                          obscureText: _obscureConfirmPassword,
                          suffixIcon: IconButton(
                            icon: Icon(
                              _obscureConfirmPassword ? Icons.visibility : Icons.visibility_off,
                            ),
                            onPressed: () {
                              setState(() {
                                _obscureConfirmPassword = !_obscureConfirmPassword;
                              });
                            },
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return 'Please confirm your password';
                            }
                            if (value != _passwordController.text) {
                              return 'Passwords do not match';
                            }
                            return null;
                          },
                        ),
                        const SizedBox(height: AppConstants.paddingLarge),

                        // Optional Fields Toggle
                        TextButton.icon(
                          onPressed: () {
                            setState(() {
                              _showOptionalFields = !_showOptionalFields;
                            });
                          },
                          icon: Icon(
                            _showOptionalFields ? Icons.expand_less : Icons.expand_more,
                          ),
                          label: Text(_showOptionalFields ? 'Hide Optional Fields' : 'Show Optional Fields'),
                        ),
                        const SizedBox(height: AppConstants.paddingSmall),

                        // Optional Fields
                        if (_showOptionalFields) ...[
                          CustomTextField(
                            controller: _phoneController,
                            labelText: 'Phone Number',
                            prefixIcon: Icons.phone,
                            keyboardType: TextInputType.phone,
                          ),
                          const SizedBox(height: AppConstants.paddingMedium),

                          CustomTextField(
                            controller: _addressController,
                            labelText: 'Address',
                            prefixIcon: Icons.location_on,
                            maxLines: 2,
                          ),
                          const SizedBox(height: AppConstants.paddingMedium),

                          CustomTextField(
                            controller: _additionalInfoController,
                            labelText: 'Additional Information',
                            prefixIcon: Icons.info,
                            maxLines: 3,
                          ),
                          const SizedBox(height: AppConstants.paddingLarge),
                        ],

                        // Sign Up Button
                        Consumer<AuthProvider>(
                          builder: (context, authProvider, child) {
                            return CustomButton(
                              onPressed: authProvider.isLoading ? null : _signup,
                              child: authProvider.isLoading
                                  ? const SizedBox(
                                      width: 20,
                                      height: 20,
                                      child: CircularProgressIndicator(
                                        strokeWidth: 2,
                                        valueColor: AlwaysStoppedAnimation<Color>(Colors.white),
                                      ),
                                    )
                                  : const Text('Sign Up'),
                            );
                          },
                        ),
                        const SizedBox(height: AppConstants.paddingLarge),

                        // Login Link
                        Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Text(
                              'Already have an account? ',
                              style: AppConstants.captionStyle,
                            ),
                            TextButton(
                              onPressed: () => context.go('/login'),
                              child: const Text(
                                'Login',
                                style: TextStyle(
                                  color: AppConstants.primaryColor,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
} 