import 'package:json_annotation/json_annotation.dart';
import 'enums.dart';

part 'user.g.dart';

@JsonSerializable()
class User {
  @JsonKey(name: 'userId')
  final int? userId;
  
  @JsonKey(name: 'createdAt')
  final DateTime? createdAt;
  
  @JsonKey(name: 'birthDay')
  final DateTime? birthDay;
  
  @JsonKey(name: 'email')
  final String email;
  
  @JsonKey(name: 'name')
  final String name;
  
  @JsonKey(name: 'phone')
  final String? phone;
  
  @JsonKey(name: 'address')
  final String? address;
  
  @JsonKey(name: 'additionalInfo')
  final String? additionalInfo;
  
  @JsonKey(name: 'password')
  final String? password;
  
  @JsonKey(name: 'image')
  final String? image;
  
  @JsonKey(name: 'token')
  final String? token;
  
  @JsonKey(name: 'languageId')
  final int languageId;
  
  @JsonKey(name: 'userStatusId')
  final EnumUserStatus? userStatusId;

  // Additional fields for the app
  EnumUserRole? role;
  String? organizationId;

  User({
    this.userId,
    this.createdAt,
    this.birthDay,
    required this.email,
    required this.name,
    this.phone,
    this.address,
    this.additionalInfo,
    this.password,
    this.image,
    this.token,
    this.languageId = 1,
    this.userStatusId,
    this.role,
    this.organizationId,
  });

  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  Map<String, dynamic> toJson() => _$UserToJson(this);

  User copyWith({
    int? userId,
    DateTime? createdAt,
    DateTime? birthDay,
    String? email,
    String? name,
    String? phone,
    String? address,
    String? additionalInfo,
    String? password,
    String? image,
    String? token,
    int? languageId,
    EnumUserStatus? userStatusId,
    EnumUserRole? role,
    String? organizationId,
  }) {
    return User(
      userId: userId ?? this.userId,
      createdAt: createdAt ?? this.createdAt,
      birthDay: birthDay ?? this.birthDay,
      email: email ?? this.email,
      name: name ?? this.name,
      phone: phone ?? this.phone,
      address: address ?? this.address,
      additionalInfo: additionalInfo ?? this.additionalInfo,
      password: password ?? this.password,
      image: image ?? this.image,
      token: token ?? this.token,
      languageId: languageId ?? this.languageId,
      userStatusId: userStatusId ?? this.userStatusId,
      role: role ?? this.role,
      organizationId: organizationId ?? this.organizationId,
    );
  }

  bool get isActive => userStatusId == EnumUserStatus.active;
  bool get isAdmin => role == EnumUserRole.admin;
  bool get isLeader => role == EnumUserRole.leader;
  
  String get displayName => name.isNotEmpty ? name : email;
  
  String get initials {
    if (name.isEmpty) return email.substring(0, 2).toUpperCase();
    final names = name.split(' ');
    if (names.length >= 2) {
      return '${names[0][0]}${names[1][0]}'.toUpperCase();
    }
    return name.substring(0, 2).toUpperCase();
  }
} 