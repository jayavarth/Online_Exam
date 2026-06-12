
--GO
--USE OnlineExamDB;
--USE master;
--ALTER DATABASE ONlineExamDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE ONlineExamDB;

--GO
--CREATE USER [INFICS\vilvapriyam] FOR LOGIN [INFICS\vilvapriyam];
--ALTER ROLE db_owner ADD MEMBER [INFICS\vilvapriyam];

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
------
CREATE TABLE Countries
(
    CountryId INT IDENTITY(1,1) PRIMARY KEY,
    CountryName NVARCHAR(100)
);

CREATE TABLE States
(
    StateId INT IDENTITY(1,1) PRIMARY KEY,
    CountryId INT,
    StateName NVARCHAR(100),

    FOREIGN KEY(CountryId)
    REFERENCES Countries(CountryId)
);

CREATE TABLE Cities
(
    CityId INT IDENTITY(1,1) PRIMARY KEY,
    StateId INT,
    CityName NVARCHAR(100),

    FOREIGN KEY(StateId)
    REFERENCES States(StateId)
);

CREATE TABLE Qualifications
(
    QualificationId INT IDENTITY(1,1) PRIMARY KEY,
    QualificationName NVARCHAR(100)
);
select * from users
alter table users drop column qualification
ALTER TABLE Users
ADD CountryId INT;

ALTER TABLE Users
ADD StateId INT;

ALTER TABLE Users
ADD CityId INT;

