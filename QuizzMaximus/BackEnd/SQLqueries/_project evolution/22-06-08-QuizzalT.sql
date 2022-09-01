USE master;
GO
if exists (select * from sysdatabases where name='QuizzalT')
        drop database QuizzalT
GO

CREATE DATABASE QuizzalT
GO

USE "QuizzalT";

CREATE TABLE Users (
    UserId int not null IDENTITY(1,1),
	Username nvarchar(25) not null,
	Email nvarchar(50)not null,
	UserPassword nvarchar(100) not null,
	PasswordSalt Char(100) null,
	CONSTRAINT "PK_Users" PRIMARY KEY CLUSTERED ("UserId"),
	CONSTRAINT "UQ_Users_Username" UNIQUE("Username"),
	CONSTRAINT "UQ_Users_Email" UNIQUE("Email")
	);
CREATE INDEX "Username" ON "dbo"."Users"("Username");
CREATE INDEX "Email" ON "dbo"."Users"("Email");
GO

CREATE TABLE Friends (
    UserId int not null,
	FriendUserId int not null,
	CONSTRAINT "PK_Friends" PRIMARY KEY CLUSTERED ("UserId", "FriendUserId"),
	CONSTRAINT "FK_Users_Users"   FOREIGN KEY  ("UserId")       REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK_Friends_Users" FOREIGN KEY  ("FriendUserId") REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "CK_Friends_Users" CHECK (UserId != FriendUserId)
	);
GO

CREATE TABLE Themes (
    ThemeId int not null IDENTITY(1,1),
	ThemeName nvarchar(15) not null,
	CONSTRAINT "PK_Themes" PRIMARY KEY CLUSTERED ("ThemeId"),
	CONSTRAINT "UQ_Themes_ThemeName" UNIQUE("ThemeName")   
	);
CREATE INDEX ThemeName ON "dbo".Themes("ThemeName");
GO

CREATE TABLE Achievements (
    ThemeId int not null,
	UserId int not null,
	"Level" int not null CONSTRAINT "DF_Achievements_Level" DEFAULT (1),
	CONSTRAINT "PK_Achievements" PRIMARY KEY CLUSTERED ("ThemeId", "UserId"),
	CONSTRAINT "FK_Achievements_Themes" FOREIGN KEY ("ThemeId") REFERENCES "dbo"."Themes" ("ThemeId"),
	CONSTRAINT "FK_Achievements_Users"  FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users" ("UserId")
	);
GO

CREATE TABLE Questions (
    QuestionId int not null identity(1,1),
	UserId int not null,
	ThemeId int not null,
	QuestionText nvarchar(100) not null,
	Difficulty int not null CONSTRAINT "DF_Questions_Difficulty" DEFAULT (1),
	Points int not null CONSTRAINT "DF_Questions_Points" DEFAULT (0),
	"Status" nvarchar(15) not null,
	DateCreated datetime null CONSTRAINT "DF_Questions_DateCreated" DEFAULT (getdate()),
	DateEdited datetime null,
	CONSTRAINT "PK_Questions" PRIMARY KEY CLUSTERED ("QuestionId"),
	CONSTRAINT "FK_Questions_Users"  FOREIGN KEY ("UserId")  REFERENCES "dbo"."Users"  ("UserId"),
	CONSTRAINT "FK_Questions_Themes" FOREIGN KEY ("ThemeId") REFERENCES "dbo"."Themes" ("ThemeId"),
	CONSTRAINT "CK_Questions_Difficulty" CHECK (Difficulty >= 0 AND (Difficulty <= 10)),
	CONSTRAINT "CK_Questions_Points" CHECK (Points >= 0 AND (Points <= 100))
	);
GO

CREATE TABLE Answers (
    AnswerId int not null IDENTITY(1,1),
	QuestionId int not null,
	AnswerText nvarchar(100) not null,
	RightAnswer bit not null,
	DateCreated datetime null CONSTRAINT "DF_Answers_DateCreated" DEFAULT (getdate()),
	CONSTRAINT "PK_Answers" PRIMARY KEY CLUSTERED ("AnswerId"),
	CONSTRAINT "FK_Answers_Questions" FOREIGN KEY ("QuestionId") REFERENCES "dbo"."Questions" ("QuestionId")
	);
GO

CREATE TABLE PlayedQuestions (
    PlayedQuestionId int not null IDENTITY(1,1),
	UserId int not null,
	QuestionId int not null,
	GotItRight bit not null,
	Points int not null CONSTRAINT "DF_PlayedQuestions_Points" DEFAULT (0),
	DateAnswered datetime null CONSTRAINT "DF_PlayedQuestions_DateAnswered" DEFAULT (getdate()),
	CONSTRAINT "PK_PlayedQuestions" PRIMARY KEY CLUSTERED ("PlayedQuestionId"),
	CONSTRAINT "FK_PlayedQuestions_Users" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK_PlayedQuestions_Questions" FOREIGN KEY ("QuestionId") REFERENCES "dbo"."Questions" ("QuestionId")
	);
