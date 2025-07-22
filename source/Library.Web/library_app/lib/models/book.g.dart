// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'book.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Book _$BookFromJson(Map<String, dynamic> json) => Book(
      bookId: (json['bookId'] as num?)?.toInt(),
      createdAt: json['createdAt'] == null
          ? null
          : DateTime.parse(json['createdAt'] as String),
      bookStatusId: $enumDecode(_$EnumBookStatusEnumMap, json['bookStatusId']),
      name: json['name'] as String,
      ownerId: (json['ownerId'] as num).toInt(),
      author: json['author'] as String?,
      genre: $enumDecodeNullable(_$EnumGenreEnumMap, json['genre']),
      description: json['description'] as String?,
      url: json['url'] as String?,
      image: json['image'] as String?,
      updatedAt: json['updatedAt'] == null
          ? null
          : DateTime.parse(json['updatedAt'] as String),
      owner: json['owner'] == null
          ? null
          : User.fromJson(json['owner'] as Map<String, dynamic>),
      currentLoanStatus: $enumDecodeNullable(
          _$EnumLoanStatusEnumMap, json['currentLoanStatus']),
      currentBorrower: json['currentBorrower'] == null
          ? null
          : User.fromJson(json['currentBorrower'] as Map<String, dynamic>),
      dueDate: json['dueDate'] == null
          ? null
          : DateTime.parse(json['dueDate'] as String),
      returnDate: json['returnDate'] == null
          ? null
          : DateTime.parse(json['returnDate'] as String),
      queueList: (json['queueList'] as List<dynamic>?)
          ?.map((e) => User.fromJson(e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$BookToJson(Book instance) => <String, dynamic>{
      'bookId': instance.bookId,
      'createdAt': instance.createdAt?.toIso8601String(),
      'bookStatusId': _$EnumBookStatusEnumMap[instance.bookStatusId]!,
      'name': instance.name,
      'ownerId': instance.ownerId,
      'author': instance.author,
      'genre': _$EnumGenreEnumMap[instance.genre],
      'description': instance.description,
      'url': instance.url,
      'image': instance.image,
      'updatedAt': instance.updatedAt?.toIso8601String(),
      'owner': instance.owner,
      'currentLoanStatus': _$EnumLoanStatusEnumMap[instance.currentLoanStatus],
      'currentBorrower': instance.currentBorrower,
      'dueDate': instance.dueDate?.toIso8601String(),
      'returnDate': instance.returnDate?.toIso8601String(),
      'queueList': instance.queueList,
    };

const _$EnumBookStatusEnumMap = {
  EnumBookStatus.available: 'available',
  EnumBookStatus.inactive: 'inactive',
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

const _$EnumLoanStatusEnumMap = {
  EnumLoanStatus.pending: 'pending',
  EnumLoanStatus.approved: 'approved',
  EnumLoanStatus.rejected: 'rejected',
  EnumLoanStatus.received: 'received',
  EnumLoanStatus.returned: 'returned',
  EnumLoanStatus.overdue: 'overdue',
};