ALTER TABLE Users
ADD QualificationId INT;
--ccountries insertion
INSERT INTO Countries VALUES ('Afghanistan');
INSERT INTO Countries VALUES ('Albania');
INSERT INTO Countries VALUES ('Algeria');
INSERT INTO Countries VALUES ('Andorra');
INSERT INTO Countries VALUES ('Angola');
INSERT INTO Countries VALUES ('Antigua and Barbuda');
INSERT INTO Countries VALUES ('Argentina');
INSERT INTO Countries VALUES ('Armenia');
INSERT INTO Countries VALUES ('Australia');
INSERT INTO Countries VALUES ('Austria');
INSERT INTO Countries VALUES ('Azerbaijan');
INSERT INTO Countries VALUES ('Bahamas');
INSERT INTO Countries VALUES ('Bahrain');
INSERT INTO Countries VALUES ('Bangladesh');
INSERT INTO Countries VALUES ('Barbados');
INSERT INTO Countries VALUES ('Belarus');
INSERT INTO Countries VALUES ('Belgium');
INSERT INTO Countries VALUES ('Belize');
INSERT INTO Countries VALUES ('Benin');
INSERT INTO Countries VALUES ('Bhutan');
INSERT INTO Countries VALUES ('Bolivia');
INSERT INTO Countries VALUES ('Bosnia and Herzegovina');
INSERT INTO Countries VALUES ('Botswana');
INSERT INTO Countries VALUES ('Brazil');
INSERT INTO Countries VALUES ('Brunei');
INSERT INTO Countries VALUES ('Bulgaria');
INSERT INTO Countries VALUES ('Burkina Faso');
INSERT INTO Countries VALUES ('Burundi');
INSERT INTO Countries VALUES ('Cabo Verde');
INSERT INTO Countries VALUES ('Cambodia');
INSERT INTO Countries VALUES ('Cameroon');
INSERT INTO Countries VALUES ('Canada');
INSERT INTO Countries VALUES ('Central African Republic');
INSERT INTO Countries VALUES ('Chad');
INSERT INTO Countries VALUES ('Chile');
INSERT INTO Countries VALUES ('China');
INSERT INTO Countries VALUES ('Colombia');
INSERT INTO Countries VALUES ('Comoros');
INSERT INTO Countries VALUES ('Congo');
INSERT INTO Countries VALUES ('Costa Rica');
INSERT INTO Countries VALUES ('Croatia');
INSERT INTO Countries VALUES ('Cuba');
INSERT INTO Countries VALUES ('Cyprus');
INSERT INTO Countries VALUES ('Czech Republic');
INSERT INTO Countries VALUES ('Democratic Republic of the Congo');
INSERT INTO Countries VALUES ('Denmark');
INSERT INTO Countries VALUES ('Djibouti');
INSERT INTO Countries VALUES ('Dominica');
INSERT INTO Countries VALUES ('Dominican Republic');
INSERT INTO Countries VALUES ('Ecuador');
INSERT INTO Countries VALUES ('Egypt');
INSERT INTO Countries VALUES ('El Salvador');
INSERT INTO Countries VALUES ('Equatorial Guinea');
INSERT INTO Countries VALUES ('Eritrea');
INSERT INTO Countries VALUES ('Estonia');
INSERT INTO Countries VALUES ('Eswatini');
INSERT INTO Countries VALUES ('Ethiopia');
INSERT INTO Countries VALUES ('Fiji');
INSERT INTO Countries VALUES ('Finland');
INSERT INTO Countries VALUES ('France');
INSERT INTO Countries VALUES ('Gabon');
INSERT INTO Countries VALUES ('Gambia');
INSERT INTO Countries VALUES ('Georgia');
INSERT INTO Countries VALUES ('Germany');
INSERT INTO Countries VALUES ('Ghana');
INSERT INTO Countries VALUES ('Greece');
INSERT INTO Countries VALUES ('Grenada');
INSERT INTO Countries VALUES ('Guatemala');
INSERT INTO Countries VALUES ('Guinea');
INSERT INTO Countries VALUES ('Guinea-Bissau');
INSERT INTO Countries VALUES ('Guyana');
INSERT INTO Countries VALUES ('Haiti');
INSERT INTO Countries VALUES ('Honduras');
INSERT INTO Countries VALUES ('Hungary');
INSERT INTO Countries VALUES ('Iceland');
INSERT INTO Countries VALUES ('India');
INSERT INTO Countries VALUES ('Indonesia');
INSERT INTO Countries VALUES ('Iran');
INSERT INTO Countries VALUES ('Iraq');
INSERT INTO Countries VALUES ('Ireland');
INSERT INTO Countries VALUES ('Israel');
INSERT INTO Countries VALUES ('Italy');
INSERT INTO Countries VALUES ('Jamaica');
INSERT INTO Countries VALUES ('Japan');
INSERT INTO Countries VALUES ('Jordan');
INSERT INTO Countries VALUES ('Kazakhstan');
INSERT INTO Countries VALUES ('Kenya');
INSERT INTO Countries VALUES ('Kiribati');
INSERT INTO Countries VALUES ('Kuwait');
INSERT INTO Countries VALUES ('Kyrgyzstan');
INSERT INTO Countries VALUES ('Laos');
INSERT INTO Countries VALUES ('Latvia');
INSERT INTO Countries VALUES ('Lebanon');
INSERT INTO Countries VALUES ('Lesotho');
INSERT INTO Countries VALUES ('Liberia');
INSERT INTO Countries VALUES ('Libya');
INSERT INTO Countries VALUES ('Liechtenstein');
INSERT INTO Countries VALUES ('Lithuania');
INSERT INTO Countries VALUES ('Luxembourg');
INSERT INTO Countries VALUES ('Madagascar');
INSERT INTO Countries VALUES ('Malawi');
INSERT INTO Countries VALUES ('Malaysia');
INSERT INTO Countries VALUES ('Maldives');
INSERT INTO Countries VALUES ('Mali');
INSERT INTO Countries VALUES ('Malta');
INSERT INTO Countries VALUES ('Marshall Islands');
INSERT INTO Countries VALUES ('Mauritania');
INSERT INTO Countries VALUES ('Mauritius');
INSERT INTO Countries VALUES ('Mexico');
INSERT INTO Countries VALUES ('Micronesia');
INSERT INTO Countries VALUES ('Moldova');
INSERT INTO Countries VALUES ('Monaco');
INSERT INTO Countries VALUES ('Mongolia');
INSERT INTO Countries VALUES ('Montenegro');
INSERT INTO Countries VALUES ('Morocco');
INSERT INTO Countries VALUES ('Mozambique');
INSERT INTO Countries VALUES ('Myanmar');
INSERT INTO Countries VALUES ('Namibia');
INSERT INTO Countries VALUES ('Nauru');
INSERT INTO Countries VALUES ('Nepal');
INSERT INTO Countries VALUES ('Netherlands');
INSERT INTO Countries VALUES ('New Zealand');
INSERT INTO Countries VALUES ('Nicaragua');
INSERT INTO Countries VALUES ('Niger');
INSERT INTO Countries VALUES ('Nigeria');
INSERT INTO Countries VALUES ('North Korea');
INSERT INTO Countries VALUES ('North Macedonia');
INSERT INTO Countries VALUES ('Norway');
INSERT INTO Countries VALUES ('Oman');
INSERT INTO Countries VALUES ('Pakistan');
INSERT INTO Countries VALUES ('Palau');
INSERT INTO Countries VALUES ('Panama');
INSERT INTO Countries VALUES ('Papua New Guinea');
INSERT INTO Countries VALUES ('Paraguay');
INSERT INTO Countries VALUES ('Peru');
INSERT INTO Countries VALUES ('Philippines');
INSERT INTO Countries VALUES ('Poland');
INSERT INTO Countries VALUES ('Portugal');
INSERT INTO Countries VALUES ('Qatar');
INSERT INTO Countries VALUES ('Romania');
INSERT INTO Countries VALUES ('Russia');
INSERT INTO Countries VALUES ('Rwanda');
INSERT INTO Countries VALUES ('Saint Kitts and Nevis');
INSERT INTO Countries VALUES ('Saint Lucia');
INSERT INTO Countries VALUES ('Saint Vincent and the Grenadines');
INSERT INTO Countries VALUES ('Samoa');
INSERT INTO Countries VALUES ('San Marino');
INSERT INTO Countries VALUES ('Sao Tome and Principe');
INSERT INTO Countries VALUES ('Saudi Arabia');
INSERT INTO Countries VALUES ('Senegal');
INSERT INTO Countries VALUES ('Serbia');
INSERT INTO Countries VALUES ('Seychelles');
INSERT INTO Countries VALUES ('Sierra Leone');
INSERT INTO Countries VALUES ('Singapore');
INSERT INTO Countries VALUES ('Slovakia');
INSERT INTO Countries VALUES ('Slovenia');
INSERT INTO Countries VALUES ('Solomon Islands');
INSERT INTO Countries VALUES ('Somalia');
INSERT INTO Countries VALUES ('South Africa');
INSERT INTO Countries VALUES ('South Korea');
INSERT INTO Countries VALUES ('South Sudan');
INSERT INTO Countries VALUES ('Spain');
INSERT INTO Countries VALUES ('Sri Lanka');
INSERT INTO Countries VALUES ('Sudan');
INSERT INTO Countries VALUES ('Suriname');
INSERT INTO Countries VALUES ('Sweden');
INSERT INTO Countries VALUES ('Switzerland');
INSERT INTO Countries VALUES ('Syria');
INSERT INTO Countries VALUES ('Taiwan');
INSERT INTO Countries VALUES ('Tajikistan');
INSERT INTO Countries VALUES ('Tanzania');
INSERT INTO Countries VALUES ('Thailand');
INSERT INTO Countries VALUES ('Timor-Leste');
INSERT INTO Countries VALUES ('Togo');
INSERT INTO Countries VALUES ('Tonga');
INSERT INTO Countries VALUES ('Trinidad and Tobago');
INSERT INTO Countries VALUES ('Tunisia');
INSERT INTO Countries VALUES ('Turkey');
INSERT INTO Countries VALUES ('Turkmenistan');
INSERT INTO Countries VALUES ('Tuvalu');
INSERT INTO Countries VALUES ('Uganda');
INSERT INTO Countries VALUES ('Ukraine');
INSERT INTO Countries VALUES ('United Arab Emirates');
INSERT INTO Countries VALUES ('United Kingdom');
INSERT INTO Countries VALUES ('United States');
INSERT INTO Countries VALUES ('Uruguay');
INSERT INTO Countries VALUES ('Uzbekistan');
INSERT INTO Countries VALUES ('Vanuatu');
INSERT INTO Countries VALUES ('Vatican City');
INSERT INTO Countries VALUES ('Venezuela');
INSERT INTO Countries VALUES ('Vietnam');
INSERT INTO Countries VALUES ('Yemen');
INSERT INTO Countries VALUES ('Zambia');
INSERT INTO Countries VALUES ('Zimbabwe');

