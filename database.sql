CREATE DATABASE OnlineExamDB;
GO
USE OnlineExamDB;
--USE master;
--ALTER DATABASE ONlineExamDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE ONlineExamDB;

--GO
--CREATE USER [INFICS\vilvapriyam] FOR LOGIN [INFICS\vilvapriyam];
--ALTER ROLE db_owner ADD MEMBER [INFICS\vilvapriyam];




CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Mobile NVARCHAR(15),
    City NVARCHAR(50),
    State NVARCHAR(50),
    DOB DATE,
    Qualification NVARCHAR(100),
    YearOfCompletion INT
);

CREATE TABLE Admins
(
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100),
    Password NVARCHAR(100)
);
INSERT INTO Admins
VALUES
(
'admin@gmail.com',
'admin123'
);
CREATE TABLE Exams
(
    ExamId INT IDENTITY(1,1) PRIMARY KEY,
    ExamName NVARCHAR(100),
    LevelNo INT,
    TotalQuestions INT,
    PassMarks INT,
    DurationMinutes INT
);

INSERT INTO Exams
(
    ExamName,
    LevelNo,
    TotalQuestions,
    PassMarks,
    DurationMinutes
)
VALUES
('C#',1,10,6,30);

INSERT INTO Exams
(
    ExamName,
    LevelNo,
    TotalQuestions,
    PassMarks,
    DurationMinutes
)
VALUES
('C#',2,10,6,30);

INSERT INTO Exams
(
    ExamName,
    LevelNo,
    TotalQuestions,
    PassMarks,
    DurationMinutes
)
VALUES
('C#',3,10,6,30);

CREATE TABLE Questions
(
    QuestionId INT IDENTITY(1,1) PRIMARY KEY,
    ExamId INT,
    QuestionText NVARCHAR(MAX),
    OptionA NVARCHAR(500),
    OptionB NVARCHAR(500),
    OptionC NVARCHAR(500),
    OptionD NVARCHAR(500),
    CorrectAnswer CHAR(1),

    FOREIGN KEY (ExamId)
    REFERENCES Exams(ExamId)
);

CREATE TABLE Results
(
    ResultId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    ExamId INT,
    Score INT,
    Status NVARCHAR(20),
    ExamDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY(UserId)
    REFERENCES Users(UserId),

    FOREIGN KEY(ExamId)
    REFERENCES Exams(ExamId)
);

select * from users