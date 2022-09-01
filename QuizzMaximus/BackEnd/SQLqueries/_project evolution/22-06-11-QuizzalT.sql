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

CREATE TABLE UserWatchList (
    UserId           int not null,
	WatchId          int not null,
	DateCreated datetime     null CONSTRAINT "DF_UserWatchList_DateCreated" DEFAULT (getdate()),
	CONSTRAINT "PK_UserWatchList" PRIMARY KEY CLUSTERED ("UserId", "WatchId"),
	CONSTRAINT "FK1_WatchList_Users" FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK2_WatchList_Users" FOREIGN KEY ("WatchId") REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "CK_Users_Watchs" CHECK (UserId != WatchId)
	);
GO

CREATE TABLE UserFriends (
    UserId           int not null,
	FriendId         int not null,
	DateCreated datetime     null CONSTRAINT "DF_UserFriends_DateCreated" DEFAULT (getdate()),
	CONSTRAINT "PK_UserFriends" PRIMARY KEY CLUSTERED ("UserId", "FriendId"),
	CONSTRAINT "FK1_Friends_Users" FOREIGN KEY ("UserId")   REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK2_Friends_Users" FOREIGN KEY ("FriendId") REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "CK_Users_Friends" CHECK (UserId != FriendId)
	);
GO

CREATE TABLE UserBFFs (
    UserId           int not null,
	BffId            int not null,
	DateCreated datetime     null CONSTRAINT "DF_UserBFFs_DateCreated" DEFAULT (getdate()),
	CONSTRAINT "PK_UserBFFs" PRIMARY KEY CLUSTERED ("UserId", "BffId"),
	CONSTRAINT "FK1_Bffs_Users" FOREIGN KEY ("UserId")   REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK2_Bffs_Users" FOREIGN KEY ("BffId") REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "CK_Users_Bff" CHECK (UserId != BffId)
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
	CONSTRAINT "FK_Answers_Questions" FOREIGN KEY ("QuestionId") REFERENCES "dbo"."Questions" ("QuestionId")
	);
GO

CREATE TABLE PlayedQuestions (
    PlayedQuestionId  int not null IDENTITY(1,1),
	UserId            int not null,
	QuestionId        int not null,
	GotItRight        bit not null,
	Points            int not null CONSTRAINT "DF_PlayedQuestions_Points"       DEFAULT (0),
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
	CONSTRAINT "FK_Quizzes_Users" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users" ("UserId")
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
	TotalPoints       int not null CONSTRAINT "DF_PlayedQuizzes_TotalPoints"  DEFAULT (0),
	DateAnswered datetime null     CONSTRAINT "DF_PlayedQuizzes_DateAnswered" DEFAULT (getdate()),
	CONSTRAINT "PK_PlayedQuizzes" PRIMARY KEY CLUSTERED ("PlayedQuizzId"),
	CONSTRAINT "FK_PlayedQuizzes_Users"   FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK_PlayedQuizzes_Quizzes" FOREIGN KEY ("QuizzId") REFERENCES "dbo"."Quizzes" ("QuizzId"),
	CONSTRAINT "CK_PlayedQuizzes_TotalPoints" CHECK (TotalPoints > 0)
	);
GO

--USE "QuizzalT";

--Users:  UserId / Username / Email, "Role" / DateCreated / "Password" / PasswordSalt
INSERT INTO "Users" 
	(Username, Email, "Role", DateCreated, "Password", PasswordSalt)
VALUES
	('MyAdmin',  'Admin@email.com',     'Admin', null, '123qwerty', null),
	(   'Igor',   'Igor@email.com', 'Moderator', null, '123qwerty', null),
	(    'Ivo',    'Ivo@email.com', 'Moderator', null, '123qwerty', null),
	(   'João',   'Joao@email.com', 'Moderator', null, '123qwerty', null),
	( 'Rafael', 'Rafael@email.com', 'Moderator', null, '123qwerty', null),
	('JohnDoe',  'JohnD@email.com',    'Player', null, '123qwerty', null),
	(  'Dummy',  'Dummy@email.com',    'Player', null, '123qwerty', null);
