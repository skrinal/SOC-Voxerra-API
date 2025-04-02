-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 17, 2025 at 11:57 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `voxerra`
--

-- --------------------------------------------------------

--
-- Table structure for table `Tblmessages`
--

CREATE TABLE `Tblmessages` (
  `Id` int(11) NOT NULL,
  `FromUserId` int(11) NOT NULL,
  `ToUserId` int(11) NOT NULL,
  `Content` longtext NOT NULL,
  `SendDateTime` datetime(6) NOT NULL,
  `IsRead` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Tblpendingfriendrequest`
--

CREATE TABLE `Tblpendingfriendrequest` (
  `Id` int(11) NOT NULL,
  `FromUserId` int(11) NOT NULL,
  `ToUserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Tblpendingpassword`
--

CREATE TABLE `Tblpendingpassword` (
  `Id` int(11) NOT NULL,
  `Code` int(11) NOT NULL,
  `Email` longtext NOT NULL,
  `ExpireTime` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Tblpendingusers`
--

CREATE TABLE `Tblpendingusers` (
  `Id` int(11) NOT NULL,
  `UserName` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `StoredSalt` longblob NOT NULL,
  `VerificationCode` int(11) NOT NULL,
  `ExpireTime` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Tbltwofactorauth`
--

CREATE TABLE `Tbltwofactorauth` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `Code` int(11) NOT NULL,
  `ExpireTime` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Tbluserfriends`
--

CREATE TABLE `Tbluserfriends` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `FriendId` int(11) NOT NULL,
  `NickName` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Tblusers`
--

CREATE TABLE `Tblusers` (
  `Id` int(11) NOT NULL,
  `UserName` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `StoredSalt` longblob NOT NULL,
  `AvatarSourceName` longtext NOT NULL,
  `Bio` longtext NOT NULL,
  `IsOnline` tinyint(1) NOT NULL,
  `LastLogonTime` datetime(6) NOT NULL,
  `CreationDate` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `Tblusers`
--

INSERT INTO `Tblusers` (`Id`, `UserName`, `Password`, `Email`, `StoredSalt`, `AvatarSourceName`, `Bio`, `IsOnline`, `LastLogonTime`, `CreationDate`) VALUES
(3, 'skrinal', 'o32t2xkWo8bzB37k37p2QynKApgMh7UFDKWuheADO6c=', 'skrinalghost@gmail.com', 0xd46b8f25b709ba2102dc7d3a9d224b56c00dd87eb0c16192c462aede2b766277, 'defaulticon.png', '', 0, '2025-02-17 02:01:10.898076', '2025-02-17 02:01:10.898164');

-- --------------------------------------------------------

--
-- Table structure for table `Tblusersettings`
--

CREATE TABLE `Tblusersettings` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LoginAlertsEnabled` tinyint(1) NOT NULL,
  `WhereIsUserLoggedIn` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `Tblusersettings`
--

INSERT INTO `Tblusersettings` (`Id`, `UserId`, `TwoFactorEnabled`, `LoginAlertsEnabled`, `WhereIsUserLoggedIn`) VALUES
(3, 3, 0, 0, '');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250211180044_xeee', '8.0.11');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Tblmessages`
--
ALTER TABLE `Tblmessages`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Tblmessages_FromUserId` (`FromUserId`),
  ADD KEY `IX_Tblmessages_ToUserId` (`ToUserId`);

--
-- Indexes for table `Tblpendingfriendrequest`
--
ALTER TABLE `Tblpendingfriendrequest`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Tblpendingfriendrequest_FromUserId` (`FromUserId`),
  ADD KEY `IX_Tblpendingfriendrequest_ToUserId` (`ToUserId`);

--
-- Indexes for table `Tblpendingpassword`
--
ALTER TABLE `Tblpendingpassword`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `Tblpendingusers`
--
ALTER TABLE `Tblpendingusers`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `Tbltwofactorauth`
--
ALTER TABLE `Tbltwofactorauth`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Tbltwofactorauth_UserId` (`UserId`);

--
-- Indexes for table `Tbluserfriends`
--
ALTER TABLE `Tbluserfriends`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Tbluserfriends_FriendId` (`FriendId`),
  ADD KEY `IX_Tbluserfriends_UserId` (`UserId`);

--
-- Indexes for table `Tblusers`
--
ALTER TABLE `Tblusers`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `Tblusersettings`
--
ALTER TABLE `Tblusersettings`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Tblusersettings_UserId` (`UserId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Tblmessages`
--
ALTER TABLE `Tblmessages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Tblpendingfriendrequest`
--
ALTER TABLE `Tblpendingfriendrequest`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Tblpendingpassword`
--
ALTER TABLE `Tblpendingpassword`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Tblpendingusers`
--
ALTER TABLE `Tblpendingusers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `Tbltwofactorauth`
--
ALTER TABLE `Tbltwofactorauth`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Tbluserfriends`
--
ALTER TABLE `Tbluserfriends`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Tblusers`
--
ALTER TABLE `Tblusers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `Tblusersettings`
--
ALTER TABLE `Tblusersettings`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Tblmessages`
--
ALTER TABLE `Tblmessages`
  ADD CONSTRAINT `FK_Tblmessages_Tblusers_FromUserId` FOREIGN KEY (`FromUserId`) REFERENCES `Tblusers` (`Id`),
  ADD CONSTRAINT `FK_Tblmessages_Tblusers_ToUserId` FOREIGN KEY (`ToUserId`) REFERENCES `Tblusers` (`Id`);

--
-- Constraints for table `Tblpendingfriendrequest`
--
ALTER TABLE `Tblpendingfriendrequest`
  ADD CONSTRAINT `FK_Tblpendingfriendrequest_Tblusers_FromUserId` FOREIGN KEY (`FromUserId`) REFERENCES `Tblusers` (`Id`),
  ADD CONSTRAINT `FK_Tblpendingfriendrequest_Tblusers_ToUserId` FOREIGN KEY (`ToUserId`) REFERENCES `Tblusers` (`Id`);

--
-- Constraints for table `Tbltwofactorauth`
--
ALTER TABLE `Tbltwofactorauth`
  ADD CONSTRAINT `FK_Tbltwofactorauth_Tblusers_UserId` FOREIGN KEY (`UserId`) REFERENCES `Tblusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `Tbluserfriends`
--
ALTER TABLE `Tbluserfriends`
  ADD CONSTRAINT `FK_Tbluserfriends_Tblusers_FriendId` FOREIGN KEY (`FriendId`) REFERENCES `Tblusers` (`Id`),
  ADD CONSTRAINT `FK_Tbluserfriends_Tblusers_UserId` FOREIGN KEY (`UserId`) REFERENCES `Tblusers` (`Id`);

--
-- Constraints for table `Tblusersettings`
--
ALTER TABLE `Tblusersettings`
  ADD CONSTRAINT `FK_Tblusersettings_Tblusers_UserId` FOREIGN KEY (`UserId`) REFERENCES `Tblusers` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