INSERT INTO States VALUES (78,'Andhra Pradesh');
INSERT INTO States VALUES (78,'Arunachal Pradesh');
INSERT INTO States VALUES (78,'Assam');
INSERT INTO States VALUES (78,'Bihar');
INSERT INTO States VALUES (78,'Chhattisgarh');
INSERT INTO States VALUES (78,'Goa');
INSERT INTO States VALUES (78,'Gujarat');
INSERT INTO States VALUES (78,'Haryana');
INSERT INTO States VALUES (78,'Himachal Pradesh');
INSERT INTO States VALUES (78,'Jharkhand');
INSERT INTO States VALUES (78,'Karnataka');
INSERT INTO States VALUES (78,'Kerala');
INSERT INTO States VALUES (78,'Madhya Pradesh');
INSERT INTO States VALUES (78,'Maharashtra');
INSERT INTO States VALUES (78,'Manipur');
INSERT INTO States VALUES (78,'Meghalaya');
INSERT INTO States VALUES (78,'Mizoram');
INSERT INTO States VALUES (78,'Nagaland');
INSERT INTO States VALUES (78,'Odisha');
INSERT INTO States VALUES (78,'Punjab');
INSERT INTO States VALUES (78,'Rajasthan');
INSERT INTO States VALUES (78,'Sikkim');
INSERT INTO States VALUES (78,'Tamil Nadu');
INSERT INTO States VALUES (78,'Telangana');
INSERT INTO States VALUES (78,'Tripura');
INSERT INTO States VALUES (78,'Uttar Pradesh');
INSERT INTO States VALUES (78,'Uttarakhand');
INSERT INTO States VALUES (78,'West Bengal');


