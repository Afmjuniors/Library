import 'package:json_annotation/json_annotation.dart';

part 'user_model.g.dart';

@JsonSerializable()
class UserModel {
  @JsonKey(name: 'userId')
  final int userId;
  
  final String name;
  final String email;
  final String? phone;
  final String? address;
  final String? additionalInfo;
  final String? image;
  final String? birthDay;
  final String? createdAt;
  final String? cultureInfo;

  const UserModel({
    required this.userId,
    required this.name,
    required this.email,
    this.phone,
    this.address,
    this.additionalInfo,
    this.image,
    this.birthDay,
    this.createdAt,
    this.cultureInfo,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) => _$UserModelFromJson(json);
  Map<String, dynamic> toJson() => _$UserModelToJson(this);

  UserModel copyWith({
    int? userId,
    String? name,
    String? email,
    String? phone,
    String? address,
    String? additionalInfo,
    String? image,
    String? birthDay,
    String? createdAt,
    String? cultureInfo,
  }) {
    return UserModel(
      userId: userId ?? this.userId,
      name: name ?? this.name,
      email: email ?? this.email,
      phone: phone ?? this.phone,
      address: address ?? this.address,
      additionalInfo: additionalInfo ?? this.additionalInfo,
      image: image ?? this.image,
      birthDay: birthDay ?? this.birthDay,
      createdAt: createdAt ?? this.createdAt,
      cultureInfo: cultureInfo ?? this.cultureInfo,
    );
  }
} 