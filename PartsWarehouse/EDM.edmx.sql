
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/27/2022 01:28:49
-- Generated from EDMX file: C:\Users\milic\source\repos\PartsWarehouse\PartsWarehouse\EDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PartsWarehouseDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Parts_Car]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parts] DROP CONSTRAINT [FK_Parts_Car];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCar_Car]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserCar] DROP CONSTRAINT [FK_UserCar_Car];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCar_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserCar] DROP CONSTRAINT [FK_UserCar_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Car]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Car];
GO
IF OBJECT_ID(N'[dbo].[Parts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parts];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserCar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserCar];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Car'
CREATE TABLE [dbo].[Car] (
    [IdCar] int  NOT NULL,
    [Company] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Generation] int  NOT NULL
);
GO

-- Creating table 'Parts'
CREATE TABLE [dbo].[Parts] (
    [IdPart] int  NOT NULL,
    [IdCar] int  NOT NULL,
    [Type] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Manufacturer] nvarchar(50)  NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [Image] varbinary(max)  NULL,
    [Remain] int  NOT NULL,
    [Price] float  NOT NULL,
    [PartNum] int  NOT NULL,
    [Original] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [IdUser] int  NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserCar'
CREATE TABLE [dbo].[UserCar] (
    [Id] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [IdCar] int  NOT NULL,
    [Vin] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdCar] in table 'Car'
ALTER TABLE [dbo].[Car]
ADD CONSTRAINT [PK_Car]
    PRIMARY KEY CLUSTERED ([IdCar] ASC);
GO

-- Creating primary key on [IdPart] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [PK_Parts]
    PRIMARY KEY CLUSTERED ([IdPart] ASC);
GO

-- Creating primary key on [IdUser] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- Creating primary key on [Id] in table 'UserCar'
ALTER TABLE [dbo].[UserCar]
ADD CONSTRAINT [PK_UserCar]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdCar] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [FK_Parts_Car]
    FOREIGN KEY ([IdCar])
    REFERENCES [dbo].[Car]
        ([IdCar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Parts_Car'
CREATE INDEX [IX_FK_Parts_Car]
ON [dbo].[Parts]
    ([IdCar]);
GO

-- Creating foreign key on [IdCar] in table 'UserCar'
ALTER TABLE [dbo].[UserCar]
ADD CONSTRAINT [FK_UserCar_Car]
    FOREIGN KEY ([IdCar])
    REFERENCES [dbo].[Car]
        ([IdCar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCar_Car'
CREATE INDEX [IX_FK_UserCar_Car]
ON [dbo].[UserCar]
    ([IdCar]);
GO

-- Creating foreign key on [IdUser] in table 'UserCar'
ALTER TABLE [dbo].[UserCar]
ADD CONSTRAINT [FK_UserCar_User]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[User]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCar_User'
CREATE INDEX [IX_FK_UserCar_User]
ON [dbo].[UserCar]
    ([IdUser]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------