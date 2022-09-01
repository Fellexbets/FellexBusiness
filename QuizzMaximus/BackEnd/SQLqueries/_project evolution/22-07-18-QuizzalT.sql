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
	('MyAdmin',  'Admin@email.com',     'Admin', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='), --Password: 123qwerty
	(   'Igor',   'Igor@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(    'Ivo',    'Ivo@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(   'João',   'Joao@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	( 'Rafael', 'Rafael@email.com', 'Moderator', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	('JohnDoe',  'JohnD@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(  'Dummy',  'Dummy@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	(   'HALL',   'HALL@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg=='),
	( 'Doctor',    'Who@email.com',    'Player', 'NgT0qoF6xd9n4mtwr09qevCrN2lkqAPKQsXn8IVBRVc=', 'XDqyvx+AFEVIyU/L/10BAg==');
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
--Admin
	(1, 2, 2),
	(2, 1, 2),
	(1, 3, 2),
	(3, 1, 2),
	(1, 4, 1),
	(4, 1, 1),
	(1, 5, 3),
	(1, 6, 3),
	(1, 7, 4),
--Igor
	(2, 3, 1),
	(3, 2, 1),
	(2, 4, 1),
	(4, 2, 1),
	(2, 5, 3),
	(2, 6, 3),
	(2, 7, 4);
GO

--Themes:  ThemeId / ThemeName 
INSERT INTO "Themes" 
	(ThemeName)
VALUES
	('History'),
	('Literature'),
	('Art'),
	('Music'),
	('Movies'),
	('Geography'),
	('Astronomy'),
	('Wild Life'),
	('Chemistry'),
	('Physics'),
	('Biology');
GO

--Achievements:  ThemeId / UserId / Level
INSERT INTO "Achievements" 
	(ThemeId, UserId, GainedPoints)
VALUES
	( 1, 1, 3),
	( 2, 1, 8),
	( 2, 2, 9),
	( 3, 2, 6),
	( 4, 3, 2),
	( 5, 3, 7),
	( 2, 4, 6),
	( 4, 4, 9);
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
	( 1,  2,   'Dostoevsky naming',                 'What is the name of the main character in Dostoevsky book Crime and Punishment?', 3, 'Published'), --10
	( 1,  2,         'Dune author',                                                               'Who wrote the scifi classic Dune?', 4, 'Published'), --11
	( 1,  2,         '2001 author',                                                                   'Who wrote 2001 Space Odissey?', 2, 'Published'), --12
	( 1,  2,        'Scifi author',                                                                      'Who wrote Minority Report?', 3, 'Published'), --13
	( 1,  2,   'Foundation author',                                                           'Who wrote the scifi novel Foundation?', 6, 'Published'), --14
	( 1,  2,   'Fahrenheit author',                                                       'Who wrote the scifi novel Fahrenheit 451?', 4, 'Published'), --15
	( 1,  2,    'Dystopian author',                                                                          'Who wrote Animal Farm?', 4, 'Published'), --16
	( 1,  2,         '1984 author',                                                                                 'Who wrote 1984?', 3, 'Published'), --17
	( 1,  2,          'LSD author',                                                      'Who wrote the scifi novel Brave New World?', 3, 'Published'), --18
	--Art
	( 1,  3,       'Famous paiter',                                                                           'Who painted Guernica?', 1, 'Published'), --19
	--Biology
	( 1, 11,       'The innermost',                                               'What is the name of the innermost part of a cell?', 3, 'Published'), --20
	--Dummy
	( 1,  1,              'Dummy ',                                                       'Dummy Dummy Dummy Dummy Dummy Dummy Dummy', 3,     'Draft'); --ddd
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
	--Biology
	( 20,             'Nucleolus', 1), ( 20,         'Mitochondrion', 0), ( 20,             'Cytoplasm', 0), ( 20,              'Membrane', 0), ( 20,              'Ribosome', 0);
	--Dummy

GO

--Quizzes:  QuizzId / UserId / QuizzName / "Status" / DateCreated / DateEdited
INSERT INTO "Quizzes" 
	(UserId, QuizzName, "Status")
VALUES
	( 1,   'History Quizz', 'Published'), 
	( 1, 'LiteratureQuizz', 'Published'), 
	( 1,        'HL Quizz', 'Published'), 
	( 1,  'AllSoFar Quizz', 'Published'); 
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
	--All so far
	( 4, 1), ( 4, 2), ( 4, 3), ( 4, 4), ( 4, 5), ( 4, 6), ( 4, 7), ( 4, 8), ( 4, 9), ( 4, 10), ( 4, 11), ( 4, 12), ( 4, 13), ( 4, 14), ( 4, 15), ( 4, 16), ( 4, 17), ( 4, 18), ( 4, 19), ( 4, 20);
GO

/*
--PlayedQuestions:  PlayedQuestionId / UserId / QuestionId / GotItRight / Points / DateAnswered
INSERT INTO "PlayedQuestions"
	(UserId, QuestionId, GotItRight, Points)
VALUES
	( 1, 1, 1, 30),
	( 1, 2, 0,  0),
	( 1, 3, 1, 30),
	( 1, 4, 0,  0),
	( 2, 1, 0,  0),
	( 2, 2, 1, 10),
	( 2, 3, 0,  0),
	( 2, 4, 1, 40),
	( 3, 1, 0,  0),
	( 3, 2, 1, 10),
	( 3, 3, 1, 30),
	( 3, 4, 0,  0),
	( 4, 1, 1, 30),
	( 4, 2, 0,  0),
	( 4, 3, 0,  0),
	( 4, 4, 1, 40);
GO

--PlayedQuizzes:  PlayedQuizzId / UserId / QuizzId / TotalPoints / DateAnswered
INSERT INTO "PlayedQuizzes"
	(UserId, QuizzId, TotalPoints)
VALUES
	( 1, 2, 30);
GO
*/




/*
CREATE TABLE RefreshTokens (
    RefreshTokenId            int not null IDENTITY(1,1),
	UserId                    int not null,
	Token           nvarchar(200) not null,
	DateExpires          datetime     null,
	DateCreated          datetime     null CONSTRAINT "DF_RefreshTokens_DateCreated" DEFAULT (getdate()),
	CreatedByIp      nvarchar(20)     null,
	DateRevoked          datetime     null,
	ReplacedByToken nvarchar(200)     null,
	ReasonRevoked    nvarchar(50)     null,
	IsExpired                 bit     null,
	IsRevoked                 bit     null,
	IsActive                  bit     null,
	CONSTRAINT "PK_RefreshTokens" PRIMARY KEY CLUSTERED ("RefreshTokenId"),
	CONSTRAINT "FK_RefreshTokens_Users"   FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users" ("UserId"),
	);
GO
*/