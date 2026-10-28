USE PlacementDB;
GO

DROP TABLE IF EXISTS dbo.Placement;
DROP TABLE IF EXISTS dbo.Company;
DROP TABLE IF EXISTS dbo.Student;
GO

CREATE TABLE dbo.Student
(
    StudentId    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Student PRIMARY KEY,
    EnrollmentNo VARCHAR(20)       NOT NULL CONSTRAINT UQ_Student_EnrollmentNo UNIQUE,
    StudentName  NVARCHAR(100)     NOT NULL,
    Department   NVARCHAR(80)      NOT NULL,
    Email        VARCHAR(120)      NULL,
    CGPA         DECIMAL(3,2)      NULL
);

CREATE TABLE dbo.Company
(
    CompanyId   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Company PRIMARY KEY,
    CompanyName NVARCHAR(100)     NOT NULL,
    JobRole     NVARCHAR(100)     NOT NULL
);

CREATE TABLE dbo.Placement
(
    PlacementId     INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Placement PRIMARY KEY,
    StudentId       INT           NOT NULL CONSTRAINT FK_Placement_Student REFERENCES dbo.Student(StudentId),
    CompanyId       INT           NOT NULL CONSTRAINT FK_Placement_Company REFERENCES dbo.Company(CompanyId),
    PackageLPA      DECIMAL(8,2)  NOT NULL CONSTRAINT CK_Placement_Package CHECK (PackageLPA >= 0),
    PlacementStatus NVARCHAR(20)  NOT NULL CONSTRAINT CK_Placement_Status
                    CHECK (PlacementStatus IN ('Placed', 'In Process', 'Not Placed')),
    PlacementDate   DATE          NULL
);
GO

INSERT INTO dbo.Student (EnrollmentNo, StudentName, Department, Email, CGPA) VALUES
('CE101', N'Nirav Parmar',   N'Computer Engineering',   'nirav@example.com',  8.60),
('CE102', N'Hetvi Kapadia',  N'Computer Engineering',   'hetvi@example.com',  9.20),
('CE103', N'Yash Solanki',   N'Information Technology', 'yash@example.com',   7.90),
('CE104', N'Meera Bhatt',    N'Computer Engineering',   'meera@example.com',  8.30),
('CE105', N'Kunal Raval',    N'Information Technology', 'kunal@example.com',  7.10),
('CE106', N'Dhruvi Pandya',  N'Computer Engineering',   'dhruvi@example.com', 8.00),
('CE107', N'Parth Vyas',     N'Information Technology', 'parth@example.com',  8.45);

INSERT INTO dbo.Company (CompanyName, JobRole) VALUES
(N'NexaSoft',    N'Software Developer'),
(N'CloudNest',   N'Cloud Engineer'),
(N'DataPulse',   N'Data Analyst'),
(N'PixelForge',  N'UI Developer'),
(N'SecureLayer', N'Security Analyst');

INSERT INTO dbo.Placement (StudentId, CompanyId, PackageLPA, PlacementStatus, PlacementDate) VALUES
(1, 1, 6.80, 'Placed',     '2026-03-10'),
(2, 2, 9.00, 'Placed',     '2026-03-14'),
(3, 3, 4.80, 'In Process', NULL),
(4, 1, 7.50, 'Placed',     '2026-03-18'),
(5, 4, 3.60, 'Not Placed', NULL),
(6, 3, 5.90, 'In Process', NULL),
(7, 5, 6.25, 'Placed',     '2026-03-22');
GO

SELECT * FROM dbo.Student;
SELECT * FROM dbo.Company;
SELECT * FROM dbo.Placement;
GO
