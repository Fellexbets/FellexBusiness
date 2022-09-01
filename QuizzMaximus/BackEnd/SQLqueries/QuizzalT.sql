USE master;
GO
if exists (select * from sysdatabases where name='QuizzalT')
        drop database QuizzalT
GO

CREATE DATABASE QuizzalT
GO

USE "QuizzalT";

CREATE TABLE Users (
    UserId                 int not null IDENTITY(1,1),
	Username      nvarchar(25) not null,
	Email         nvarchar(50) not null,
	"Role"        nvarchar(20) not null,
	DateCreated       datetime     null CONSTRAINT "DF_Users_DateCreated" DEFAULT (getdate()),
	"Password"    nvarchar(50) not null,
	PasswordSalt nvarchar(100)     null,
	Photo image null,
	Token        nvarchar(200)     null,
	CONSTRAINT "PK_Users" PRIMARY KEY CLUSTERED ("UserId"),
	CONSTRAINT "UQ_Users_Username" UNIQUE("Username"),
	CONSTRAINT "UQ_Users_Email"    UNIQUE("Email")
	);
CREATE INDEX "Username" ON "dbo"."Users"("Username");
CREATE INDEX "Email"    ON "dbo"."Users"("Email");
CREATE INDEX "Password" ON "dbo"."Users"("Password");
GO

CREATE TABLE UserRelations (
    UserId           int not null,
	RelatedUserId    int not null,
	RelationId       int not null,
	DateCreated datetime     null CONSTRAINT "DF_UserRelations_DateCreated" DEFAULT (getdate()),
	CONSTRAINT "PK_UserRelations" PRIMARY KEY CLUSTERED ("UserId", "RelatedUserId"),
	CONSTRAINT "FK1_UserRelations_Users" FOREIGN KEY ("UserId")        REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK2_UserRelations_Users" FOREIGN KEY ("RelatedUserId") REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "CK_UserId_RelatedUserId" CHECK (UserId != RelatedUserId)
	);
GO

CREATE TABLE Relations (
	RelationId            int not null IDENTITY(1,1),
	RelationName nvarchar(15) not null,
	CONSTRAINT "PK_Relations" PRIMARY KEY CLUSTERED ("RelationId"),
	);
GO

CREATE TABLE Themes (
    ThemeId            int not null IDENTITY(1,1),
	ThemeName nvarchar(15) not null,
	CONSTRAINT "PK_Themes" PRIMARY KEY CLUSTERED ("ThemeId"),
	CONSTRAINT "UQ_Themes_ThemeName" UNIQUE("ThemeName")   
	);
CREATE INDEX ThemeName ON "dbo".Themes("ThemeName");
GO

CREATE TABLE Achievements (
    ThemeId      int not null,
	UserId       int not null,
	gainedPoints int not null CONSTRAINT "DF_Achievements_Level" DEFAULT (0),
	CONSTRAINT "PK_Achievements" PRIMARY KEY CLUSTERED ("ThemeId", "UserId"),
	CONSTRAINT "FK_Achievements_Themes" FOREIGN KEY ("ThemeId") REFERENCES "dbo"."Themes" ("ThemeId"),
	CONSTRAINT "FK_Achievements_Users"  FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users" ("UserId")
	);
GO

CREATE TABLE Questions (
    QuestionId             int not null identity(1,1),
	UserId                 int not null,
	ThemeId                int not null,
	QuestionName  nvarchar(30) not null,
	QuestionText nvarchar(100) not null,
	Difficulty             int not null CONSTRAINT "DF_Questions_Difficulty" DEFAULT (1),
	"Status"      nvarchar(15) not null,
	QuestionImage        image     null,
	DateCreated       datetime     null CONSTRAINT "DF_Questions_DateCreated" DEFAULT (getdate()),
	DateEdited        datetime     null,
	CONSTRAINT "PK_Questions" PRIMARY KEY CLUSTERED ("QuestionId"),
	CONSTRAINT "FK_Questions_Users"  FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users"  ("UserId"),
	CONSTRAINT "FK_Questions_Themes" FOREIGN KEY ("ThemeId") REFERENCES "dbo"."Themes" ("ThemeId"),
	CONSTRAINT "CK_Questions_Difficulty" CHECK (Difficulty >= 0 AND (Difficulty <= 10)),
	);
