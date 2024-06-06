create database crmdb;
USE crmdb;

Create Table Registration (ID INT Identity(1,1) Primary Key, Name VARCHAR(100), Email VARCHAR(100),
Password VARCHAR(100), PhoneNumber VARCHAR(100), IsActive INT, IsApproved INT);

Create Table Article (ID INT Identity(1,1) Primary Key, Title VARCHAR(100), Content VARCHAR(100),
Email VARCHAR(100), Image VARCHAR(100), IsActive INT, IsApproved INT);

Create Table News (ID INT Identity(1,1) Primary Key, Title VARCHAR(100), Content VARCHAR(100),
Email VARCHAR(100), IsActive INT, CreatedOn Datetime);

Create Table Events  (ID INT Identity(1,1) Primary Key, Title VARCHAR(100), Content VARCHAR(100),
Email VARCHAR(100), IsActive INT, CreatedOn Datetime);

Create Table Staff (ID INT Identity(1,1) Primary Key, Name VARCHAR(100), Email VARCHAR(100),
Password VARCHAR(100), IsActive INT);
 
SELECT * FROM Registration;
SELECT * FROM Article;
SELECT * FROM News WHERE IsActive = 1;
SELECT * FROM Events;
SELECT * FROM Staff;