GO

CREATE TABLE Quizzes (
    QuizzId int not null IDENTITY(1,1),
	UserId int not null,
	DateCreated datetime null CONSTRAINT "DF_PlayedQuestions_DateCreated" DEFAULT (getdate()),
	DateEdited datetime null,
	CONSTRAINT "PK_Quizzes" PRIMARY KEY CLUSTERED ("QuizzId"),
	CONSTRAINT "FK_Quizzes_Users" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users" ("UserId")
	);
GO

CREATE TABLE QuizzQuestions (
    QuizzId int not null,
	QuestionId int not null,
	CONSTRAINT "PK_QuizzQuestions" PRIMARY KEY CLUSTERED ("QuizzId", "QuestionId"),
	CONSTRAINT "FK_QuizzQuestions_Quizzes" FOREIGN KEY ("QuizzId") REFERENCES "dbo"."Quizzes" ("QuizzId"),
	CONSTRAINT "FK_QuizzQuestions_Questions"  FOREIGN KEY ("QuestionId")  REFERENCES "dbo"."Questions" ("QuestionId")
	);
GO

CREATE TABLE PlayedQuizzes (
    PlayedQuizzId int not null IDENTITY(1,1),
	UserId int not null,
	QuizzId int not null,
	TotalPoints int not null CONSTRAINT "DF_PlayedQuizzes_TotalPoints" DEFAULT (0),
	DateAnswered datetime null CONSTRAINT "DF_PlayedQuizzes_DateAnswered" DEFAULT (getdate()),
	CONSTRAINT "PK_PlayedQuizzes" PRIMARY KEY CLUSTERED ("PlayedQuizzId"),
	CONSTRAINT "FK_PlayedQuizzes_Users" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users" ("UserId"),
	CONSTRAINT "FK_PlayedQuizzes_Quizzes" FOREIGN KEY ("QuizzId") REFERENCES "dbo"."Quizzes" ("QuizzId"),
	CONSTRAINT "CK_PlayedQuizzes_TotalPoints" CHECK (TotalPoints > 0)
	);
GO

--USE "QuizzalT";

--Users:  UserId / Username / Email / UserPassword / PasswordSalt
INSERT INTO "Users" VALUES(   'Igor',    'Igor@email.com', '123qwerty', null);
INSERT INTO "Users" VALUES(    'Ivo',     'Ivo@email.com', '123qwerty', null);
INSERT INTO "Users" VALUES(   'João',    'Joao@email.com', '123qwerty', null);
INSERT INTO "Users" VALUES( 'Rafael',  'Rafael@email.com', '123qwerty', null);
INSERT INTO "Users" VALUES('JohnDoe', 'JohnDoe@email.com', '123qwerty', null);
INSERT INTO "Users" VALUES(  'Dummy',   'Dummy@email.com', '123qwerty', null);
GO

--Friends:  UserId / FriendUserId
INSERT INTO "Friends" VALUES( 1, 2);
INSERT INTO "Friends" VALUES( 1, 3);
INSERT INTO "Friends" VALUES( 1, 4);
INSERT INTO "Friends" VALUES( 2, 1);
INSERT INTO "Friends" VALUES( 2, 3);
INSERT INTO "Friends" VALUES( 2, 4);
INSERT INTO "Friends" VALUES( 3, 1);
INSERT INTO "Friends" VALUES( 3, 2);
INSERT INTO "Friends" VALUES( 3, 4);
INSERT INTO "Friends" VALUES( 4, 1);
INSERT INTO "Friends" VALUES( 4, 2);
INSERT INTO "Friends" VALUES( 4, 3);
GO

--Themes:  ThemeId / ThemeName 
INSERT INTO "Themes" VALUES(    'History');
INSERT INTO "Themes" VALUES( 'Literature');
INSERT INTO "Themes" VALUES(        'Art');
INSERT INTO "Themes" VALUES(    'Biology');
INSERT INTO "Themes" VALUES(    'Physics');
GO

--Achievements:  ThemeId / UserId / Level
INSERT INTO "Achievements" VALUES( 1, 1, 3);
INSERT INTO "Achievements" VALUES( 2, 1, 8);
INSERT INTO "Achievements" VALUES( 2, 2, 9);
INSERT INTO "Achievements" VALUES( 3, 2, 6);
INSERT INTO "Achievements" VALUES( 4, 3, 2);
INSERT INTO "Achievements" VALUES( 5, 3, 7);
INSERT INTO "Achievements" VALUES( 2, 4, 6);
INSERT INTO "Achievements" VALUES( 4, 4, 9);
GO

