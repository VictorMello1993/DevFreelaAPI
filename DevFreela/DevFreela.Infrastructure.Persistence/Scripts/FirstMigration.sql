--dotnet ef migrations script 0 FirstMigration -s ../DevFreela.API -o ./Scripts/FirstMigration.sql

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Skills` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Skills` PRIMARY KEY (`Id`)
);

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    `BirthDate` datetime(6) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `Active` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
);

CREATE TABLE `ProvidedServices` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `IdClient` int NOT NULL,
    `IdFreelancer` int NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `StartedAt` datetime(6) NULL,
    `FinishedAt` datetime(6) NULL,
    `Status` int NOT NULL,
    `TotalCost` decimal(65,30) NOT NULL,
    CONSTRAINT `PK_ProvidedServices` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ProvidedServices_Users_IdClient` FOREIGN KEY (`IdClient`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_ProvidedServices_Users_IdFreelancer` FOREIGN KEY (`IdFreelancer`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `UsersSkills` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `IdUser` int NOT NULL,
    `IdSkill` int NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_UsersSkills` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UsersSkills_Skills_IdSkill` FOREIGN KEY (`IdSkill`) REFERENCES `Skills` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_UsersSkills_Users_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_ProvidedServices_IdClient` ON `ProvidedServices` (`IdClient`);

CREATE INDEX `IX_ProvidedServices_IdFreelancer` ON `ProvidedServices` (`IdFreelancer`);

CREATE INDEX `IX_UsersSkills_IdSkill` ON `UsersSkills` (`IdSkill`);

CREATE INDEX `IX_UsersSkills_IdUser` ON `UsersSkills` (`IdUser`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201121203039_FirstMigration', '3.1.10');

