CREATE DATABASE LendingProcessingSystem;
GO
USE LendingProcessingSystem;
GO

CREATE TABLE AspNetRoles
(
    Id NVARCHAR(450) NOT NULL PRIMARY KEY,
    Name NVARCHAR(256) NULL,
    NormalizedName NVARCHAR(256) NULL,
    ConcurrencyStamp NVARCHAR(MAX) NULL
);

CREATE TABLE AspNetUsers
(
    Id NVARCHAR(450) NOT NULL PRIMARY KEY,
    UserName NVARCHAR(256) NULL,
    NormalizedUserName NVARCHAR(256) NULL,
    Email NVARCHAR(256) NULL,
    NormalizedEmail NVARCHAR(256) NULL,
    EmailConfirmed BIT NOT NULL DEFAULT 0,
    PasswordHash NVARCHAR(MAX) NULL,
    SecurityStamp NVARCHAR(MAX) NULL,
    ConcurrencyStamp NVARCHAR(MAX) NULL,
    PhoneNumber NVARCHAR(50) NULL,
    PhoneNumberConfirmed BIT NOT NULL DEFAULT 0,
    TwoFactorEnabled BIT NOT NULL DEFAULT 0,
    LockoutEnd DATETIMEOFFSET NULL,
    LockoutEnabled BIT NOT NULL DEFAULT 0,
    AccessFailedCount INT NOT NULL DEFAULT 0,
    FirstName NVARCHAR(100) NULL,
    LastName NVARCHAR(100) NULL
);

CREATE TABLE AspNetUserRoles
(
    UserId NVARCHAR(450) NOT NULL,
    RoleId NVARCHAR(450) NOT NULL,
    CONSTRAINT PK_AspNetUserRoles PRIMARY KEY (UserId, RoleId)
);

CREATE TABLE UserProfiles
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(100) NULL,
    LastName NVARCHAR(100) NULL,
    Phone NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(450) NULL,
    UpdatedAt DATETIME NULL,
    LastLoginAt DATETIME NULL
);

CREATE TABLE UserResetTokens
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Token NVARCHAR(500) NOT NULL,
    ExpiryAt DATETIME NOT NULL,
    IsUsed BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Products
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(500) NULL,
    WorkflowId INT NULL,
    Status INT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);

CREATE TABLE Workflows
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE WorkflowSteps
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    WorkflowId INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    StepType INT NOT NULL,
    Description NVARCHAR(500) NULL,
    SortOrder INT NOT NULL,
    ConditionExpression NVARCHAR(500) NULL,
    ApiDefinitionId INT NULL,
    RequestTemplate NVARCHAR(MAX) NULL,
    ResponseMapping NVARCHAR(MAX) NULL,
    TimeoutSeconds INT NOT NULL DEFAULT 60,
    IsRequired BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Cases
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CaseNumber NVARCHAR(50) NOT NULL UNIQUE,
    BorrowerName NVARCHAR(200) NOT NULL,
    NationalId NVARCHAR(50) NULL,
    Age INT NOT NULL,
    LoanAmount DECIMAL(18,2) NOT NULL,
    ProductId INT NOT NULL,
    CurrentStage NVARCHAR(200) NOT NULL DEFAULT 'Received',
    Status INT NOT NULL DEFAULT 0,
    Notes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    CreatedBy NVARCHAR(450) NULL
);

CREATE TABLE AuditTrail
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NULL,
    UserEmail NVARCHAR(255) NULL,
    EntityName NVARCHAR(200) NOT NULL,
    EntityId INT NULL,
    ActionType NVARCHAR(100) NOT NULL,
    CaseId INT NULL,
    WorkflowStepId INT NULL,
    OldValueJson NVARCHAR(MAX) NULL,
    NewValueJson NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(450) NULL,
    Source NVARCHAR(200) NULL,
    IpAddress NVARCHAR(100) NULL
);

