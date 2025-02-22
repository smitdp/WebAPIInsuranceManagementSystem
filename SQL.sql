﻿create database InsuranceAndClaimManagementDB;
use InsuranceAndClaimManagementDB;

CREATE TABLE Roles (
    ID INT PRIMARY KEY IDENTITY,
    RoleName VARCHAR(50) NOT NULL
);


CREATE TABLE Users (
    ID INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20),
    RoleId INT FOREIGN KEY REFERENCES Roles(ID) NOT NULL,
    IsApproved INT, --default 0 for admin and agent. 1 for approved, 2 for rejected, for customer 3(no need to be approved)
    IsActive BIT NOT NULL
);


CREATE TABLE PolicyTypes (
    ID INT PRIMARY KEY IDENTITY,
    PolicyTypeName VARCHAR(100) NOT NULL
);


CREATE TABLE Policies (
    ID INT PRIMARY KEY IDENTITY,
    PolicyNumber VARCHAR(50) NOT NULL,
    PolicyTypeID INT FOREIGN KEY REFERENCES PolicyTypes(ID) NOT NULL,
    PolicyName VARCHAR(100),
    Duration INT,
    Description VARCHAR(MAX),
    Installment DECIMAL(18,2),
    PremiumAmount DECIMAL(18,2),
    IsActive BIT NOT NULL
);


CREATE TABLE DocumentList (
    ID INT PRIMARY KEY IDENTITY,
    PolicyId INT FOREIGN KEY REFERENCES Policies(ID) NOT NULL,
    DocumentType VARCHAR(100) NOT NULL
);


CREATE TABLE UserPolicy (
    ID INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(ID) NOT NULL,
    PolicyId INT FOREIGN KEY REFERENCES Policies(ID) NOT NULL,
	AgentId INT FOREIGN KEY REFERENCES Users(ID) NOT NULL,
    EnrollmentDate DATE NOT NULL,
    EndDate DATE
);

ALTER TABLE UserPolicy
ALTER COLUMN EnrollmentDate DATETIME NOT NULL
ALTER COLUMN EndDate DATETIME;


CREATE TABLE Claims (
    ID INT PRIMARY KEY IDENTITY,
    PolicyId INT FOREIGN KEY REFERENCES Policies(ID) NOT NULL,
    UserId INT FOREIGN KEY REFERENCES Users(ID) NOT NULL,
    IncidentDate DATE NOT NULL,
    IncidentLocation VARCHAR(100),
    Address VARCHAR(255),
    Description VARCHAR(MAX),
    Status INT 
);

--1 for Pending
--2 for Under Review
--3 for Approved
--4 for Denied
--5 for Processing
--6 for Done
--7 for Closed



CREATE TABLE ClaimDocuments (
    ID INT PRIMARY KEY IDENTITY,
    ClaimId INT FOREIGN KEY REFERENCES Claims(ID) NOT NULL,
    DocumentPath VARCHAR(255) NOT NULL
);


CREATE TABLE AuditLogs (
    ID INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(ID) NOT NULL,
    Timestamp DATETIME NOT NULL,
    Action VARCHAR(100) NOT NULL,
    Category VARCHAR(100), 
    Details VARCHAR(MAX),
    IsSuccess BIT
);


INSERT INTO Roles (RoleName)
VALUES ('Insurer'), ('Agent'), ('Claimant');

INSERT INTO Users (FirstName, LastName, Password, Email, PhoneNumber, RoleId, IsApproved, IsActive)
VALUES ('Admin', 'Admin', 'adminpassword', 'admin@example.com', '1234567890', 1, 1, 1);

UPDATE Users
SET Email = 'admin@gmail.com'
WHERE FirstName = 'Admin' AND LastName = 'Admin' AND Email = 'admin@example.com';




																										
INSERT INTO PolicyTypes (PolicyTypeName)
VALUES ('Health'), ('Life'), ('Automobile'), ('Home'), ('Travel');



