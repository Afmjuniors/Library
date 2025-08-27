// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'book_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

BookModel _$BookModelFromJson(Map<String, dynamic> json) => BookModel(
      bookId: (json['bookId'] as num).toInt(),
      name: json['name'] as String,
      author: json['author'] as String,
      genre: (json['genre'] as num).toInt(),
      description: json['description'] as String,
      url: json['url'] as String,
      image: json['image'] as String,
      bookStatusId: (json['bookStatusId'] as num).toInt(),
      ownerId: (json['ownerId'] as num).toInt(),
      createdAt: json['createdAt'] as String,
      updatedAt: json['updatedAt'] as String?,
      visibilitySettings: json['visibilitySettings'] == null
          ? null
          : BookVisibilitySettings.fromJson(
              json['visibilitySettings'] as Map<String, dynamic>),
      loanInfo: json['loanInfo'] == null
          ? null
          : BookLoanInfo.fromJson(json['loanInfo'] as Map<String, dynamic>),
      ownerInfo: json['ownerInfo'] == null
          ? null
          : BookOwnerInfo.fromJson(json['ownerInfo'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$BookModelToJson(BookModel instance) => <String, dynamic>{
      'bookId': instance.bookId,
      'name': instance.name,
      'author': instance.author,
      'genre': instance.genre,
      'description': instance.description,
      'url': instance.url,
      'image': instance.image,
      'bookStatusId': instance.bookStatusId,
      'ownerId': instance.ownerId,
      'createdAt': instance.createdAt,
      'updatedAt': instance.updatedAt,
      'visibilitySettings': instance.visibilitySettings,
      'loanInfo': instance.loanInfo,
      'ownerInfo': instance.ownerInfo,
    };

BookVisibilitySettings _$BookVisibilitySettingsFromJson(
        Map<String, dynamic> json) =>
    BookVisibilitySettings(
      isPublic: json['isPublic'] as bool,
      visibleOrganizations: (json['visibleOrganizations'] as List<dynamic>)
          .map((e) => (e as num).toInt())
          .toList(),
      hiddenOrganizations: (json['hiddenOrganizations'] as List<dynamic>)
          .map((e) => (e as num).toInt())
          .toList(),
    );

Map<String, dynamic> _$BookVisibilitySettingsToJson(
        BookVisibilitySettings instance) =>
    <String, dynamic>{
      'isPublic': instance.isPublic,
      'visibleOrganizations': instance.visibleOrganizations,
      'hiddenOrganizations': instance.hiddenOrganizations,
    };

BookLoanInfo _$BookLoanInfoFromJson(Map<String, dynamic> json) => BookLoanInfo(
      borrowerId: (json['borrowerId'] as num).toInt(),
      borrowerName: json['borrowerName'] as String,
      borrowerEmail: json['borrowerEmail'] as String,
      loanDate: json['loanDate'] as String,
      returnDate: json['returnDate'] as String,
      isOverdue: json['isOverdue'] as bool,
    );

Map<String, dynamic> _$BookLoanInfoToJson(BookLoanInfo instance) =>
    <String, dynamic>{
      'borrowerId': instance.borrowerId,
      'borrowerName': instance.borrowerName,
      'borrowerEmail': instance.borrowerEmail,
      'loanDate': instance.loanDate,
      'returnDate': instance.returnDate,
      'isOverdue': instance.isOverdue,
    };

BookOwnerInfo _$BookOwnerInfoFromJson(Map<String, dynamic> json) =>
    BookOwnerInfo(
      name: json['name'] as String,
      email: json['email'] as String,
      image: json['image'] as String?,
      organization: json['organization'] as String?,
      organizationRole: json['organizationRole'] as String?,
      totalBooks: (json['totalBooks'] as num).toInt(),
      booksLent: (json['booksLent'] as num).toInt(),
      booksAvailable: (json['booksAvailable'] as num).toInt(),
    );

Map<String, dynamic> _$BookOwnerInfoToJson(BookOwnerInfo instance) =>
    <String, dynamic>{
      'name': instance.name,
      'email': instance.email,
      'image': instance.image,
      'organization': instance.organization,
      'organizationRole': instance.organizationRole,
      'totalBooks': instance.totalBooks,
      'booksLent': instance.booksLent,
      'booksAvailable': instance.booksAvailable,
    };
