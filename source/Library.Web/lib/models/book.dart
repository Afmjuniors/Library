import 'package:json_annotation/json_annotation.dart';
import 'enums.dart';
import 'user.dart';

part 'book.g.dart';

@JsonSerializable()
class Book {
  @JsonKey(name: 'bookId')
  final int? bookId;
  
  @JsonKey(name: 'createdAt')
  final DateTime? createdAt;
  
  @JsonKey(name: 'bookStatusId')
  final EnumBookStatus bookStatusId;
  
  @JsonKey(name: 'name')
  final String name;
  
  @JsonKey(name: 'ownerId')
  final int ownerId;
  
  @JsonKey(name: 'author')
  final String? author;
  
  @JsonKey(name: 'genre')
  final EnumGenre? genre;
  
  @JsonKey(name: 'description')
  final String? description;
  
  @JsonKey(name: 'url')
  final String? url;
  
  @JsonKey(name: 'image')
  final String? image;
  
  @JsonKey(name: 'updatedAt')
  final DateTime? updatedAt;
  
  @JsonKey(name: 'owner')
  final User? owner;

  // Additional fields for the app
  EnumLoanStatus? currentLoanStatus;
  User? currentBorrower;
  DateTime? dueDate;
  DateTime? returnDate;
  List<User>? queueList;

  Book({
    this.bookId,
    this.createdAt,
    required this.bookStatusId,
    required this.name,
    required this.ownerId,
    this.author,
    this.genre,
    this.description,
    this.url,
    this.image,
    this.updatedAt,
    this.owner,
    this.currentLoanStatus,
    this.currentBorrower,
    this.dueDate,
    this.returnDate,
    this.queueList,
  });

  factory Book.fromJson(Map<String, dynamic> json) => _$BookFromJson(json);
  Map<String, dynamic> toJson() => _$BookToJson(this);

  Book copyWith({
    int? bookId,
    DateTime? createdAt,
    EnumBookStatus? bookStatusId,
    String? name,
    int? ownerId,
    String? author,
    EnumGenre? genre,
    String? description,
    String? url,
    String? image,
    DateTime? updatedAt,
    User? owner,
    EnumLoanStatus? currentLoanStatus,
    User? currentBorrower,
    DateTime? dueDate,
    DateTime? returnDate,
    List<User>? queueList,
  }) {
    return Book(
      bookId: bookId ?? this.bookId,
      createdAt: createdAt ?? this.createdAt,
      bookStatusId: bookStatusId ?? this.bookStatusId,
      name: name ?? this.name,
      ownerId: ownerId ?? this.ownerId,
      author: author ?? this.author,
      description: description ?? this.description,
      url: url ?? this.url,
      image: image ?? this.image,
      updatedAt: updatedAt ?? this.updatedAt,
      owner: owner ?? this.owner,
      currentLoanStatus: currentLoanStatus ?? this.currentLoanStatus,
      currentBorrower: currentBorrower ?? this.currentBorrower,
      dueDate: dueDate ?? this.dueDate,
      returnDate: returnDate ?? this.returnDate,
      queueList: queueList ?? this.queueList,
    );
  }

  bool get isAvailable => bookStatusId == EnumBookStatus.available;
  bool get isLent => currentLoanStatus == EnumLoanStatus.received;
  bool get isOverdue => dueDate != null && DateTime.now().isAfter(dueDate!) && isLent;
  bool get hasQueue => queueList != null && queueList!.isNotEmpty;
  
  String get statusDisplay {
    if (!isAvailable) return 'Inactive';
    if (isOverdue) return 'Overdue';
    if (isLent) return 'Lent';
    return 'Available';
  }

  String get genreDisplay => genre?.displayName ?? 'Not specified';
  
  String get authorDisplay => author?.isNotEmpty == true ? author! : 'Unknown Author';
} 