CREATE TABLE ReportDefinitions
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) NULL,
    StoredProcedureName NVARCHAR(200) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedBy NVARCHAR(450) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ReportColumns
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReportDefinitionId INT NOT NULL,
    ColumnName NVARCHAR(200) NOT NULL,
    DisplayName NVARCHAR(200) NOT NULL,
    SortOrder INT NOT NULL,
    IsVisible BIT NOT NULL DEFAULT 1,
    DataType NVARCHAR(50) NOT NULL DEFAULT 'string',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ReportFilters
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReportDefinitionId INT NOT NULL,
    FilterName NVARCHAR(200) NOT NULL,
    FilterType NVARCHAR(50) NOT NULL,
    DefaultValue NVARCHAR(200) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ScheduledJobs
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    JobName NVARCHAR(200) NOT NULL,
    CronExpression NVARCHAR(100) NOT NULL,
    JobType NVARCHAR(100) NOT NULL,
    IsEnabled BIT NOT NULL DEFAULT 1,
    LastRunAt DATETIME NULL,
    NextRunAt DATETIME NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ApiDefinitions
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) NULL,
    ApiType INT NOT NULL,
    BaseUrl NVARCHAR(500) NULL,
    HttpMethod NVARCHAR(20) NOT NULL DEFAULT 'POST',
    AuthType INT NOT NULL DEFAULT 0,
    AuthHeaderName NVARCHAR(200) NULL,
    AuthHeaderValue NVARCHAR(500) NULL,
    RequestTemplate NVARCHAR(MAX) NULL,
    ResponseMapping NVARCHAR(MAX) NULL,
    TimeoutSeconds INT NOT NULL DEFAULT 60,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ApiParameters
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ApiDefinitionId INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    DataType NVARCHAR(50) NOT NULL DEFAULT 'string',
    SourceType NVARCHAR(50) NOT NULL DEFAULT 'CaseField',
    SourceValue NVARCHAR(200) NULL,
    IsRequired BIT NOT NULL DEFAULT 1,
    DefaultValue NVARCHAR(200) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ApiExecutionLogs
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CaseId INT NULL,
    WorkflowStepId INT NULL,
    ApiDefinitionId INT NOT NULL,
    RequestPayload NVARCHAR(MAX) NULL,
    ResponsePayload NVARCHAR(MAX) NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    ErrorMessage NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

CREATE OR ALTER PROCEDURE dbo.usp_UserProfile_Create
    @UserId NVARCHAR(450),
    @Email NVARCHAR(255),
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Phone NVARCHAR(50),
    @CreatedBy NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO UserProfiles
    (
        UserId, Email, FirstName, LastName, Phone, IsActive, IsDeleted, CreatedAt, CreatedBy
    )
    VALUES
    (
        @UserId, @Email, @FirstName, @LastName, @Phone, 1, 0, GETDATE(), @CreatedBy
    );
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_UserProfile_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM UserProfiles
    WHERE IsDeleted = 0
    ORDER BY CreatedAt DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_UserProfile_SetActiveState
    @UserId NVARCHAR(450),
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE UserProfiles
    SET IsActive = @IsActive, UpdatedAt = GETDATE()
    WHERE UserId = @UserId;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_UserResetToken_Create
    @UserId NVARCHAR(450),
    @Email NVARCHAR(255),
    @Token NVARCHAR(500),
    @ExpiryAt DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO UserResetTokens (UserId, Email, Token, ExpiryAt, IsUsed, CreatedAt)
    VALUES (@UserId, @Email, @Token, @ExpiryAt, 0, GETDATE());
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_UserResetToken_Validate
    @Token NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 *
    FROM UserResetTokens
    WHERE Token = @Token
      AND IsUsed = 0
      AND ExpiryAt > GETDATE();
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_UserResetToken_MarkUsed
    @Token NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE UserResetTokens
    SET IsUsed = 1
    WHERE Token = @Token;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_AuditTrail_Insert
    @UserId NVARCHAR(450),
    @UserEmail NVARCHAR(255),
    @EntityName NVARCHAR(200),
    @EntityId INT,
    @ActionType NVARCHAR(100),
    @CaseId INT,
    @WorkflowStepId INT,
    @OldValueJson NVARCHAR(MAX),
    @NewValueJson NVARCHAR(MAX),
    @CreatedBy NVARCHAR(450),
    @Source NVARCHAR(200),
    @IpAddress NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditTrail
    (
        UserId, UserEmail, EntityName, EntityId, ActionType,
        CaseId, WorkflowStepId, OldValueJson, NewValueJson,
        CreatedBy, Source, IpAddress, CreatedAt
    )
    VALUES
    (
        @UserId, @UserEmail, @EntityName, @EntityId, @ActionType,
        @CaseId, @WorkflowStepId, @OldValueJson, @NewValueJson,
        @CreatedBy, @Source, @IpAddress, GETDATE()
    );
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Report_CaseSummary
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.CaseNumber,
        c.BorrowerName,
        p.Name AS ProductName,
        c.Status,
        c.CurrentStage,
        c.LoanAmount,
        c.CreatedAt
    FROM Cases c
    INNER JOIN Products p ON p.Id = c.ProductId
    ORDER BY c.CreatedAt DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_ReportDefinition_Create
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @StoredProcedureName NVARCHAR(200),
    @CreatedBy NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ReportDefinitions (Name, Description, StoredProcedureName, IsActive, CreatedBy, CreatedAt)
    VALUES (@Name, @Description, @StoredProcedureName, 1, @CreatedBy, GETDATE());
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_ScheduledJob_Create
    @JobName NVARCHAR(200),
    @CronExpression NVARCHAR(100),
    @JobType NVARCHAR(100),
    @IsEnabled BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ScheduledJobs (JobName, CronExpression, JobType, IsEnabled, CreatedAt)
    VALUES (@JobName, @CronExpression, @JobType, @IsEnabled, GETDATE());
END
GO