INSERT INTO States VALUES (189,'Alabama');
INSERT INTO States VALUES (189,'Alaska');
INSERT INTO States VALUES (189,'Arizona');
INSERT INTO States VALUES (189,'Arkansas');
INSERT INTO States VALUES (189,'California');
INSERT INTO States VALUES (189,'Colorado');
INSERT INTO States VALUES (189,'Florida');
INSERT INTO States VALUES (189,'Georgia');
INSERT INTO States VALUES (189,'Illinois');
INSERT INTO States VALUES (189,'Michigan');
INSERT INTO States VALUES (189,'New York');
INSERT INTO States VALUES (189,'Ohio');
INSERT INTO States VALUES (189,'Pennsylvania');
INSERT INTO States VALUES (189,'Texas');
INSERT INTO States VALUES (189,'Virginia');
INSERT INTO States VALUES (189,'Washington');




INSERT INTO States VALUES (188,'England');
INSERT INTO States VALUES (188,'Scotland');
INSERT INTO States VALUES (188,'Wales');
INSERT INTO States VALUES (188,'Northern Ireland');

INSERT INTO States VALUES (32,'Alberta');
INSERT INTO States VALUES (32,'British Columbia');
INSERT INTO States VALUES (32,'Manitoba');
INSERT INTO States VALUES (32,'New Brunswick');
INSERT INTO States VALUES (32,'Newfoundland and Labrador');
INSERT INTO States VALUES (32,'Nova Scotia');
INSERT INTO States VALUES (32,'Ontario');
INSERT INTO States VALUES (32,'Prince Edward Island');
INSERT INTO States VALUES (32,'Quebec');
INSERT INTO States VALUES (32,'Saskatchewan');

