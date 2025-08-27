import 'package:json_annotation/json_annotation.dart';

part 'book_model.g.dart';

@JsonSerializable()
class BookModel {
  @JsonKey(name: 'bookId')
  final int bookId;
  
  final String name;
  final String author;
  final int genre;
  final String description;
  final String url;
  final String image;
  
  @JsonKey(name: 'bookStatusId')
  final int bookStatusId;
  
  @JsonKey(name: 'ownerId')
  final int ownerId;
  
  final String createdAt;
  final String? updatedAt;

  @JsonKey(name: 'visibilitySettings')
  final BookVisibilitySettings? visibilitySettings;

  @JsonKey(name: 'loanInfo')
  final BookLoanInfo? loanInfo;

  @JsonKey(name: 'ownerInfo')
  final BookOwnerInfo? ownerInfo;

  const BookModel({
    required this.bookId,
    required this.name,
    required this.author,
    required this.genre,
    required this.description,
    required this.url,
    required this.image,
    required this.bookStatusId,
    required this.ownerId,
    required this.createdAt,
    this.updatedAt,
    this.visibilitySettings,
    this.loanInfo,
    this.ownerInfo,
  });

  factory BookModel.fromJson(Map<String, dynamic> json) => _$BookModelFromJson(json);
  Map<String, dynamic> toJson() => _$BookModelToJson(this);

  BookModel copyWith({
    int? bookId,
    String? name,
    String? author,
    int? genre,
    String? description,
    String? url,
    String? image,
    int? bookStatusId,
    int? ownerId,
    String? createdAt,
    String? updatedAt,
    BookVisibilitySettings? visibilitySettings,
    BookLoanInfo? loanInfo,
    BookOwnerInfo? ownerInfo,
  }) {
    return BookModel(
      bookId: bookId ?? this.bookId,
      name: name ?? this.name,
      author: author ?? this.author,
      genre: genre ?? this.genre,
      description: description ?? this.description,
      url: url ?? this.url,
      image: image ?? this.image,
      bookStatusId: bookStatusId ?? this.bookStatusId,
      ownerId: ownerId ?? this.ownerId,
      createdAt: createdAt ?? this.createdAt,
      updatedAt: updatedAt ?? this.updatedAt,
      visibilitySettings: visibilitySettings ?? this.visibilitySettings,
      loanInfo: loanInfo ?? this.loanInfo,
      ownerInfo: ownerInfo ?? this.ownerInfo,
    );
  }
}

@JsonSerializable()
class BookVisibilitySettings {
  @JsonKey(name: 'isPublic')
  final bool isPublic;
  
  @JsonKey(name: 'visibleOrganizations')
  final List<int> visibleOrganizations;
  
  @JsonKey(name: 'hiddenOrganizations')
  final List<int> hiddenOrganizations;

  const BookVisibilitySettings({
    required this.isPublic,
    required this.visibleOrganizations,
    required this.hiddenOrganizations,
  });

  factory BookVisibilitySettings.fromJson(Map<String, dynamic> json) => 
      _$BookVisibilitySettingsFromJson(json);
  Map<String, dynamic> toJson() => _$BookVisibilitySettingsToJson(this);
}

@JsonSerializable()
class BookLoanInfo {
  @JsonKey(name: 'borrowerId')
  final int borrowerId;
  
  @JsonKey(name: 'borrowerName')
  final String borrowerName;
  
  @JsonKey(name: 'borrowerEmail')
  final String borrowerEmail;
  
  @JsonKey(name: 'loanDate')
  final String loanDate;
  
  @JsonKey(name: 'returnDate')
  final String returnDate;
  
  @JsonKey(name: 'isOverdue')
  final bool isOverdue;

  const BookLoanInfo({
    required this.borrowerId,
    required this.borrowerName,
    required this.borrowerEmail,
    required this.loanDate,
    required this.returnDate,
    required this.isOverdue,
  });

  factory BookLoanInfo.fromJson(Map<String, dynamic> json) => 
      _$BookLoanInfoFromJson(json);
  Map<String, dynamic> toJson() => _$BookLoanInfoToJson(this);
}

@JsonSerializable()
class BookOwnerInfo {
  final String name;
  final String email;
  final String? image;
  final String? organization;
  final String? organizationRole;
  
  @JsonKey(name: 'totalBooks')
  final int totalBooks;
  
  @JsonKey(name: 'booksLent')
  final int booksLent;
  
  @JsonKey(name: 'booksAvailable')
  final int booksAvailable;

  const BookOwnerInfo({
    required this.name,
    required this.email,
    this.image,
    this.organization,
    this.organizationRole,
    required this.totalBooks,
    required this.booksLent,
    required this.booksAvailable,
  });

  factory BookOwnerInfo.fromJson(Map<String, dynamic> json) => 
      _$BookOwnerInfoFromJson(json);
  Map<String, dynamic> toJson() => _$BookOwnerInfoToJson(this);
} 