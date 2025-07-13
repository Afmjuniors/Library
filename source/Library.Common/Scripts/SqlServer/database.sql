CREATE TABLE [USERS] (
  [user_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [email] varchar(255) NOT NULL,
  [name] varchar(255) NOT NULL,
  [phone] varchar(255) NULL,
  [address] varchar(255) null,
  [additional_info] varchar(255) null,
  [user_status_id] int NOT NULL, 
  [password] varchar(255) NOT NULL,
  [image] varchar(255) null,
  [created_at] datetime NOT NULL,
  [updated_at] datetime null,
  [birthday] datetime null,
  [language_id] int not null
);

CREATE TABLE [COUNTRIES](
	[country_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[prefix_number] [int] NOT NULL,
 CONSTRAINT [PK_COUNTRIES] PRIMARY KEY CLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [LANGUAGES](
	[language_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[code] [varchar](50) NULL,
	[country_id] [int] NOT NULL,
 CONSTRAINT [PK_LANGUAGES] PRIMARY KEY CLUSTERED 
(
	[language_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [USERS_STATUS] (
  [user_status_id] int PRIMARY KEY,
  [value] varchar(255) NOT NULL
);

CREATE TABLE [ORGANIZATIONS] (
  [organization_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [name] varchar(255) NOT NULL,
  [image] varchar(255) null,
  [description] varchar(255) null,
  [created_at] datetime NOT NULL,
  [created_by] bigint NOT NULL,
  [updated_at] datetime null,
  [updated_by] bigint null
);

CREATE TABLE [ORGANIZATIONS_USERS] (
  [organization_user_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [user_id] bigint NOT NULL,
  [organization_id] bigint NOT NULL
);

CREATE TABLE [BOOKS] (
  [book_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [name] varchar(255) NOT NULL,
  [author] varchar(255) null,
  [book_status_id] int NOT NULL,
  [owner_id] bigint NOT NULL,
  [image] varchar(255) null,
  [genre] varchar(255) null,
  [description] varchar(255) null,
  [url] varchar(255) null,
  [created_at] datetime NOT NULL,
  [updated_at] datetime null
);

CREATE TABLE [BOOKS_STATUS] (
  [book_status_id] int PRIMARY KEY,
  [value] varchar(255) NOT NULL
);
CREATE TABLE [BOOKS_GENRES] (
  [genre_id] int PRIMARY KEY,
  [value] varchar(255) NOT NULL
);

CREATE TABLE [LOANS] (
  [loan_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [borrower_id] bigint NOT NULL,
  [lender_id] bigint NOT NULL,
  [book_id] bigint NOT NULL,
  [loan_status_id] int NOT NULL,
  [due_date] datetime null,
  [received_date] datetime null,
  [returned_date] datetime null,
  [created_at] datetime NOT NULL,
  [created_by] bigint NOT NULL,
  [updated_at] datetime null,
  [updated_by] bigint null
);

CREATE TABLE [LOANS_STATUS] (
  [loan_status_id] int PRIMARY KEY,
  [value] varchar(255) NOT NULL
);
CREATE TABLE [LOANS_QUEUES_STATUS] (
  [loan_queue_status_id] int PRIMARY KEY,
  [value] varchar(255) NOT NULL
);

CREATE TABLE [LOANS_QUEUES] (
  [queue_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [book_id] bigint NOT NULL,
  [lender_id] bigint NOT NULL,
  [borrower_id] bigint NOT NULL,
  [previous_id] bigint null,
  [expected_loan_date] datetime null,
  [created_at] datetime NOT NULL,
  [updated_at] datetime null,
  [status_queue_laon] int NOT NULL
);

CREATE TABLE [RATINGS] (
  [rating_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [user_id] bigint NOT NULL,
  [book_id] bigint NOT NULL,
  [rate] int NOT NULL,
  [comments] varchar(255) null,
  [created_at] datetime NOT NULL,
  [updated_at] datetime null
);

CREATE TABLE [RULES] (
  [rule_id] int PRIMARY KEY,
  [value] varchar(255) NOT NULL
);

CREATE TABLE [REGULATIONS] (
  [regulation_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [organization_id] bigint NOT NULL,
  [rule_id] int NOT NULL,
  [value] varchar(255) NOT NULL,
  [updated_at] datetime null,
  [updated_by] bigint null

);

ALTER TABLE [USERS] ADD FOREIGN KEY ([user_status_id]) REFERENCES [USERS_STATUS] ([user_status_id]);

ALTER TABLE [BOOKS] ADD FOREIGN KEY ([book_status_id]) REFERENCES [BOOKS_STATUS] ([book_status_id]);

ALTER TABLE [BOOKS] ADD FOREIGN KEY ([owner_id]) REFERENCES [USERS] ([user_id]);

ALTER TABLE [LOANS] ADD FOREIGN KEY ([borrower_id]) REFERENCES [USERS] ([user_id]);

ALTER TABLE [LOANS] ADD FOREIGN KEY ([lender_id]) REFERENCES [USERS] ([user_id]);

ALTER TABLE [LOANS] ADD FOREIGN KEY ([book_id]) REFERENCES [BOOKS] ([book_id]);

ALTER TABLE [LOANS] ADD FOREIGN KEY ([loan_status_id]) REFERENCES [LOANS_STATUS] ([loan_status_id]);

ALTER TABLE [LOANS_QUEUES] ADD FOREIGN KEY ([book_id]) REFERENCES [BOOKS] ([book_id]);

ALTER TABLE [LOANS_QUEUES] ADD FOREIGN KEY ([lender_id]) REFERENCES [USERS] ([user_id]);

ALTER TABLE [LOANS_QUEUES] ADD FOREIGN KEY ([borrower_id]) REFERENCES [USERS] ([user_id]);

ALTER TABLE [LOANS_QUEUES] ADD FOREIGN KEY ([previous_id]) REFERENCES [LOANS_QUEUES] ([queue_id]);

ALTER TABLE [RATINGS] ADD FOREIGN KEY ([user_id]) REFERENCES [USERS] ([user_id]);

ALTER TABLE [RATINGS] ADD FOREIGN KEY ([book_id]) REFERENCES [BOOKS] ([book_id]);

ALTER TABLE [ORGANIZATIONS_USERS] ADD FOREIGN KEY ([user_id]) REFERENCES [USERS] ([user_id]);

ALTER TABLE [ORGANIZATIONS_USERS] ADD FOREIGN KEY ([organization_id]) REFERENCES [ORGANIZATIONS] ([organization_id]);

ALTER TABLE [REGULATIONS] ADD FOREIGN KEY ([organization_id]) REFERENCES [ORGANIZATIONS] ([organization_id]);

ALTER TABLE [REGULATIONS] ADD FOREIGN KEY ([rule_id]) REFERENCES [RULES] ([rule_id]);