-- Afghanistan (CountryId = 1)
INSERT INTO States VALUES (1,'Kabul');
INSERT INTO States VALUES (1,'Kandahar');
INSERT INTO States VALUES (1,'Herat');
INSERT INTO States VALUES (1,'Balkh');
INSERT INTO States VALUES (1,'Nangarhar');

-- Albania (CountryId = 2)
INSERT INTO States VALUES (2,'Tirana');
INSERT INTO States VALUES (2,'Durres');
INSERT INTO States VALUES (2,'Vlore');
INSERT INTO States VALUES (2,'Shkoder');
INSERT INTO States VALUES (2,'Elbasan');

-- Algeria (CountryId = 3)
INSERT INTO States VALUES (3,'Algiers');
INSERT INTO States VALUES (3,'Oran');
INSERT INTO States VALUES (3,'Constantine');
INSERT INTO States VALUES (3,'Annaba');
INSERT INTO States VALUES (3,'Blida');

-- Andorra (CountryId = 4)
INSERT INTO States VALUES (4,'Andorra la Vella');
INSERT INTO States VALUES (4,'Canillo');
INSERT INTO States VALUES (4,'Encamp');
INSERT INTO States VALUES (4,'La Massana');
INSERT INTO States VALUES (4,'Ordino');

-- Angola (CountryId = 5)
INSERT INTO States VALUES (5,'Luanda');
INSERT INTO States VALUES (5,'Benguela');
INSERT INTO States VALUES (5,'Huambo');
INSERT INTO States VALUES (5,'Huila');
INSERT INTO States VALUES (5,'Uige');

INSERT INTO Qualifications VALUES ('SSLC');
INSERT INTO Qualifications VALUES ('HSC');
INSERT INTO Qualifications VALUES ('Diploma');
INSERT INTO Qualifications VALUES ('B.Sc');
INSERT INTO Qualifications VALUES ('BCA');
INSERT INTO Qualifications VALUES ('B.E');
INSERT INTO Qualifications VALUES ('B.Tech');
INSERT INTO Qualifications VALUES ('MCA');
INSERT INTO Qualifications VALUES ('M.Tech');

select * from Countries
SELECT * FROM Countries
WHERE CountryName = 'India';
UPDATE States
SET CountryId = 76
WHERE CountryId = 78;


-- Tamil Nadu
INSERT INTO Cities VALUES (23,'Chennai');
INSERT INTO Cities VALUES (23,'Coimbatore');
INSERT INTO Cities VALUES (23,'Madurai');
INSERT INTO Cities VALUES (23,'Trichy');
INSERT INTO Cities VALUES (23,'Salem');

-- Kerala
INSERT INTO Cities VALUES (12,'Kochi');
INSERT INTO Cities VALUES (12,'Thiruvananthapuram');
INSERT INTO Cities VALUES (12,'Kozhikode');
INSERT INTO Cities VALUES (12,'Thrissur');
INSERT INTO Cities VALUES (12,'Kannur');

-- Karnataka
INSERT INTO Cities VALUES (11,'Bengaluru');
INSERT INTO Cities VALUES (11,'Mysuru');
INSERT INTO Cities VALUES (11,'Mangalore');
INSERT INTO Cities VALUES (11,'Hubli');
INSERT INTO Cities VALUES (11,'Belagavi');


select * from states
-- Texas
INSERT INTO Cities VALUES (189,'Houston');
INSERT INTO Cities VALUES (189,'Dallas');
INSERT INTO Cities VALUES (189,'Austin');
INSERT INTO Cities VALUES (189,'San Antonio');
INSERT INTO Cities VALUES (189,'Fort Worth');

-- California
INSERT INTO Cities VALUES (33,'Los Angeles');
INSERT INTO Cities VALUES (33,'San Diego');
INSERT INTO Cities VALUES (33,'San Jose');
INSERT INTO Cities VALUES (33,'San Francisco');
INSERT INTO Cities VALUES (33,'Sacramento');