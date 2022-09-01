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

-- context.Database.ExecuteSqlCommand("PR_Create_PlayedQuizz @q0, @q1", parameters: new[] { "1", "2" });   -- @UserId:"1"/@QuizzId:"2" https://www.entityframeworktutorial.net/efcore/working-with-stored-procedure-in-ef-core.aspx
CREATE PROCEDURE PR_Create_PlayedQuizz
	@UserId int,
	@QuizzId int
AS
SET NOCOUNT ON;
BEGIN
	INSERT INTO PlayedQuizzes(
           [UserId],
           [QuizzId],
		   [TotalPoints]
           )
	VALUES (@UserId, @QuizzId, --TotalPoints
		(SELECT SUM(Points) FROM Questions WHERE QuestionId IN
		(SELECT QuestionId  FROM QuizzQuestions WHERE QuizzId = @QuizzId AND QuestionId IN
		(SELECT QuestionId  FROM PlayedQuestions WHERE GotItRight = 1 ))))
END

SET NOCOUNT OFF;
GO

--USE "QuizzalT";

--Users:  UserId / Username / Email / "Role" / DateCreated / "Password" / PasswordSalt
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
--Igor
	(1, 2, 2),
	(2, 1, 2),
	(1, 3, 2),
	(3, 1, 2),
	(1, 4, 1),
	(4, 1, 1),
	(1, 5, 3),
	(1, 6, 3),
	(1, 7, 4),
--Ivo
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
	( 1,  2,                 'What is the name of the main character in Dostoevsky book Crime and Punishment?', 3, 30, 'Published'), --1 id
	( 2,  3,                                                                           'Who painted Guernica?', 1, 10, 'Published'), --2
	( 3, 11,                                               'What is the name of the innermost part of a cell?', 3, 30, 'Published'), --3
	( 4,  1,                                                                      'Where was Belisarius from?', 4, 40, 'Published'), --4
	( 2,  1,                                                     'Who built the first Great Pyramid in Cairo?', 3, 30, 'Published'), --5
	( 2,  1,                                                       'In what year was Napoleon Bonaparte born?', 7, 70, 'Published'), --6
	( 2,  1,                'The Peloponnesian War was a conflict in ancient Greece between wich city states?', 5, 50, 'Published'), --7
	( 2,  1,                          'The conflict between Rome and Carthage was set in how many Punic Wars?', 7, 70, 'Published'), --8
	( 2,  1, 'How many major battles did Hannibal win against Rome in mainland Italy on the second Punic War?', 8, 80, 'Published'), --9
	( 2,  1,                                              'Who is the mythical founder of the city of Lisbon?', 6, 60, 'Published'), --10
	( 2,  1,                                                  'In what year did Brazil gain its independence?', 6, 60, 'Published'), --11
	( 2,  1,                                 'Who conquered Constantinople in 1453 from the Byzantine Empire?', 4, 40, 'Published'), --12
	( 2,  1,                                                       'Dummy Dummy Dummy Dummy Dummy Dummy Dummy', 3, 30,     'Draft'); --ddd
GO

--Answers:  AnswerId / QuestionId / AnswerText / RightAnswer / DateCreated / DateEdited
INSERT INTO "Answers"
	(QuestionId, AnswerText, RightAnswer)
VALUES
	( 1, 'Romanovich Raskolnikov', 1), ( 1,  'Semyonovna Marmeladov', 0), ( 1, 'Ivanovich Svidrigailov', 0), ( 1,   'Prokofych Razumikhin', 0), ( 1,      'Porfiry Petrovich', 0),
	( 2,          'Pablo Picasso', 1), ( 2,          'Henri Matisse', 0), ( 2,           'Claude Monet', 0), ( 2,          'Salvador Dali', 0), ( 2,         'Marcel Duchamp', 0),
	( 3,              'Nucleolus', 1), ( 3,          'Mitochondrion', 0), ( 3,              'Cytoplasm', 0), ( 3,               'Membrane', 0), ( 3,               'Ribosome', 0),
	( 4,       'Byzantine Empire', 1), ( 4,        'Seleucid Empire', 0), ( 4,                'Bactria', 0), ( 4,                'Parthia', 0), ( 4,               'Carthage', 0),
    ( 5,                  'Khufu', 1), ( 5,                 'Djoser', 0), ( 5,            'Ramesses II', 0), ( 5,            'Tutankhamun', 0), ( 5,             'Hatshepsut', 0), ( 5,                 'Snefru', 0),
	( 6,                   '1769', 1), ( 6,                   '1821', 0), ( 6,                   '1721', 0), ( 6,                   '1749', 0), ( 6,                   '1699', 0), ( 6,                   '1803', 0), ( 6,                   '1783', 0),
	( 7,      'Athens and Sparta', 1), ( 7,      'Athens and Thebes', 0), ( 7,     'Corinth and Rhodes', 0), ( 7,                'Parthia', 0), ( 7,        'Athens and Troy', 0),
	( 8,                      '3', 1), ( 8,                      '2', 0), ( 8,                      '4', 0), ( 8,                      '1', 0), ( 8,                      '5', 0),
	( 9,                      '3', 1), ( 9,                      '2', 0), ( 9,                      '4', 0), ( 9,                      '1', 0), ( 9,                      '5', 0),
	( 10,               'Ulysses', 1), ( 10,             'Gilgamesh', 0), ( 10,              'Viriatus', 0), ( 10,                  'Loki', 0), ( 10,              'Achilles', 0),
	( 11,                  '1822', 1), ( 11,                  '1802', 0), ( 11,                  '1788', 0), ( 11,                  '1844', 0), ( 11,                  '1852', 0),
	( 12,        'Ottoman Empire', 1), ( 12,          'Roman Empire', 0), ( 12,          'Golden Horde', 0), ( 12,       'Assyrian Empire', 0), ( 12,       'Egyptian Empire', 0);
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



