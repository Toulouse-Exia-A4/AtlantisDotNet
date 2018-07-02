CREATE TABLE [dbo].[Admin]
(
	[Id] INT NOT NULL IDENTITY(0,1) PRIMARY KEY,
	[AdminId] VARCHAR(50) NOT NULL, 
	[Password] VARCHAR(MAX) NOT NULL,
    CONSTRAINT [AK_Admin_AdminId] UNIQUE ([AdminId])
)

GO

CREATE TABLE [dbo].[DeviceType]
(
	[Id] INT NOT NULL IDENTITY(0,1) PRIMARY KEY,
	[Type] VARCHAR(50) NOT NULL,
	[Unit] VARCHAR(MAX) NULL,
	CONSTRAINT [AK_DeviceType_Type] UNIQUE ([Type])
)

GO

CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY(0,1) PRIMARY KEY,
	[UserId] VARCHAR(50) NOT NULL,
	[Firstname] VARCHAR(50) NULL,
	[Lastname] VARCHAR(50) NULL,
	CONSTRAINT [AK_User_UserId] UNIQUE ([UserId])
)

GO

CREATE TABLE [dbo].[Device]
(
	[Id] INT NOT NULL IDENTITY(0,1) PRIMARY KEY,
	[DeviceId] VARCHAR(100) NOT NULL,
	[DeviceTypeId] INT NOT NULL,
	[UserId] INT NULL,
    CONSTRAINT [FK_Device_DeviceType] FOREIGN KEY ([DeviceTypeId]) REFERENCES [DeviceType]([Id]),
	CONSTRAINT [FK_Device_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
	CONSTRAINT [AK_Device_DeviceId] UNIQUE ([DeviceId])
)

GO