// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'organization_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OrganizationModel _$OrganizationModelFromJson(Map<String, dynamic> json) =>
    OrganizationModel(
      organizationId: (json['organizationId'] as num).toInt(),
      name: json['name'] as String,
      description: json['description'] as String?,
      image: json['image'] as String?,
      memberCount: (json['memberCount'] as num).toInt(),
      createdAt: json['createdAt'] as String,
      rules: OrganizationRules.fromJson(json['rules'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$OrganizationModelToJson(OrganizationModel instance) =>
    <String, dynamic>{
      'organizationId': instance.organizationId,
      'name': instance.name,
      'description': instance.description,
      'image': instance.image,
      'memberCount': instance.memberCount,
      'createdAt': instance.createdAt,
      'rules': instance.rules,
    };

ExtendedOrganizationModel _$ExtendedOrganizationModelFromJson(
        Map<String, dynamic> json) =>
    ExtendedOrganizationModel(
      id: (json['id'] as num).toInt(),
      name: json['name'] as String,
      description: json['description'] as String,
      memberCount: (json['memberCount'] as num).toInt(),
      role: json['role'] as String,
      isActive: json['isActive'] as bool,
      image: json['image'] as String,
      createdAt: json['createdAt'] as String,
      rules: OrganizationRules.fromJson(json['rules'] as Map<String, dynamic>),
      regulations: (json['regulations'] as List<dynamic>)
          .map((e) => e as String)
          .toList(),
      stats: OrganizationStats.fromJson(json['stats'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$ExtendedOrganizationModelToJson(
        ExtendedOrganizationModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'description': instance.description,
      'memberCount': instance.memberCount,
      'role': instance.role,
      'isActive': instance.isActive,
      'image': instance.image,
      'createdAt': instance.createdAt,
      'rules': instance.rules,
      'regulations': instance.regulations,
      'stats': instance.stats,
    };

OrganizationRules _$OrganizationRulesFromJson(Map<String, dynamic> json) =>
    OrganizationRules(
      loanDurationDays: (json['loanDurationDays'] as num).toInt(),
      meetingFrequency: json['meetingFrequency'] as String,
      meetingDay: json['meetingDay'] as String?,
      meetingWeek: (json['meetingWeek'] as num?)?.toInt(),
      meetingTime: json['meetingTime'] as String?,
      requireCompleteUserInfo: json['requireCompleteUserInfo'] as bool,
    );

Map<String, dynamic> _$OrganizationRulesToJson(OrganizationRules instance) =>
    <String, dynamic>{
      'loanDurationDays': instance.loanDurationDays,
      'meetingFrequency': instance.meetingFrequency,
      'meetingDay': instance.meetingDay,
      'meetingWeek': instance.meetingWeek,
      'meetingTime': instance.meetingTime,
      'requireCompleteUserInfo': instance.requireCompleteUserInfo,
    };

OrganizationStats _$OrganizationStatsFromJson(Map<String, dynamic> json) =>
    OrganizationStats(
      totalBooks: (json['totalBooks'] as num).toInt(),
      activeLoans: (json['activeLoans'] as num).toInt(),
      totalMembers: (json['totalMembers'] as num).toInt(),
      monthlyLoans: (json['monthlyLoans'] as num).toInt(),
    );

Map<String, dynamic> _$OrganizationStatsToJson(OrganizationStats instance) =>
    <String, dynamic>{
      'totalBooks': instance.totalBooks,
      'activeLoans': instance.activeLoans,
      'totalMembers': instance.totalMembers,
      'monthlyLoans': instance.monthlyLoans,
    };
