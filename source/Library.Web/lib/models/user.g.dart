// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

User _$UserFromJson(Map<String, dynamic> json) => User(
      userId: (json['userId'] as num?)?.toInt(),
      createdAt: json['createdAt'] == null
          ? null
          : DateTime.parse(json['createdAt'] as String),
      birthDay: json['birthDay'] == null
          ? null
          : DateTime.parse(json['birthDay'] as String),
      email: json['email'] as String,
      name: json['name'] as String,
      phone: json['phone'] as String?,
      address: json['address'] as String?,
      additionalInfo: json['additionalInfo'] as String?,
      password: json['password'] as String?,
      image: json['image'] as String?,
      token: json['token'] as String?,
      languageId: (json['languageId'] as num?)?.toInt() ?? 1,
      userStatusId:
          $enumDecodeNullable(_$EnumUserStatusEnumMap, json['userStatusId']),
      role: $enumDecodeNullable(_$EnumUserRoleEnumMap, json['role']),
      organizationId: json['organizationId'] as String?,
    );

Map<String, dynamic> _$UserToJson(User instance) => <String, dynamic>{
      'userId': instance.userId,
      'createdAt': instance.createdAt?.toIso8601String(),
      'birthDay': instance.birthDay?.toIso8601String(),
      'email': instance.email,
      'name': instance.name,
      'phone': instance.phone,
      'address': instance.address,
      'additionalInfo': instance.additionalInfo,
      'password': instance.password,
      'image': instance.image,
      'token': instance.token,
      'languageId': instance.languageId,
      'userStatusId': _$EnumUserStatusEnumMap[instance.userStatusId],
      'role': _$EnumUserRoleEnumMap[instance.role],
      'organizationId': instance.organizationId,
    };

const _$EnumUserStatusEnumMap = {
  EnumUserStatus.active: 'active',
  EnumUserStatus.desactive: 'desactive',
  EnumUserStatus.inapt: 'inapt',
};

const _$EnumUserRoleEnumMap = {
  EnumUserRole.admin: 'admin',
  EnumUserRole.leader: 'leader',
  EnumUserRole.normal: 'normal',
};
