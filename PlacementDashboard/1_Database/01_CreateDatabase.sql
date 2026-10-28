USE master;
GO

IF DB_ID(N'PlacementDB') IS NULL
BEGIN
    CREATE DATABASE PlacementDB;
    PRINT 'Database PlacementDB created.';
END
ELSE
    PRINT 'Database PlacementDB already exists.';
GO

USE PlacementDB;
GO
