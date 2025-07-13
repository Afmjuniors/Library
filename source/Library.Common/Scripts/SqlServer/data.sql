
INSERT INTO countries (name, prefix_number) values ('Brasil', 55); 
INSERT INTO countries (name, prefix_number) values ('Estados unidos', 60); 

INSERT INTO  LANGUAGES (NAME, CODE, COUNTRY_ID) VALUES ('Português', 'pt-BR', 1); 
INSERT INTO  LANGUAGES (NAME, CODE, COUNTRY_ID) VALUES ('Inglês', 'en-US', 2); 

INSERT INTO  [BOOKS_STATUS] ([book_status_id], value ) VALUES (1, 'Available'); 
INSERT INTO  [BOOKS_STATUS] ([book_status_id], value) VALUES (2, 'Inactive'); 

INSERT INTO  [BOOKS_GENREs] ([genre_id], value ) VALUES 
(1, 'Action'),
(2, 'Adventure'),
(3, 'Comedy'),
(4, 'Drama'),
(5, 'Fantasy'),
(6, 'Horror'),
(7, 'Mystery'),
(8, 'Romance'),
(9, 'ScienceFiction'),
(10, 'Thriller'),
(11, 'Western');

INSERT INTO  [LOANS_STATUS] ([loan_status_id], value ) VALUES 
(1, 'Waiting'),
(2, 'InProgress'),
(3, 'Completed');

INSERT INTO  [LOANS_QUEUES_STATUS] ([loan_queue_status_id], value ) VALUES 
(1, 'Pending'),
(2, 'Approved'),
(3, 'Rejected'),
(4, 'Received'),
(5, 'Returned'),
(6, 'Overdue');

INSERT INTO  [RULES] ([rule_id], value ) VALUES 
(1, 'RepeatedMeeting'),
(2, 'DayOfMeatting'),
(3, 'DaysToReady');

INSERT INTO  [USERS_STATUS] ([user_status_id], value ) VALUES 
(1, 'Active'),
(2, 'Desactive'),
(3, 'Inapt');
 
insert into users (email, [name], [user_status_id],[password],[created_at], language_id) values ('afmjuniors@gmail.com', 'Alexandre Machado', 1,'123456',getdate(), 1); 