GO

CREATE TABLE Answers (
    AnswerId            int not null IDENTITY(1,1),
	QuestionId          int not null,
	AnswerText nvarchar(50) not null,
	RightAnswer         bit not null,
	DateCreated    datetime     null CONSTRAINT "DF_Answers_DateCreated" DEFAULT (getdate()),
	DateEdited     datetime     null,
	CONSTRAINT "PK_Answers" PRIMARY KEY CLUSTERED ("AnswerId"),
	CONSTRAINT "FK_Answers_Questions" FOREIGN KEY ("QuestionId") REFERENCES "dbo"."Questions" ("QuestionId"),
	);
GO

CREATE TABLE PlayedQuestions (
    PlayedQuestionId  int not null IDENTITY(1,1),
	UserId            int not null,
	QuestionId        int not null,
	GotItRight        bit not null,
	Points            int not null CONSTRAINT "DF_PlayedQuestions_Points" DEFAULT (0),
	DateAnswered datetime     null CONSTRAINT "DF_PlayedQuestions_DateAnswered" DEFAULT (getdate()),
	CONSTRAINT "PK_PlayedQuestions" PRIMARY KEY CLUSTERED ("PlayedQuestionId"),
	CONSTRAINT "FK_PlayedQuestions_Users"     FOREIGN KEY ("UserId")     REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK_PlayedQuestions_Questions" FOREIGN KEY ("QuestionId") REFERENCES "dbo"."Questions" ("QuestionId")
	);
GO

CREATE TABLE Quizzes (
    QuizzId            int not null IDENTITY(1,1),
	UserId             int not null,
	QuizzName nvarchar(20) not null,
	"Status"  nvarchar(15) not null,
	DateCreated   datetime     null CONSTRAINT "DF_PlayedQuestions_DateCreated" DEFAULT (getdate()),
	DateEdited    datetime     null,
	CONSTRAINT "PK_Quizzes" PRIMARY KEY CLUSTERED ("QuizzId"),
	CONSTRAINT "FK_Quizzes_Users" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users" ("UserId"),
	);
GO

CREATE TABLE QuizzQuestions (
    QuizzId    int not null,
	QuestionId int not null,
	CONSTRAINT "PK_QuizzQuestions" PRIMARY KEY CLUSTERED ("QuizzId", "QuestionId"),
	CONSTRAINT "FK_QuizzQuestions_Quizzes"   FOREIGN KEY ("QuizzId")    REFERENCES "dbo"."Quizzes" ("QuizzId"),
	CONSTRAINT "FK_QuizzQuestions_Questions" FOREIGN KEY ("QuestionId") REFERENCES "dbo"."Questions" ("QuestionId")
	);
GO

CREATE TABLE PlayedQuizzes (
    PlayedQuizzId     int not null IDENTITY(1,1),
	UserId            int not null,
	QuizzId           int not null,
	TotalPoints       int not null CONSTRAINT "DF_PlayedQuizzes_TotalPoints" DEFAULT (0),
	DateAnswered datetime     null CONSTRAINT "DF_PlayedQuizzes_DateAnswered" DEFAULT (getdate()),
	CONSTRAINT "PK_PlayedQuizzes" PRIMARY KEY CLUSTERED ("PlayedQuizzId"),
	CONSTRAINT "FK_PlayedQuizzes_Users"   FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK_PlayedQuizzes_Quizzes" FOREIGN KEY ("QuizzId") REFERENCES "dbo"."Quizzes" ("QuizzId"),
	CONSTRAINT "CK_PlayedQuizzes_TotalPoints" CHECK (TotalPoints > 0)
	);
GO