--Questions:  QuestionId / UserId / ThemeId / QuestionText / Difficulty / Points / Status / DateCreated / DateEdited
INSERT INTO "Questions" VALUES( 1, 2, 'What is the name of the main character in Dostoevsky book Crime and Punishment?', 3, 30, 'Published', null, null);
INSERT INTO "Questions" VALUES( 2, 3, 'Who painted Guernica?', 1, 10, 'Published', null, null);
INSERT INTO "Questions" VALUES( 3, 4, 'What is the name of the innermost part of a cell?', 3, 30, 'Published', null, null);
INSERT INTO "Questions" VALUES( 4, 1, 'Where was Belisarius from?', 4, 40, 'Published', null, null);
GO

--Answers:  AnswerId / QuestionId / AnswerText / RightAnswer / DateCreated
INSERT INTO "Answers" VALUES( 1, 'Romanovich Raskolnikov', 1, null);
INSERT INTO "Answers" VALUES( 1, 'Semyonovna Marmeladov', 0, null);
INSERT INTO "Answers" VALUES( 1, 'Ivanovich Svidrigailov', 0, null);
INSERT INTO "Answers" VALUES( 1, 'Prokofych Razumikhin', 0, null);
INSERT INTO "Answers" VALUES( 1, 'Porfiry Petrovich', 0, null);
INSERT INTO "Answers" VALUES( 2, 'Pablo Picasso', 1, null);
INSERT INTO "Answers" VALUES( 2, 'Henri Matisse', 0, null);
INSERT INTO "Answers" VALUES( 2, 'Claude Monet', 0, null);
INSERT INTO "Answers" VALUES( 2, 'Salvador Dali', 0, null);
INSERT INTO "Answers" VALUES( 2, 'Marcel Duchamp', 0, null);
INSERT INTO "Answers" VALUES( 3, 'Nucleolus', 1, null);
INSERT INTO "Answers" VALUES( 3, 'Mitochondrion', 0, null);
INSERT INTO "Answers" VALUES( 3, 'Cytoplasm', 0, null);
INSERT INTO "Answers" VALUES( 3, 'Membrane', 0, null);
INSERT INTO "Answers" VALUES( 3, 'Ribosome', 0, null);
INSERT INTO "Answers" VALUES( 4, 'Byzantine Empire', 1, null);
INSERT INTO "Answers" VALUES( 4, 'Seleucid Empire', 0, null);
INSERT INTO "Answers" VALUES( 4, 'Bactria', 0, null);
INSERT INTO "Answers" VALUES( 4, 'Parthia', 0, null);
INSERT INTO "Answers" VALUES( 4, 'Carthage Empire', 0, null);
GO

--PlayedQuestions:  PlayedQuestionId / UserId / QuestionId / GotItRight / Points / DateAnswered
INSERT INTO "PlayedQuestions" VALUES( 1, 1, 1, 30, null);
INSERT INTO "PlayedQuestions" VALUES( 1, 2, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 1, 3, 1, 30, null);
INSERT INTO "PlayedQuestions" VALUES( 1, 4, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 2, 1, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 2, 2, 1, 10, null);
INSERT INTO "PlayedQuestions" VALUES( 2, 3, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 2, 4, 1, 40, null);
INSERT INTO "PlayedQuestions" VALUES( 3, 1, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 3, 2, 1, 10, null);
INSERT INTO "PlayedQuestions" VALUES( 3, 3, 1, 30, null);
INSERT INTO "PlayedQuestions" VALUES( 3, 4, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 4, 1, 1, 30, null);
INSERT INTO "PlayedQuestions" VALUES( 4, 2, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 4, 3, 0, 0, null);
INSERT INTO "PlayedQuestions" VALUES( 4, 4, 1, 40, null);
GO

--Quizzes:  QuizzId / UserId / DateCreated / DateEdited
INSERT INTO "Quizzes" VALUES( 1, null, null);
INSERT INTO "Quizzes" VALUES( 2, null, null);
INSERT INTO "Quizzes" VALUES( 3, null, null);
INSERT INTO "Quizzes" VALUES( 4, null, null);
GO

--QuizzQuestions:  QuizzId / QuestionId 
INSERT INTO "QuizzQuestions" VALUES( 1, 1);
INSERT INTO "QuizzQuestions" VALUES( 1, 2);
INSERT INTO "QuizzQuestions" VALUES( 2, 2);
INSERT INTO "QuizzQuestions" VALUES( 2, 3);
INSERT INTO "QuizzQuestions" VALUES( 3, 3);
INSERT INTO "QuizzQuestions" VALUES( 3, 4);
INSERT INTO "QuizzQuestions" VALUES( 4, 1);
INSERT INTO "QuizzQuestions" VALUES( 4, 4);
GO

--PlayedQuizzes:  PlayedQuizzId / UserId / QuizzId / TotalPoints / DateAnswered
INSERT INTO "PlayedQuizzes" VALUES( 1, 2, 30, null);
GO



