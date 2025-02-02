-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 02, 2025 at 07:30 PM
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
-- Table structure for table `tblmessages`
--

CREATE TABLE `tblmessages` (
  `Id` int(11) NOT NULL,
  `FromUserId` int(11) NOT NULL,
  `ToUserId` int(11) NOT NULL,
  `Content` longtext NOT NULL,
  `SendDateTime` datetime(6) NOT NULL,
  `IsRead` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblmessages`
--

INSERT INTO `tblmessages` (`Id`, `FromUserId`, `ToUserId`, `Content`, `SendDateTime`, `IsRead`) VALUES
(1, 1, 2, 'Testing sql 1', '2024-12-16 22:53:55.000000', 0),
(2, 2, 1, 'testing sql 2', '2024-12-16 22:53:55.000000', 0),
(3, 1, 2, 'halo', '2024-12-16 22:56:31.322097', 0),
(4, 1, 2, 'xd', '2024-12-17 00:58:48.021053', 0),
(5, 2, 1, 'roman boa', '2024-12-17 00:58:53.640316', 0),
(6, 2, 1, 'xd', '2024-12-17 00:58:56.261923', 0),
(7, 1, 2, 'halo 9iqan 9g8iufbwneu98ignwe9ugnw9g', '2024-12-17 00:59:03.723129', 0),
(8, 2, 1, '89ejnt09hje89thj89ejh98erjh98ejh98ejt98hj3e5-089hj3e-589hjm9e5hjmpoiemh', '2024-12-17 00:59:09.233866', 0),
(9, 1, 2, 'xd', '2024-12-25 14:44:31.005485', 0),
(10, 1, 2, 'ddd', '2024-12-25 14:49:44.615729', 0),
(11, 1, 3, 'test3', '2024-12-26 14:55:52.000000', 0),
(12, 1, 4, 'test4', '2024-12-25 14:55:52.000000', 0),
(13, 1, 5, 'test5', '2024-12-25 14:56:24.000000', 0),
(14, 1, 3, 'xd', '2024-12-25 15:00:03.204871', 0),
(15, 1, 3, 'ddd', '2024-12-25 15:00:19.317516', 0),
(16, 1, 5, 'xd', '2024-12-25 15:00:56.960793', 0),
(17, 1, 2, 'dd', '2024-12-25 15:01:14.807871', 0),
(18, 1, 4, 'testing 4444', '2024-12-25 15:01:28.324979', 0),
(19, 1, 4, 'funguje ?', '2024-12-25 15:09:34.150857', 0),
(20, 1, 5, 'uz to funguje', '2024-12-25 15:09:47.981442', 0),
(21, 1, 3, 'xd', '2024-12-25 15:16:28.641685', 0),
(22, 1, 5, '.LP.p', '2024-12-25 15:16:54.525515', 0),
(23, 2, 1, 'halo', '2024-12-25 15:25:59.267600', 0),
(24, 2, 1, 'xd', '2024-12-25 15:26:11.398533', 0),
(25, 2, 1, 'stefan', '2024-12-25 15:26:17.138072', 0),
(26, 2, 1, 'skusam zase', '2024-12-25 15:28:00.934143', 0),
(27, 2, 1, 'coe', '2024-12-25 15:28:16.371246', 0),
(28, 2, 1, 'bzum bzum', '2024-12-25 15:30:50.910507', 0),
(29, 2, 1, 'zas3', '2024-12-25 15:31:24.339425', 0),
(30, 2, 1, 'skusam', '2024-12-25 15:33:07.691587', 0),
(31, 2, 1, 'boa', '2024-12-25 15:33:47.174916', 0),
(32, 2, 1, 'sss', '2024-12-25 18:01:22.341847', 0),
(33, 2, 1, 'dddd', '2024-12-25 18:05:17.008250', 0),
(34, 2, 1, 'ggerge', '2024-12-25 18:05:38.527355', 0),
(35, 2, 1, 'ii', '2024-12-25 18:06:48.938930', 0),
(36, 2, 1, 'skibid', '2024-12-25 18:12:37.758454', 0),
(37, 2, 1, 'ddd', '2024-12-25 18:19:18.871328', 0),
(38, 1, 2, 'toilet', '2025-01-21 18:56:20.335281', 0),
(41, 1, 2, 'hal', '2025-01-31 23:42:25.528404', 0),
(42, 2, 1, 'xdd', '2025-01-31 23:44:01.487784', 0),
(43, 2, 1, 'ol', '2025-01-31 23:44:36.615179', 0),
(44, 2, 1, 'x', '2025-01-31 23:45:58.881338', 0),
(45, 2, 1, 'niue', '2025-01-31 23:46:22.324156', 0),
(46, 2, 1, 'dd', '2025-01-31 23:47:16.708751', 0),
(47, 2, 1, 'pls', '2025-01-31 23:48:33.051573', 0),
(48, 2, 1, 'ddd', '2025-01-31 23:56:13.823981', 0),
(49, 2, 1, 'bjnjh', '2025-01-31 23:56:50.245926', 0),
(50, 2, 1, 'fwe', '2025-01-31 23:58:12.632572', 0),
(51, 2, 1, 'ffff', '2025-02-01 00:00:03.059635', 0),
(52, 2, 1, 'xd', '2025-02-01 00:01:41.919368', 0),
(53, 2, 1, 'funguj', '2025-02-01 00:03:18.071000', 0),
(54, 2, 1, 'dadad', '2025-02-01 00:06:53.532460', 0),
(55, 2, 1, 'pip', '2025-02-01 00:10:26.220558', 0),
(56, 2, 1, 'yes', '2025-02-01 00:12:01.276354', 0),
(57, 2, 1, '6ytu', '2025-02-01 00:17:27.699798', 0),
(58, 1, 3, 'ikj', '2025-02-01 00:18:32.096602', 0),
(59, 2, 1, 'd', '2025-02-01 00:19:55.225254', 0),
(60, 2, 1, 'da', '2025-02-01 00:20:09.884167', 0),
(61, 2, 1, 'ere5tue5tuje5tuj', '2025-02-01 00:20:52.533542', 0),
(62, 2, 1, 'yo', '2025-02-01 09:49:02.054473', 0),
(63, 1, 13, 'xd', '2025-02-01 09:50:13.758528', 0),
(64, 13, 1, 'd', '2025-02-01 09:52:34.427593', 0),
(65, 13, 1, 'd', '2025-02-01 09:52:36.654516', 0),
(66, 13, 1, 'ad', '2025-02-01 09:52:37.879019', 0),
(67, 13, 1, 'ad', '2025-02-01 09:52:38.538683', 0),
(68, 13, 1, 'ad', '2025-02-01 09:52:39.239485', 0),
(69, 13, 1, 'ad', '2025-02-01 09:52:41.174616', 0),
(70, 13, 1, 'ad', '2025-02-01 09:52:41.931808', 0),
(71, 13, 1, 'ad', '2025-02-01 09:52:42.842794', 0),
(72, 1, 3, 'f', '2025-02-01 14:51:48.999179', 0),
(73, 1, 3, 'awd', '2025-02-01 14:51:50.156106', 0),
(74, 1, 3, 'eh', '2025-02-01 14:51:51.028204', 0),
(75, 1, 3, 'eht', '2025-02-01 14:51:51.807433', 0),
(76, 1, 3, 'ehertu', '2025-02-01 14:51:54.276732', 0),
(77, 1, 3, 'erjrtj', '2025-02-01 14:51:55.248717', 0),
(78, 1, 3, 'retjrtj', '2025-02-01 14:51:56.112207', 0),
(79, 2, 1, 'halo', '2025-02-01 15:00:01.620362', 0),
(80, 2, 1, 'u', '2025-02-01 15:00:50.451001', 0),
(81, 2, 1, 'j', '2025-02-01 15:01:44.444100', 0);

-- --------------------------------------------------------

--
-- Table structure for table `tblpendingfriendrequest`
--

CREATE TABLE `tblpendingfriendrequest` (
  `Id` int(11) NOT NULL,
  `FromUserId` int(11) NOT NULL,
  `ToUserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tblpendingpassword`
--

CREATE TABLE `tblpendingpassword` (
  `Id` int(11) NOT NULL,
  `Token` int(11) NOT NULL,
  `Email` longtext NOT NULL,
  `ValidUntil` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tblpendingusers`
--

CREATE TABLE `tblpendingusers` (
  `Id` int(11) NOT NULL,
  `UserName` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `StoredSalt` longblob NOT NULL,
  `VerificationCode` int(11) NOT NULL,
  `ValidUntil` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbluserfriends`
--

CREATE TABLE `tbluserfriends` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `FriendId` int(11) NOT NULL,
  `NickName` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbluserfriends`
--

INSERT INTO `tbluserfriends` (`Id`, `UserId`, `FriendId`, `NickName`) VALUES
(1, 1, 2, ''),
(2, 2, 1, ''),
(3, 1, 3, ''),
(4, 1, 4, ''),
(5, 1, 5, ''),
(6, 1, 13, ''),
(7, 13, 1, '');

-- --------------------------------------------------------

--
-- Table structure for table `tblusers`
--

CREATE TABLE `tblusers` (
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
-- Dumping data for table `tblusers`
--

INSERT INTO `tblusers` (`Id`, `UserName`, `Password`, `Email`, `StoredSalt`, `AvatarSourceName`, `Bio`, `IsOnline`, `LastLogonTime`, `CreationDate`) VALUES
(1, 'test1', 'LvkT4Bx3/suEdntu0MbO20bHdcKO/tZ3jV/jDtCvrHI=', 'test1gmail.com', 0x88a2db71906e7c50fb4570104ec44625, 'defaulticon.png', '', 0, '2025-01-29 11:35:24.495349', '2025-01-29 11:35:24.495430'),
(2, 'test2', 'wy8zm0YQYIJ9h03ImUwMX9b3O1VQkzF9wERydL2rYmc=', 'test2gmail.com', 0x6c14aec4a64aa7043d0c114b6e5a0288, 'defaulticon2.png', '', 0, '2025-01-29 11:35:24.495349', '2025-01-29 11:35:24.495430'),
(3, 'test3', 'J7rSXOQXkjrpLkfsJP11pQUJazYsvzrakk/TgnSU7hM=', 'test3gmail.com', 0xce93e11cde62253b1e5b445cd471d0b7, 'defaulticon3.png', '', 0, '2025-01-29 11:35:24.495349', '2025-01-29 11:35:24.495430'),
(4, 'test4', '9v3JaXOk7eVP508yzwVduDivMVsB07u76vkI+8rkXcY=', 'test4gmail.com', 0x7b4e890e322f95c1e1537c877ef70794, 'defaulticon4.png', '', 0, '2025-01-29 11:35:24.495349', '2025-01-29 11:35:24.495430'),
(5, 'test5', 'GbenJE02nvEBnVbE1akVzxl7ltKTZSlIYjne9ftRKEk=', 'test5gmail.com', 0x2952c786aed4bee31d7e63e4c978e42d, 'defaulticon5.png', '', 0, '2025-01-29 11:35:24.495349', '2025-01-29 11:35:24.495430'),
(12, 'crazy', '2fB4OB50hmGQFNT2YLUpVAaNQd3f1PUed0s71UXkIKk=', 'craziikfor@gmail.com', 0x276a5ad41a61f34833b39117be3a5553, 'defaulticon.png', '', 0, '2025-01-29 11:35:24.495349', '2025-01-29 11:35:24.495430'),
(13, 'intro', 'qhzMsJcWtVEE1o4cfBHuAjnNMpUwDDDSKN95i+Y0jXQ=', 'introforuse11@gmail.com', 0x65702354c77d5e561c52e5230a586102, 'defaulticon.png', '', 0, '2025-01-29 11:35:24.495349', '2025-01-29 11:35:24.495430');

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
('20250131220727_test', '8.0.11');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tblmessages`
--
ALTER TABLE `tblmessages`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tblpendingfriendrequest`
--
ALTER TABLE `tblpendingfriendrequest`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tblpendingpassword`
--
ALTER TABLE `tblpendingpassword`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tblpendingusers`
--
ALTER TABLE `tblpendingusers`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tbluserfriends`
--
ALTER TABLE `tbluserfriends`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tblusers`
--
ALTER TABLE `tblusers`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tblmessages`
--
ALTER TABLE `tblmessages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=82;

--
-- AUTO_INCREMENT for table `tblpendingfriendrequest`
--
ALTER TABLE `tblpendingfriendrequest`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tblpendingpassword`
--
ALTER TABLE `tblpendingpassword`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tblpendingusers`
--
ALTER TABLE `tblpendingusers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tbluserfriends`
--
ALTER TABLE `tbluserfriends`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `tblusers`
--
ALTER TABLE `tblusers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