CREATE TRIGGER TR_Questions_UpdateDateEdited ON Questions AFTER UPDATE 
AS
SET NOCOUNT ON;
UPDATE Questions 
SET DateEdited = getdate()
WHERE QuestionId IN (SELECT DISTINCT QuestionId FROM Inserted)
GO

CREATE TRIGGER TR_Answers_UpdateDateEdited ON Answers AFTER UPDATE 
AS
SET NOCOUNT ON;
UPDATE Answers 
SET DateEdited = getdate()
WHERE AnswerId IN (SELECT DISTINCT AnswerId FROM Inserted)
GO

CREATE TRIGGER TR_Quizzes_UpdateDateEdited ON Quizzes AFTER UPDATE 
AS
SET NOCOUNT ON;
UPDATE Quizzes 
SET DateEdited = getdate()
WHERE QuizzId IN (SELECT DISTINCT QuizzId FROM Inserted)
GO

--Users:  UserId / Username / Email / "Role" / DateCreated / "Password" / PasswordSalt / Token
INSERT INTO "Users" 
	(Username, Email, "Role", "Password", PasswordSalt)
VALUES
	('Professor', 'Professor@email.com',     'Admin', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='), --Password: 123qwerty
	(     'Igor',      'Igor@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(      'Ivo',       'Ivo@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(     'Joao',      'Joao@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(   'Rafael',    'Rafael@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(   'Doctor',       'Who@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(      'HAL',       'HAL@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(    'Dummy',     'Dummy@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(  'JohnDoe',     'JohnD@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg==');
;
GO

--Relations: RelationId / RelationName
INSERT INTO "Relations" 
	(RelationName)
VALUES
	('Bff'),
	('Friend'),
	('WatchList'),
	('Blocked');
GO

--UserRelations: UserId / RelatedUserId / RelationId / DateCreated
-- Friends(id:2) and Bff(id:1) must generate a doubled inverted tuple 
INSERT INTO "UserRelations" 
	(UserId, RelatedUserId, RelationId)
VALUES
--Professor (Admin) - no relations here
--Igor
	(2, 3, 1), (2, 5, 1),  --bff
	(2, 4, 2), (2, 6, 2),  --friend
	(2, 7, 3),	--watch
	(2, 8, 4), (2, 9, 4),  --blocked
--Ivo
	(3, 2, 1), (3, 4, 1),
	(3, 5, 2), (3, 7, 2), 
	(3, 8, 3), (3, 9, 3),
	(3, 6, 4), 
--Jo�o
	(4, 5, 1), (4, 3, 1),
	(4, 2, 2), (4, 6, 2),
	(4, 8, 3),
	(4, 7, 4), (4, 9, 4),
--Rafael
	(5, 4, 1), (5, 2, 1),
	(5, 3, 2), (5, 8, 2),
	(5, 7, 3), (5, 9, 3),
	(5, 6, 4),
--Doctor
	(6, 7, 1), (6, 8, 1),
	(6, 4, 2),
	(6, 2, 3),
	(6, 9, 4),
--HAL
	(7, 6, 1), (7, 9, 1),
	(7, 4, 2),
	(7, 3, 3),
	(7, 8, 4),
--Dummy
	(8, 9, 1), (8, 6, 1),
	(8, 5, 2),
	(8, 4, 3),
	(8, 7, 4),
--JohnDoe
	(9, 8, 1), (9, 7, 1),
	(9, 5, 3),
	(9, 6, 4);
GO

--Themes:  ThemeId / ThemeName 
INSERT INTO "Themes" 
	(ThemeName)
VALUES
	('History'),   --1
	('Literature'),--2
	('Art'),       --3
	('Music'),     --4
	('Movies'),    --5
	('Geography'), --6
	('Astronomy'), --7
	('Wild Life'), --8
	('Chemistry'), --9
	('Physics'),   --10
	('Biology'),   --11
	('English'),   --12
	('Math'),      --13
	('Science');   --14
GO



--Questions:  QuestionId / UserId / ThemeId / QuestionName / QuestionText / Difficulty / --Points / Status / DateCreated / DateEdited
INSERT INTO "Questions" 
	(UserId, ThemeId, QuestionName, QuestionText, Difficulty, "Status")
VALUES
	--History 
	( 1,  1,   'Belisarius origin',                                                                      'Where was Belisarius from?', 4, 'Published'), --1
	( 1,  1, 'First Great Pyramid',                                                     'Who built the first Great Pyramid in Cairo?', 3, 'Published'), --2
	( 1,  1,      'Napoleon birth',                                                       'In what year was Napoleon Bonaparte born?', 7, 'Published'), --3
	( 1,  1,   'Peloponnesian War',                'The Peloponnesian War was a conflict in ancient Greece between wich city states?', 5, 'Published'), --4
	( 1,  1,    'Punic Wars Count',                          'The conflict between Rome and Carthage was set in how many Punic Wars?', 7, 'Published'), --5
	( 1,  1,    'Hannibal in Rome', 'How many major battles did Hannibal win against Rome in mainland Italy on the second Punic War?', 8, 'Published'), --6
	( 1,  1,      'Lisbon founder',                                              'Who is the mythical founder of the city of Lisbon?', 6, 'Published'), --7
	( 1,  1,         'Free Brazil',                                                  'In what year did Brazil gain its independence?', 6, 'Published'), --8
	( 1,  1, 'Constantinople fall',                                 'Who conquered Constantinople in 1453 from the Byzantine Empire?', 4, 'Published'), --9
	--Literature
	( 1,  2,   'Dostoevsky naming',                  'What is the name of the main character in Dostoevsky book Crime and Punishment?', 3, 'Published'), --10
	( 1,  2,         'Dune author',                                                                'Who wrote the scifi classic Dune?', 4, 'Published'), --11
	( 1,  2,         '2001 author',                                                                    'Who wrote 2001 Space Odissey?', 2, 'Published'), --12
	( 1,  2,        'Scifi author',                                                                       'Who wrote Minority Report?', 3, 'Published'), --13
	( 1,  2,   'Foundation author',                                                            'Who wrote the scifi novel Foundation?', 6, 'Published'), --14
	( 1,  2,   'Fahrenheit author',                                                        'Who wrote the scifi novel Fahrenheit 451?', 4, 'Published'), --15
	( 1,  2,    'Dystopian author',                                                                           'Who wrote Animal Farm?', 4, 'Published'), --16
	( 1,  2,         '1984 author',                                                                                  'Who wrote 1984?', 3, 'Published'), --17
	( 1,  2,          'LSD author',                                                       'Who wrote the scifi novel Brave New World?', 3, 'Published'), --18
	--Art
	( 1,  3,       'Famous paiter',                                                                            'Who painted Guernica?', 1, 'Published'), --19
	( 1,  3,      'Louvre premier',                       'Who was the first living person to have their art displayed in The Louvre?', 7, 'Published'), --20
	( 1,  3,         'Art deviant',                               'The artist Kandinsky is considered the first for this type of art?', 5, 'Published'), --21
	( 1,  3,          'Claude art',                                    'The artist Claude Monet is associated with wich art movement?', 4, 'Published'), --22
	( 1,  3,           'Art Quote',                  'Who said "When you think about it, department stores are kind of like museums"?', 6, 'Published'), --23
	--English
	( 1,  12,       'English ABC1',															              'What is the plural of man?', 1, 'Published'), --24
	( 1,  12,       'English ABC2',															                   'What does weird mean?', 1, 'Published'), --25
	( 1,  12,       'English ABC3',															         'What is the Antonym of Synonym?', 1, 'Published'), --26
	--Math
	( 1,  13,          'Math ABC1',			         'The Pythagorean Theorem says the two squares of the legs equals the square of...', 2, 'Published'), --27
	( 1,  13,          'Math ABC2', 'A natural number greater than 1 that is not a product of two smaller natural numbers is known as?', 3, 'Published'), --28
	( 1,  13,          'Math ABC3',				    			                                                         'What is 1+1?', 1, 'Published'), --29
	--Science
	( 1,  14,       'Science ABC1',											      'What general name given to Earth natural satellite?', 1, 'Published'), --30
	( 1,  14,       'Science ABC2',									   'What is the name given to lava before leaving the Earth crust?', 2, 'Published'), --31
	( 1,  14,       'Science ABC3',									      'What is the heaviest natural element on the periodic table?', 4, 'Published'), --32
	--Biology
	( 1, 11,       'The innermost',                                                 'What is the name of the innermost part of a cell?', 3, 'Published'); --33

GO

--Answers:  AnswerId / QuestionId / AnswerText / RightAnswer / DateCreated / DateEdited
INSERT INTO "Answers"
	(QuestionId, AnswerText, RightAnswer)
VALUES
	--History 
	( 1,       'Byzantine Empire', 1), ( 1,        'Seleucid Empire', 0), ( 1,                'Bactria', 0), ( 1,                'Parthia', 0), ( 1,               'Carthage', 0),
    ( 2,                  'Khufu', 1), ( 2,                 'Djoser', 0), ( 2,            'Ramesses II', 0), ( 2,            'Tutankhamun', 0), ( 2,             'Hatshepsut', 0), ( 2,                 'Snefru', 0),
	( 3,                   '1769', 1), ( 3,                   '1821', 0), ( 3,                   '1721', 0), ( 3,                   '1749', 0), ( 3,                   '1699', 0), ( 3,                   '1803', 0), ( 3,                   '1783', 0),
	( 4,      'Athens and Sparta', 1), ( 4,      'Athens and Thebes', 0), ( 4,     'Corinth and Rhodes', 0), ( 4,     'Sparta and Corinth', 0), ( 4,        'Athens and Troy', 0),
	( 5,                      '3', 1), ( 5,                      '2', 0), ( 5,                      '4', 0), ( 5,                      '1', 0), ( 5,                      '5', 0),
	( 6,                      '3', 1), ( 6,                      '2', 0), ( 6,                      '4', 0), ( 6,                      '1', 0), ( 6,                      '5', 0),
	( 7,                'Ulysses', 1), ( 7,              'Gilgamesh', 0), ( 7,               'Viriatus', 0), ( 7,                   'Loki', 0), ( 7,               'Achilles', 0),
	( 8,                   '1822', 1), ( 8,                   '1802', 0), ( 8,                   '1788', 0), ( 8,                   '1844', 0), ( 8,                   '1852', 0),
	( 9,         'Ottoman Empire', 1), ( 9,           'Roman Empire', 0), ( 9,           'Golden Horde', 0), ( 9,        'Assyrian Empire', 0), ( 9,        'Egyptian Empire', 0),
	--Literature
	( 10,'Romanovich Raskolnikov', 1), ( 10, 'Semyonovna Marmeladov', 0), ( 10,'Ivanovich Svidrigailov', 0), ( 10,  'Prokofych Razumikhin', 0), ( 10,     'Porfiry Petrovich', 0),
	( 11,         'Frank Herbert', 1), ( 11,       'Arthur C. Clark', 0), ( 11,        'Philip K. Dick', 0), ( 11,          'Isaac Asimov', 0), ( 11,          'Ray Bradbury', 0), ( 11,         'George Orwell', 0), ( 11,         'Aldous Huxley', 0),
	( 12,       'Arthur C. Clark', 1), ( 12,         'Frank Herbert', 0), ( 12,        'Philip K. Dick', 0), ( 12,          'Isaac Asimov', 0), ( 12,          'Ray Bradbury', 0), ( 12,         'George Orwell', 0), ( 12,         'Aldous Huxley', 0),
	( 13,        'Philip K. Dick', 1), ( 13,         'Frank Herbert', 0), ( 13,       'Arthur C. Clark', 0), ( 13,          'Isaac Asimov', 0), ( 13,          'Ray Bradbury', 0), ( 13,         'George Orwell', 0), ( 13,         'Aldous Huxley', 0),
	( 14,          'Isaac Asimov', 1), ( 14,         'Frank Herbert', 0), ( 14,       'Arthur C. Clark', 0), ( 14,        'Philip K. Dick', 0), ( 14,          'Ray Bradbury', 0), ( 14,         'George Orwell', 0), ( 14,         'Aldous Huxley', 0),
	( 15,          'Ray Bradbury', 1), ( 15,         'Frank Herbert', 0), ( 15,       'Arthur C. Clark', 0), ( 15,        'Philip K. Dick', 0), ( 15,          'Isaac Asimov', 0), ( 15,         'George Orwell', 0), ( 15,         'Aldous Huxley', 0),
	( 16,         'George Orwell', 1), ( 16,         'Frank Herbert', 0), ( 16,       'Arthur C. Clark', 0), ( 16,        'Philip K. Dick', 0), ( 16,          'Isaac Asimov', 0), ( 16,          'Ray Bradbury', 0), ( 16,         'Aldous Huxley', 0),
	( 17,         'George Orwell', 1), ( 17,         'Frank Herbert', 0), ( 17,       'Arthur C. Clark', 0), ( 17,        'Philip K. Dick', 0), ( 17,          'Isaac Asimov', 0), ( 17,          'Ray Bradbury', 0), ( 17,         'Aldous Huxley', 0),
	( 18,         'Aldous Huxley', 1), ( 18,         'Frank Herbert', 0), ( 18,       'Arthur C. Clark', 0), ( 18,        'Philip K. Dick', 0), ( 18,          'Isaac Asimov', 0), ( 18,          'Ray Bradbury', 0), ( 18,         'George Orwell', 0),
	--Art
	( 19,         'Pablo Picasso', 1), ( 19,         'Henri Matisse', 0), ( 19,          'Claude Monet', 0), ( 19,         'Salvador Dali', 0), ( 19,        'Marcel Duchamp', 0),
	( 20,        'Georges Braque', 1), ( 20,         'Pablo Picasso', 0), ( 20,       'Diego Vel�zquez', 0), ( 20,             'Rembrandt', 0), ( 20,          'Claude Monet', 0),
	( 21,              'Abstract', 1), ( 21,               'Fauvism', 0), ( 21,          'Suprematisme', 0), ( 21,                'Cubism', 0), ( 21,         'Impressionism', 0),	
	( 22,         'Impressionism', 1), ( 22,               'Fauvism', 0), ( 22,          'Suprematisme', 0), ( 22,                'Cubism', 0), ( 22,              'Abstract', 0),		
	( 23,           'Andy Warhol', 1), ( 23,          'Joseph Beuys', 0), ( 23,        'Marcel Duchamp', 0), ( 23,            'Jeff Koons', 0), ( 23,             'Ai Weiwei', 0),	
	--English
	( 24,                   'Men', 1), ( 24,                  'Mans', 0), ( 24,                'Manies', 0), ( 24,                  'Meen', 0), ( 24,                  'Many', 0),	
	( 25,               'Strange', 1), ( 25,                'Pretty', 0), ( 25,                   'Old', 0), ( 25,             'Exquisite', 0), ( 25,     'None of the other', 0),
	( 26,               'Antonym', 1), ( 26,               'Oposite', 0), ( 26,              'Familiar', 0), ( 26,                'Closer', 0), ( 26,     'None of the other', 0),
	--Math
	( 27,        'The hypotenuse', 1), ( 27,            'Three legs', 0), ( 27,              'Two arms', 0), ( 27,              'The area', 0), ( 27,              'Half leg', 0),
	( 28,          'Prime Number', 1), ( 28,          'Weird Number', 0), ( 28,                    'Pi', 0), ( 28,                   'Log', 0), ( 28,                'Im out', 0),
	( 29,                     '2', 1), ( 29,                    '11', 0), ( 29,                    '-1', 0), ( 29,                     '0', 0), ( 29,                   '-11', 0),
	--Science
	( 30,                  'Moon', 1), ( 30,               'Orbital', 0), ( 30,               'Sputnik', 0), ( 30,                 'Comet', 0), ( 30,                 'Astro', 0),
	( 31,                 'Magma', 1), ( 31,               'Geolava', 0), ( 31,               'Magnite', 0), ( 31,             'Plutonium', 0), ( 31,               'Uranium', 0),
	( 32,               'Uranium', 1), ( 32,           'Plutonium', 0), ( 32,                    'Lead', 0), ( 32,                  'Gold', 0), ( 32,                'Helium', 0),
	--Biology
	( 33,             'Nucleolus', 1), ( 20,         'Mitochondrion', 0), ( 20,             'Cytoplasm', 0), ( 20,              'Membrane', 0), ( 20,              'Ribosome', 0);
GO

--Quizzes:  QuizzId / UserId / QuizzName / "Status" / DateCreated / DateEdited
INSERT INTO "Quizzes" 
	(UserId, QuizzName, "Status")
VALUES
	( 1,   'History Quizz', 'Published'),  --1
	( 1, 'LiteratureQuizz', 'Published'),  --2
	( 1,        'HL Quizz', 'Published'),  --3
	( 1,        '20 Quizz', 'Published'),  --4
	( 1,       'Art Quizz', 'Published'),  --5
	( 1,   'English Quizz', 'Published'),  --6
	( 1,      'Math Quizz', 'Published'),  --7
	( 1,   'Science Quizz', 'Published');  --8
GO

--QuizzQuestions:  QuizzId / QuestionId 
INSERT INTO "QuizzQuestions" 
	(QuizzId, QuestionId)
VALUES
    --History Quizz
	( 1, 1), ( 1, 2), ( 1, 3), ( 1, 4), ( 1, 5), ( 1, 6), ( 1, 7), ( 1, 8), ( 1, 9),
	--Literature
	( 2, 10), ( 2, 11), ( 2, 12), ( 2, 13), ( 2, 14), ( 2, 15), ( 2, 16), ( 2, 17), ( 2, 18),
	--History n Literature
	( 3, 4), ( 3, 5), ( 3, 13), ( 3, 8), ( 3, 9), ( 3, 12), ( 3, 10), ( 3, 18),
	--20 Quizz
	( 4, 1), ( 4, 3), ( 4, 4), ( 4, 5), ( 4, 6), ( 4, 7), ( 4, 9), ( 4, 10), ( 4, 11), ( 4, 12), ( 4, 13), ( 4, 14), ( 4, 15), ( 4, 16), ( 4, 17), ( 4, 18), ( 4, 19), ( 4, 24), ( 4, 26), ( 4, 29),
	--Art Quizz
	( 5, 19), ( 5, 20), ( 5, 21), ( 5, 22), ( 5, 23),
	--English Quizz
	( 6, 24), ( 6, 25), ( 6, 26),
	--Math Quizz
	( 7, 27), ( 7, 28), ( 7, 29),
	--Science Quizz
	( 8, 30), ( 8, 31), ( 8, 32);
GO


--PlayedQuizzes:  PlayedQuizzId / UserId / QuizzId / TotalPoints / DateAnswered
INSERT INTO "PlayedQuizzes"
	(UserId, QuizzId, TotalPoints)
VALUES
    --Igor
	( 2, 1, 340), ( 2, 1, 430), ( 2, 2, 240), ( 2, 2, 320), ( 2, 6, 20),
    --Ivo
	( 3, 1, 410), ( 3, 1, 500), ( 3, 3, 240),
	--Joao
	( 4, 5, 190),
	--Rafael
	( 5, 7,  60),
	--Doctor
	( 6, 8,  70),
	--HAL
    ( 7, 2, 230);
GO



--PlayedQuestions:  PlayedQuestionId / UserId / QuestionId / GotItRight / Points / DateAnswered
INSERT INTO "PlayedQuestions"
	(UserId, QuestionId, GotItRight, Points)
VALUES
	--Igor
	( 2, 1, 1, 40), ( 2, 2, 0, 30), ( 2, 3, 1, 70), ( 2, 4, 1, 50), ( 2, 5, 0, 70), ( 2, 6, 1, 80), ( 2, 7, 1, 60), ( 2, 8, 0, 60), ( 2, 9, 1, 40), --History Quizz
	( 2, 1, 1, 40), ( 2, 2, 1, 30), ( 2, 3, 1, 70), ( 2, 4, 1, 50), ( 2, 5, 0, 70), ( 2, 6, 1, 80), ( 2, 7, 1, 60), ( 2, 8, 1, 60), ( 2, 9, 1, 40), --History Quizz Repeat	
	( 2,10, 1, 30), ( 2,11, 0, 40), ( 2,12, 1, 20), ( 2,13, 1, 30), ( 2,14, 1, 60), ( 2,15, 0, 40), ( 2,16, 1, 40), ( 2,17, 1, 30), ( 2,18, 1, 30), --Literature Quizz
	( 2,10, 1, 30), ( 2,11, 1, 40), ( 2,12, 1, 20), ( 2,13, 1, 30), ( 2,14, 1, 60), ( 2,15, 1, 40), ( 2,16, 1, 40), ( 2,17, 1, 30), ( 2,18, 1, 30), --Literature Quizz
	( 2,24, 0, 10),	( 2,25, 1, 10),	( 2,26, 1, 10), --English Quizz
	--Ivo
	( 3, 1, 1, 40), ( 3, 2, 0, 30), ( 3, 3, 1, 70), ( 3, 4, 1, 50), ( 3, 5, 1, 70), ( 3, 6, 1, 80), ( 3, 7, 1, 60), ( 3, 8, 0, 60), ( 3, 9, 1, 40), --History Quizz
	( 3, 1, 1, 40), ( 3, 2, 1, 30), ( 3, 3, 1, 70), ( 3, 4, 1, 50), ( 3, 5, 1, 70), ( 3, 6, 1, 80), ( 3, 7, 1, 60), ( 3, 8, 1, 60), ( 3, 9, 1, 40), --History Quizz Repeat	
	( 3,10, 1, 30), ( 3,11, 0, 40), ( 3,12, 1, 20), ( 3,13, 1, 30), ( 3,14, 1, 60), ( 3,15, 0, 40), ( 3,16, 1, 40), ( 3,17, 1, 30), ( 3,18, 1, 30), --Literature Quizz
	--Jo�o
	( 4,19, 1, 10), ( 4,20, 1, 70), ( 4,21, 1, 50), ( 4,22, 0, 40), ( 4,23, 1, 60), -- Art Quizz
	--Rafael
	( 5,27, 1, 20), ( 5,28, 1, 30), ( 5,29, 1, 10), --Math Quizz
	--Doctor
	( 6,30, 1, 10), ( 6,31, 1, 20), ( 6,32, 1, 40), --Science Quizz
	--HAL
	( 7,10, 1, 30), ( 7,11, 1, 40), ( 7,12, 1, 20), ( 7,13, 1, 30), ( 7,14, 0, 60), ( 7,15, 1, 40), ( 7,16, 1, 40), ( 7,17, 1, 30), ( 7,18, 0, 30); --Literature Quizz
GO



--Achievements:  ThemeId / UserId / Level
INSERT INTO "Achievements" 
	(ThemeId, UserId, GainedPoints)
VALUES
	--Igor
	( 1, 2, 770), ( 2, 2, 560), ( 12, 2, 20),
	--Ivo
	( 1, 3, 910), ( 2, 3, 240),
	--Jo�o
	( 3, 4, 190),
	--Rafael
	(13, 5,  60),
	--Doctor
	(14, 6,  70),
	--HAL
	( 2, 7,  230);
GO

