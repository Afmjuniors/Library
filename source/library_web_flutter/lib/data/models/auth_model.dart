import 'package:json_annotation/json_annotation.dart';
import 'user_model.dart';

part 'auth_model.g.dart';

@JsonSerializable()
class AuthDataModel {
  final UserModel user;
  final String token;

  const AuthDataModel({
    required this.user,
    required this.token,
  });

  factory AuthDataModel.fromJson(Map<String, dynamic> json) => 
      _$AuthDataModelFromJson(json);
  Map<String, dynamic> toJson() => _$AuthDataModelToJson(this);
}

@JsonSerializable()
class LoginRequestModel {
  final String email;
  final String password;

  const LoginRequestModel({
    required this.email,
    required this.password,
  });

  factory LoginRequestModel.fromJson(Map<String, dynamic> json) => 
      _$LoginRequestModelFromJson(json);
  Map<String, dynamic> toJson() => _$LoginRequestModelToJson(this);
}

@JsonSerializable()
class SignupRequestModel {
  final String name;
  final String email;
  final String password;

  const SignupRequestModel({
    required this.name,
    required this.email,
    required this.password,
  });

  factory SignupRequestModel.fromJson(Map<String, dynamic> json) => 
      _$SignupRequestModelFromJson(json);
  Map<String, dynamic> toJson() => _$SignupRequestModelToJson(this);
}

@JsonSerializable()
class LoginResultModel {
  final bool success;
  final String? message;
  final AuthDataModel? data;

  const LoginResultModel({
    required this.success,
    this.message,
    this.data,
  });

  factory LoginResultModel.fromJson(Map<String, dynamic> json) => 
      _$LoginResultModelFromJson(json);
  Map<String, dynamic> toJson() => _$LoginResultModelToJson(this);
}

@JsonSerializable()
class AuthenticatedUserModel {
  final UserModel user;
  final String token;

  const AuthenticatedUserModel({
    required this.user,
    required this.token,
  });

  factory AuthenticatedUserModel.fromJson(Map<String, dynamic> json) => 
      _$AuthenticatedUserModelFromJson(json);
  Map<String, dynamic> toJson() => _$AuthenticatedUserModelToJson(this);
} 