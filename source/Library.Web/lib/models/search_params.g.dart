// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'search_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SearchBookParams _$SearchBookParamsFromJson(Map<String, dynamic> json) =>
    SearchBookParams(
      keyWord: json['keyWord'] as String?,
      userId: (json['userId'] as num?)?.toInt(),
      ownerId: (json['ownerId'] as num?)?.toInt(),
      organizationId: (json['organizationId'] as num?)?.toInt(),
      userName: json['userName'] as String?,
      author: json['author'] as String?,
      genre: $enumDecodeNullable(_$EnumGenreEnumMap, json['genre']),
      bookStatus:
          $enumDecodeNullable(_$EnumBookStatusEnumMap, json['bookStatus']),
      loanStatus:
          $enumDecodeNullable(_$EnumLoanStatusEnumMap, json['loanStatus']),
    );

Map<String, dynamic> _$SearchBookParamsToJson(SearchBookParams instance) =>
    <String, dynamic>{
      'keyWord': instance.keyWord,
      'userId': instance.userId,
      'ownerId': instance.ownerId,
      'organizationId': instance.organizationId,
      'userName': instance.userName,
      'author': instance.author,
      'genre': _$EnumGenreEnumMap[instance.genre],
      'bookStatus': _$EnumBookStatusEnumMap[instance.bookStatus],
      'loanStatus': _$EnumLoanStatusEnumMap[instance.loanStatus],
    };

const _$EnumGenreEnumMap = {
  EnumGenre.action: 'action',
  EnumGenre.adventure: 'adventure',
  EnumGenre.comedy: 'comedy',
  EnumGenre.drama: 'drama',
  EnumGenre.fantasy: 'fantasy',
  EnumGenre.horror: 'horror',
  EnumGenre.mystery: 'mystery',
  EnumGenre.romance: 'romance',
  EnumGenre.scienceFiction: 'scienceFiction',
  EnumGenre.thriller: 'thriller',
  EnumGenre.western: 'western',
};

const _$EnumBookStatusEnumMap = {
  EnumBookStatus.available: 'available',
  EnumBookStatus.inactive: 'inactive',
};

const _$EnumLoanStatusEnumMap = {
  EnumLoanStatus.pending: 'pending',
  EnumLoanStatus.approved: 'approved',
  EnumLoanStatus.rejected: 'rejected',
  EnumLoanStatus.received: 'received',
  EnumLoanStatus.returned: 'returned',
  EnumLoanStatus.overdue: 'overdue',
};
