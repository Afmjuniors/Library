import 'package:flutter/material.dart';

enum EnumBookStatus {
  available(1),
  inactive(2);

  const EnumBookStatus(this.value);
  final int value;

  static EnumBookStatus fromValue(int value) {
    return EnumBookStatus.values.firstWhere(
      (e) => e.value == value,
      orElse: () => EnumBookStatus.available,
    );
  }
}

enum EnumGenre {
  action(1),
  adventure(2),
  comedy(3),
  drama(4),
  fantasy(5),
  horror(6),
  mystery(7),
  romance(8),
  scienceFiction(9),
  thriller(10),
  western(11);

  const EnumGenre(this.value);
  final int value;

  static EnumGenre fromValue(int value) {
    return EnumGenre.values.firstWhere(
      (e) => e.value == value,
      orElse: () => EnumGenre.action,
    );
  }

  String get displayName {
    switch (this) {
      case EnumGenre.action:
        return 'Action';
      case EnumGenre.adventure:
        return 'Adventure';
      case EnumGenre.comedy:
        return 'Comedy';
      case EnumGenre.drama:
        return 'Drama';
      case EnumGenre.fantasy:
        return 'Fantasy';
      case EnumGenre.horror:
        return 'Horror';
      case EnumGenre.mystery:
        return 'Mystery';
      case EnumGenre.romance:
        return 'Romance';
      case EnumGenre.scienceFiction:
        return 'Science Fiction';
      case EnumGenre.thriller:
        return 'Thriller';
      case EnumGenre.western:
        return 'Western';
    }
  }
}

enum EnumLoanQueueStatus {
  waiting(1),
  inProgress(2),
  completed(3);

  const EnumLoanQueueStatus(this.value);
  final int value;

  static EnumLoanQueueStatus fromValue(int value) {
    return EnumLoanQueueStatus.values.firstWhere(
      (e) => e.value == value,
      orElse: () => EnumLoanQueueStatus.waiting,
    );
  }
}

enum EnumLoanStatus {
  pending(1),
  approved(2),
  rejected(3),
  received(4),
  returned(5),
  overdue(6);

  const EnumLoanStatus(this.value);
  final int value;

  static EnumLoanStatus fromValue(int value) {
    return EnumLoanStatus.values.firstWhere(
      (e) => e.value == value,
      orElse: () => EnumLoanStatus.pending,
    );
  }

  String get displayName {
    switch (this) {
      case EnumLoanStatus.pending:
        return 'Pending';
      case EnumLoanStatus.approved:
        return 'Approved';
      case EnumLoanStatus.rejected:
        return 'Rejected';
      case EnumLoanStatus.received:
        return 'Received';
      case EnumLoanStatus.returned:
        return 'Returned';
      case EnumLoanStatus.overdue:
        return 'Overdue';
    }
  }

  Color get color {
    switch (this) {
      case EnumLoanStatus.pending:
        return Colors.orange;
      case EnumLoanStatus.approved:
        return Colors.green;
      case EnumLoanStatus.rejected:
        return Colors.red;
      case EnumLoanStatus.received:
        return Colors.blue;
      case EnumLoanStatus.returned:
        return Colors.grey;
      case EnumLoanStatus.overdue:
        return Colors.red;
    }
  }
}

enum EnumUserStatus {
  active(1),
  desactive(2),
  inapt(3);

  const EnumUserStatus(this.value);
  final int value;

  static EnumUserStatus fromValue(int value) {
    return EnumUserStatus.values.firstWhere(
      (e) => e.value == value,
      orElse: () => EnumUserStatus.active,
    );
  }
}

enum EnumUserRole {
  admin,
  leader,
  normal;

  String get displayName {
    switch (this) {
      case EnumUserRole.admin:
        return 'Admin';
      case EnumUserRole.leader:
        return 'Leader';
      case EnumUserRole.normal:
        return 'Member';
    }
  }
} 