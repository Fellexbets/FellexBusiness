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
    ThemeId int not null,
	UserId  int not null,
	"Level" int not null CONSTRAINT "DF_Achievements_Level" DEFAULT (1),
	CONSTRAINT "PK_Achievements" PRIMARY KEY CLUSTERED ("ThemeId", "UserId"),
	CONSTRAINT "FK_Achievements_Themes" FOREIGN KEY ("ThemeId") REFERENCES "dbo"."Themes" ("ThemeId"),
	CONSTRAINT "FK_Achievements_Users"  FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users" ("UserId")
	);
GO

CREATE TABLE Questions (
    QuestionId             int not null identity(1,1),
	UserId                 int not null,
	ThemeId                int not null,
	QuestionText nvarchar(100) not null,
	Difficulty             int not null CONSTRAINT "DF_Questions_Difficulty" DEFAULT (1),
	Points                 int not null CONSTRAINT "DF_Questions_Points" DEFAULT (0),
	"Status"      nvarchar(15) not null,
	DateCreated       datetime     null CONSTRAINT "DF_Questions_DateCreated" DEFAULT (getdate()),
	DateEdited        datetime     null,
	CONSTRAINT "PK_Questions" PRIMARY KEY CLUSTERED ("QuestionId"),
	CONSTRAINT "FK_Questions_Users"  FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users"  ("UserId"),
	CONSTRAINT "FK_Questions_Themes" FOREIGN KEY ("ThemeId") REFERENCES "dbo"."Themes" ("ThemeId"),
	CONSTRAINT "CK_Questions_Difficulty" CHECK (Difficulty >= 0 AND (Difficulty <= 10)),
	CONSTRAINT "CK_Questions_Points"     CHECK (Points >= 0 AND (Points <= 100))
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
    QuizzId          int not null IDENTITY(1,1),
	UserId           int not null,
	DateCreated datetime     null CONSTRAINT "DF_PlayedQuestions_DateCreated" DEFAULT (getdate()),
	DateEdited  datetime     null,
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

--USE "QuizzalT";

--Users:  UserId / Username / Email, "Role" / DateCreated / "Password" / PasswordSalt
INSERT INTO "Users" 
	(Username, Email, "Role", "Password", PasswordSalt)
VALUES
	('MyAdmin',  'Admin@email.com',     'Admin', '123qwerty', null),
	(   'Igor',   'Igor@email.com', 'Moderator', '123qwerty', null),
	(    'Ivo',    'Ivo@email.com', 'Moderator', '123qwerty', null),
	(   'João',   'Joao@email.com', 'Moderator', '123qwerty', null),
	( 'Rafael', 'Rafael@email.com', 'Moderator', '123qwerty', null),
	('JohnDoe',  'JohnD@email.com',    'Player', '123qwerty', null),
	(  'Dummy',  'Dummy@email.com',    'Player', '123qwerty', null),
	(   'HALL',   'HALL@email.com',    'Player', '123qwerty', null),
	( 'Doctor',    'Who@email.com',    'Player', '123qwerty', null);
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
--Igor
	(2, 3, 2),
	(3, 2, 2),
	(2, 4, 2),
	(4, 2, 2),
	(2, 5, 1),
	(5, 2, 1),
	(2, 6, 3),
	(2, 7, 3),
	(2, 8, 4),
--Ivo
	(3, 4, 1),
	(4, 3, 1),
	(3, 5, 1),
	(5, 3, 1),
	(3, 6, 3),
	(3, 7, 3),
	(3, 8, 4);
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
	(ThemeId, UserId, "Level")
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

--Questions:  QuestionId / UserId / ThemeId / QuestionText / Difficulty / Points / Status / DateCreated / DateEdited
INSERT INTO "Questions" 
	(UserId, ThemeId, QuestionText, Difficulty, Points, "Status")
VALUES
	( 1,  2, 'What is the name of the main character in Dostoevsky book Crime and Punishment?', 3, 30, 'Published'),
	( 2,  3,                                                           'Who painted Guernica?', 1, 10, 'Published'),
	( 3, 11,                               'What is the name of the innermost part of a cell?', 3, 30, 'Published'),
	( 4,  1,                                                      'Where was Belisarius from?', 4, 40, 'Published'),
	( 1,  1,                                     'Who built the first Great Pyramid in Cairo?', 3, 30,     'Draft');
GO

--Answers:  AnswerId / QuestionId / AnswerText / RightAnswer / DateCreated / DateEdited
INSERT INTO "Answers"
	(QuestionId, AnswerText, RightAnswer)
VALUES
	( 1, 'Romanovich Raskolnikov', 1),
	( 1,  'Semyonovna Marmeladov', 0),
	( 1, 'Ivanovich Svidrigailov', 0),
	( 1,   'Prokofych Razumikhin', 0),
	( 1,      'Porfiry Petrovich', 0),
	( 2,          'Pablo Picasso', 1),
	( 2,          'Henri Matisse', 0),
	( 2,           'Claude Monet', 0),
	( 2,          'Salvador Dali', 0),
	( 2,         'Marcel Duchamp', 0),
	( 3,              'Nucleolus', 1),
	( 3,          'Mitochondrion', 0),
	( 3,              'Cytoplasm', 0),
	( 3,               'Membrane', 0),
	( 3,               'Ribosome', 0),
	( 4,       'Byzantine Empire', 1),
	( 4,        'Seleucid Empire', 0),
	( 4,                'Bactria', 0),
	( 4,                'Parthia', 0),
	( 4,               'Carthage', 0),
	( 5,                  'Khufu', 1),
	( 5,                 'Djoser', 0),
	( 5,            'Ramesses II', 0),
	( 5,            'Tutankhamun', 0),
	( 5,             'Hatshepsut', 0),
	( 5,                 'Snefru', 0);
GO

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

--Quizzes:  QuizzId / UserId / DateCreated / DateEdited
INSERT INTO "Quizzes" 
	(UserId)
VALUES
	( 1),
	( 2),
	( 3),
	( 4);
GO

--QuizzQuestions:  QuizzId / QuestionId 
INSERT INTO "QuizzQuestions" 
	(QuizzId, QuestionId)
VALUES
	( 1, 1),
	( 1, 2),
	( 2, 2),
	( 2, 3),
	( 3, 3),
	( 3, 4),
	( 4, 1),
	( 4, 4);
GO

--PlayedQuizzes:  PlayedQuizzId / UserId / QuizzId / TotalPoints / DateAnswered
INSERT INTO "PlayedQuizzes"
	(UserId, QuizzId, TotalPoints)
VALUES
	( 1, 2, 30);
GO



