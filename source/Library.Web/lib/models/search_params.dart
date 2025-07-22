import 'package:json_annotation/json_annotation.dart';
import 'enums.dart';

part 'search_params.g.dart';

@JsonSerializable()
class SearchBookParams {
  @JsonKey(name: 'keyWord')
  final String? keyWord;
  
  @JsonKey(name: 'userId')
  final int? userId;
  
  @JsonKey(name: 'ownerId')
  final int? ownerId;
  
  @JsonKey(name: 'organizationId')
  final int? organizationId;
  
  @JsonKey(name: 'userName')
  final String? userName;
  
  @JsonKey(name: 'author')
  final String? author;
  
  @JsonKey(name: 'genre')
  final EnumGenre? genre;
  
  @JsonKey(name: 'bookStatus')
  final EnumBookStatus? bookStatus;
  
  @JsonKey(name: 'loanStatus')
  final EnumLoanStatus? loanStatus;

  SearchBookParams({
    this.keyWord,
    this.userId,
    this.ownerId,
    this.organizationId,
    this.userName,
    this.author,
    this.genre,
    this.bookStatus,
    this.loanStatus,
  });

  factory SearchBookParams.fromJson(Map<String, dynamic> json) => _$SearchBookParamsFromJson(json);
  Map<String, dynamic> toJson() => _$SearchBookParamsToJson(this);

  SearchBookParams copyWith({
    String? keyWord,
    int? userId,
    int? ownerId,
    int? organizationId,
    String? userName,
    String? author,
    EnumGenre? genre,
    EnumBookStatus? bookStatus,
    EnumLoanStatus? loanStatus,
  }) {
    return SearchBookParams(
      keyWord: keyWord ?? this.keyWord,
      userId: userId ?? this.userId,
      ownerId: ownerId ?? this.ownerId,
      organizationId: organizationId ?? this.organizationId,
      userName: userName ?? this.userName,
      author: author ?? this.author,
      genre: genre ?? this.genre,
      bookStatus: bookStatus ?? this.bookStatus,
      loanStatus: loanStatus ?? this.loanStatus,
    );
  }

  bool get hasFilters {
    return keyWord?.isNotEmpty == true ||
           userId != null ||
           ownerId != null ||
           organizationId != null ||
           userName?.isNotEmpty == true ||
           author?.isNotEmpty == true ||
           genre != null ||
           bookStatus != null ||
           loanStatus != null;
  }

  SearchBookParams clear() {
    return SearchBookParams();
  }
} 