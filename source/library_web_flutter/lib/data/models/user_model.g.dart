// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserModel _$UserModelFromJson(Map<String, dynamic> json) => UserModel(
      userId: (json['userId'] as num).toInt(),
      name: json['name'] as String,
      email: json['email'] as String,
      phone: json['phone'] as String?,
      address: json['address'] as String?,
      additionalInfo: json['additionalInfo'] as String?,
      image: json['image'] as String?,
      birthDay: json['birthDay'] as String?,
      createdAt: json['createdAt'] as String?,
      cultureInfo: json['cultureInfo'] as String?,
    );

Map<String, dynamic> _$UserModelToJson(UserModel instance) => <String, dynamic>{
      'userId': instance.userId,
      'name': instance.name,
      'email': instance.email,
      'phone': instance.phone,
      'address': instance.address,
      'additionalInfo': instance.additionalInfo,
      'image': instance.image,
      'birthDay': instance.birthDay,
      'createdAt': instance.createdAt,
      'cultureInfo': instance.cultureInfo,
    };
