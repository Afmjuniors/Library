import 'package:json_annotation/json_annotation.dart';

part 'organization_model.g.dart';

@JsonSerializable()
class OrganizationModel {
  @JsonKey(name: 'organizationId')
  final int organizationId;
  
  final String name;
  final String? description;
  final String? image;
  
  @JsonKey(name: 'memberCount')
  final int memberCount;
  
  final String createdAt;
  final OrganizationRules rules;

  const OrganizationModel({
    required this.organizationId,
    required this.name,
    this.description,
    this.image,
    required this.memberCount,
    required this.createdAt,
    required this.rules,
  });

  factory OrganizationModel.fromJson(Map<String, dynamic> json) => 
      _$OrganizationModelFromJson(json);
  Map<String, dynamic> toJson() => _$OrganizationModelToJson(this);
}

@JsonSerializable()
class ExtendedOrganizationModel {
  final int id;
  final String name;
  final String description;
  
  @JsonKey(name: 'memberCount')
  final int memberCount;
  
  final String role;
  
  @JsonKey(name: 'isActive')
  final bool isActive;
  
  final String image;
  final String createdAt;
  final OrganizationRules rules;
  final List<String> regulations;
  final OrganizationStats stats;

  const ExtendedOrganizationModel({
    required this.id,
    required this.name,
    required this.description,
    required this.memberCount,
    required this.role,
    required this.isActive,
    required this.image,
    required this.createdAt,
    required this.rules,
    required this.regulations,
    required this.stats,
  });

  factory ExtendedOrganizationModel.fromJson(Map<String, dynamic> json) => 
      _$ExtendedOrganizationModelFromJson(json);
  Map<String, dynamic> toJson() => _$ExtendedOrganizationModelToJson(this);
}

@JsonSerializable()
class OrganizationRules {
  @JsonKey(name: 'loanDurationDays')
  final int loanDurationDays;
  
  @JsonKey(name: 'meetingFrequency')
  final String meetingFrequency;
  
  @JsonKey(name: 'meetingDay')
  final String? meetingDay;
  
  @JsonKey(name: 'meetingWeek')
  final int? meetingWeek;
  
  @JsonKey(name: 'meetingTime')
  final String? meetingTime;
  
  @JsonKey(name: 'requireCompleteUserInfo')
  final bool requireCompleteUserInfo;

  const OrganizationRules({
    required this.loanDurationDays,
    required this.meetingFrequency,
    this.meetingDay,
    this.meetingWeek,
    this.meetingTime,
    required this.requireCompleteUserInfo,
  });

  factory OrganizationRules.fromJson(Map<String, dynamic> json) => 
      _$OrganizationRulesFromJson(json);
  Map<String, dynamic> toJson() => _$OrganizationRulesToJson(this);
}

@JsonSerializable()
class OrganizationStats {
  @JsonKey(name: 'totalBooks')
  final int totalBooks;
  
  @JsonKey(name: 'activeLoans')
  final int activeLoans;
  
  @JsonKey(name: 'totalMembers')
  final int totalMembers;
  
  @JsonKey(name: 'monthlyLoans')
  final int monthlyLoans;

  const OrganizationStats({
    required this.totalBooks,
    required this.activeLoans,
    required this.totalMembers,
    required this.monthlyLoans,
  });

  factory OrganizationStats.fromJson(Map<String, dynamic> json) => 
      _$OrganizationStatsFromJson(json);
  Map<String, dynamic> toJson() => _$OrganizationStatsToJson(this);
} 