GO

--Friends:  UserId / FriendId / DateCreated
INSERT INTO "UserFriends" 
	(UserId, FriendId, DateCreated)
VALUES
	( 1, 2, null),
	( 1, 3, null),
	( 1, 4, null),
	( 2, 1, null),
	( 2, 3, null),
	( 2, 4, null),
	( 3, 1, null),
	( 3, 2, null),
	( 3, 4, null),
	( 4, 1, null),
	( 4, 2, null),
	( 4, 3, null);
GO

--Themes:  ThemeId / ThemeName 
INSERT INTO "Themes" 
	(ThemeName)
VALUES
	(   'History'),
	('Literature'),
	(       'Art'),
	(   'Biology'),
	(   'Physics');
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
	(UserId, ThemeId, QuestionText, Difficulty, Points, "Status", DateCreated, DateEdited)
VALUES
	( 1, 2, 'What is the name of the main character in Dostoevsky book Crime and Punishment?', 3, 30, 'Published', null, null),
	( 2, 3,                                                           'Who painted Guernica?', 1, 10, 'Published', null, null),
	( 3, 4,                               'What is the name of the innermost part of a cell?', 3, 30, 'Published', null, null),
	( 4, 1,                                                      'Where was Belisarius from?', 4, 40, 'Published', null, null);
GO

--Answers:  AnswerId / QuestionId / AnswerText / RightAnswer / DateCreated / DateEdited
INSERT INTO "Answers"
	(QuestionId, AnswerText, RightAnswer, DateCreated, DateEdited)
VALUES
	( 1, 'Romanovich Raskolnikov', 1, null, null),
	( 1,  'Semyonovna Marmeladov', 0, null, null),
	( 1, 'Ivanovich Svidrigailov', 0, null, null),
	( 1,   'Prokofych Razumikhin', 0, null, null),
	( 1,      'Porfiry Petrovich', 0, null, null),
	( 2,          'Pablo Picasso', 1, null, null),
	( 2,          'Henri Matisse', 0, null, null),
	( 2,           'Claude Monet', 0, null, null),
	( 2,          'Salvador Dali', 0, null, null),
	( 2,         'Marcel Duchamp', 0, null, null),
	( 3,              'Nucleolus', 1, null, null),
	( 3,          'Mitochondrion', 0, null, null),
	( 3,              'Cytoplasm', 0, null, null),
	( 3,               'Membrane', 0, null, null),
	( 3,               'Ribosome', 0, null, null),
	( 4,       'Byzantine Empire', 1, null, null),
	( 4,        'Seleucid Empire', 0, null, null),
	( 4,                'Bactria', 0, null, null),
	( 4,                'Parthia', 0, null, null),
	( 4,               'Carthage', 0, null, null);
GO

--PlayedQuestions:  PlayedQuestionId / UserId / QuestionId / GotItRight / Points / DateAnswered
INSERT INTO "PlayedQuestions"
	(UserId, QuestionId, GotItRight, Points, DateAnswered)
VALUES
	( 1, 1, 1, 30, null),
	( 1, 2, 0,  0, null),
	( 1, 3, 1, 30, null),
	( 1, 4, 0,  0, null),
	( 2, 1, 0,  0, null),
	( 2, 2, 1, 10, null),
	( 2, 3, 0,  0, null),
	( 2, 4, 1, 40, null),
	( 3, 1, 0,  0, null),
	( 3, 2, 1, 10, null),
	( 3, 3, 1, 30, null),
	( 3, 4, 0,  0, null),
	( 4, 1, 1, 30, null),
	( 4, 2, 0,  0, null),
	( 4, 3, 0,  0, null),
	( 4, 4, 1, 40, null);
GO

--Quizzes:  QuizzId / UserId / DateCreated / DateEdited
INSERT INTO "Quizzes" 
	(UserId, DateCreated, DateEdited)
VALUES
	( 1, null, null),
	( 2, null, null),
	( 3, null, null),
	( 4, null, null);
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
	(UserId, QuizzId, TotalPoints, DateAnswered)
VALUES
	( 1, 2, 30, null);
GO