INSERT INTO Policies (PolicyNumber, PolicyTypeID, PolicyName, Duration, Description, Installment, PremiumAmount, IsActive)
VALUES 
-- Health Policies
('HEA001', 1, 'HealthGuard Plus', 365, 'Comprehensive health insurance for individuals.', 1500 , 100000, 1),
('HEA002', 1, 'Wellness Shield', 365, 'Health insurance policy with comprehensive coverage.', 1250, 90000, 1),

-- Life Policies
('LIF001', 2, 'LifeSure Protector', 730, 'Term life insurance policy for up to 2 years.', 2100,300000 , 1),
('LIF002', 2, 'EternalLife Assurance', 1095, 'Whole life insurance policy with lifetime coverage.', 4000, 1000000, 1),

-- Automobile Policies
('AUT001', 3, 'AutoShield Comprehensive', 365, 'Comprehensive insurance coverage for automobiles.', 2000, 150000, 1),
('AUT002', 3, 'DriveSafe Liability Coverage', 365, 'Third-party liability insurance for automobiles.', 3000, 300000, 1),

-- Home Policies
('HOM001', 4, 'HomeSafe Property Insurance', 365, 'Home insurance policy covering property and belongings.', 4500, 500000, 1),
('HOM002', 4, 'RentGuard Tenant Insurance', 365, 'Renter insurance policy for tenants.', 5500, 700000, 1),

-- Travel Policies
('TRA001', 5, 'TravelCare Protection', 7, 'Travel insurance policy for short-term trips.', 999, 100000, 1),
('TRA002', 5, 'GlobalGuard Medical Coverage', 15, 'International travel insurance policy with medical coverage.', 1499, 500000, 1)


-- Inserting approved agents
INSERT INTO Users (FirstName, LastName, Password, Email, PhoneNumber, RoleId, IsApproved, IsActive)
VALUES 
    ('Agent1', 'Agent', 'agent1password', 'agent1@example.com', '1234567890', 2, 1, 1),
    ('Agent2', 'Agent', 'agent2password', 'agent2@example.com', '1234567891', 2, 1, 2),
    ('Agent3', 'Agent', 'agent3password', 'agent3@example.com', '1234567892', 2, 1, 1),
    ('Agent4', 'Agent', 'agent4password', 'agent4@example.com', '1234567893', 2, 1, 1),
    ('Agent5', 'Agent', 'agent5password', 'agent5@example.com', '1234567894', 2, 1, 2),
    ('Agent6', 'Agent', 'agent6password', 'agent6@example.com', '1234567895', 2, 1, 1),
    ('Agent7', 'Agent', 'agent7password', 'agent7@example.com', '1234567896', 2, 1, 0),
    ('Agent8', 'Agent', 'agent8password', 'agent8@example.com', '1234567897', 2, 1, 1),
    ('Agent9', 'Agent', 'agent9password', 'agent9@example.com', '1234567898', 2, 1, 0),
    ('Agent10', 'Agent', 'agent10password', 'agent10@example.com', '1234567899', 2, 1, 1),
    ('Agent11', 'Agent', 'agent11password', 'agent11@example.com', '1234567800', 2, 1, 1),
    ('Agent12', 'Agent', 'agent12password', 'agent12@example.com', '1234567801', 2, 2, 1),
    ('Agent13', 'Agent', 'agent13password', 'agent13@example.com', '1234567802', 2, 0, 1),
    ('Agent14', 'Agent', 'agent14password', 'agent14@example.com', '1234567803', 2, 1, 1),
    ('Agent15', 'Agent', 'agent15password', 'agent15@example.com', '1234567804', 2, 2, 1);



--DELETE FROM UserPolicy;
--DBCC CHECKIDENT ('UserPolicy', RESEED, 0);

select * from Roles;
select * from Users;
select * from PolicyTypes;
select * from Policies;
select * from UserPolicy;
select * from Claims;
select * from ClaimDocuments;

delete from Claims;
delete from ClaimDocuments;

