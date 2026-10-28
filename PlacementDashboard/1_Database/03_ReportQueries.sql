USE PlacementDB;
GO

PRINT 'DASHBOARD - summary counts';
SELECT COUNT(*) AS TotalStudents FROM dbo.Student;

SELECT COUNT(DISTINCT StudentId) AS PlacedStudents
FROM   dbo.Placement
WHERE  PlacementStatus = 'Placed';

SELECT COUNT(*) AS TotalCompanies FROM dbo.Company;

SELECT AVG(PackageLPA) AS AveragePackageLPA
FROM   dbo.Placement
WHERE  PlacementStatus = 'Placed';

PRINT 'REPORT - joined placement records';
SELECT s.EnrollmentNo, s.StudentName, s.Department, c.CompanyName, c.JobRole,
       p.PackageLPA, p.PlacementStatus, p.PlacementDate
FROM   dbo.Placement p
JOIN   dbo.Student s ON s.StudentId = p.StudentId
JOIN   dbo.Company c ON c.CompanyId = p.CompanyId
ORDER  BY s.StudentName;

PRINT 'REPORT - company-wise placed count';
SELECT c.CompanyName, c.JobRole,
       SUM(CASE WHEN p.PlacementStatus = 'Placed' THEN 1 ELSE 0 END) AS PlacedCount
FROM   dbo.Company c
LEFT JOIN dbo.Placement p ON p.CompanyId = c.CompanyId
GROUP  BY c.CompanyName, c.JobRole
ORDER  BY c.CompanyName;
GO
