enum Genre {
  action(1, 'Action'),
  adventure(2, 'Adventure'),
  comedy(3, 'Comedy'),
  drama(4, 'Drama'),
  fantasy(5, 'Fantasy'),
  horror(6, 'Horror'),
  mystery(7, 'Mystery'),
  romance(8, 'Romance'),
  scienceFiction(9, 'ScienceFiction'),
  thriller(10, 'Thriller'),
  western(11, 'Western');

  const Genre(this.id, this.name);
  final int id;
  final String name;

  static Genre fromId(int id) {
    return Genre.values.firstWhere((genre) => genre.id == id);
  }

  static String getText(int genreId) {
    try {
      return fromId(genreId).name;
    } catch (e) {
      return 'Desconhecido';
    }
  }
}

enum BookStatus {
  available(1, 'Disponível', 0xFF4CAF50),
  borrowed(2, 'Emprestado', 0xFFFF9800),
  reserved(3, 'Reservado', 0xFF2196F3);

  const BookStatus(this.id, this.name, this.color);
  final int id;
  final String name;
  final int color;

  static BookStatus fromId(int id) {
    return BookStatus.values.firstWhere((status) => status.id == id);
  }

  static String getText(int statusId) {
    try {
      return fromId(statusId).name;
    } catch (e) {
      return 'Desconhecido';
    }
  }

  static int getColor(int statusId) {
    try {
      return fromId(statusId).color;
    } catch (e) {
      return 0xFF999999;
    }
  }
}

enum MeetingFrequency {
  daily('daily'),
  weekly('weekly'),
  biweekly('biweekly'),
  monthly('monthly'),
  na('na');

  const MeetingFrequency(this.value);
  final String value;

  static MeetingFrequency fromValue(String value) {
    return MeetingFrequency.values.firstWhere((freq) => freq.value == value);
  }
}

enum MeetingDay {
  monday('monday'),
  tuesday('tuesday'),
  wednesday('wednesday'),
  thursday('thursday'),
  friday('friday'),
  saturday('saturday'),
  sunday('sunday');

  const MeetingDay(this.value);
  final String value;

  static MeetingDay fromValue(String value) {
    return MeetingDay.values.firstWhere((day) => day.value == value);
  }
}

enum UserRole {
  admin('Admin'),
  leader('Leader'),
  member('Member');

  const UserRole(this.value);
  final String value;

  static UserRole fromValue(String value) {
    return UserRole.values.firstWhere((role) => role.value == value);
  }
} 