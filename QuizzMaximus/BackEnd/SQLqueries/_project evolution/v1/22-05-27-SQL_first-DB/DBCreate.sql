USE master
GO
if exists (select * from sysdatabases where name='QUIZZ')
        drop database QUIZZ
GO

CREATE DATABASE Quizz
go

USE "Quizz"

CREATE TABLE Users (
    UserID int not null identity(1,1) primary key,
	Username nvarchar(25) not null unique,
	Email nvarchar(50)not null unique,
	UserPassword nvarchar(100) not null,
	PasswordSalt Char(100) null
	);

CREATE INDEX "Email" ON "dbo"."Users"("Email")
GO

CREATE TABLE Questions (
    QuestionID int not null identity(1,1) primary key,
	Question nvarchar(100)not null,
	Theme nvarchar(15)not null,
	Difficulty int not null
	);

CREATE TABLE Quizz (
	QuizzID int NOT NULL identity(1,1) primary key,
	QuizzName nvarchar(50) NOT NULL
	);

CREATE TABLE QuizzQuestions (
	QuizzID int NOT NULL,
	QuestionID int NOT NULL,
	primary key(QuizzID, QuestionID),
	constraint fk_QuizzQuestions foreign key (QuizzID) references Quizz (QuizzID),
	constraint fk_QuestionsQuizz foreign key (QuestionID) references Questions (QuestionID)
);


CREATE TABLE Answers (
    AnswerID int not null identity(1,1) primary key,
	Answer nvarchar(50)not null,
	Correct bit not null,
	QuestionID int not null, constraint fk_QuestionIDAnswers foreign key (QuestionID) references Questions (QuestionID)
	);

CREATE TABLE Score (
    UserID int not null,
	QuizzID int not null,
	primary key(UserID, QuizzID),
	constraint fk_UserIDQuizz foreign key (UserID) references Users (UserID),
	constraint fk_QuizzIDQuizz foreign key (QuizzID) references Quizz (QuizzID),
	Score int
	);