import 'package:json_annotation/json_annotation.dart';
import 'user.dart';

part 'auth.g.dart';

@JsonSerializable()
class UserAuth {
  @JsonKey(name: 'username')
  final String username;
  
  @JsonKey(name: 'password')
  final String password;

  UserAuth({
    required this.username,
    required this.password,
  });

  factory UserAuth.fromJson(Map<String, dynamic> json) => _$UserAuthFromJson(json);
  Map<String, dynamic> toJson() => _$UserAuthToJson(this);
}

@JsonSerializable()
class SignUpRequest {
  final String name;
  final String email;
  final String password;
  final DateTime? birthDay;
  final String? phone;
  final String? address;
  final String? additionalInfo;

  SignUpRequest({
    required this.name,
    required this.email,
    required this.password,
    this.birthDay,
    this.phone,
    this.address,
    this.additionalInfo,
  });

  factory SignUpRequest.fromJson(Map<String, dynamic> json) => _$SignUpRequestFromJson(json);
  Map<String, dynamic> toJson() => _$SignUpRequestToJson(this);
}

@JsonSerializable()
class AuthResponse {
  final bool success;
  final String? token;
  final String? message;
  final User? user;

  AuthResponse({
    required this.success,
    this.token,
    this.message,
    this.user,
  });

  factory AuthResponse.fromJson(Map<String, dynamic> json) => _$AuthResponseFromJson(json);
  Map<String, dynamic> toJson() => _$AuthResponseToJson(this);
} 