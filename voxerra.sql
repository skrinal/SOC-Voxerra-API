-- MySQL dump 10.13  Distrib 8.0.40, for Linux (x86_64)
--
-- Host: localhost    Database: Voxerra
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Tblmessages`
--

DROP TABLE IF EXISTS `Tblmessages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tblmessages` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FromUserId` int NOT NULL,
  `ToUserId` int NOT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SendDateTime` datetime(6) NOT NULL,
  `IsRead` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Tblmessages_FromUserId` (`FromUserId`),
  KEY `IX_Tblmessages_ToUserId` (`ToUserId`),
  CONSTRAINT `FK_Tblmessages_Tblusers_FromUserId` FOREIGN KEY (`FromUserId`) REFERENCES `Tblusers` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Tblmessages_Tblusers_ToUserId` FOREIGN KEY (`ToUserId`) REFERENCES `Tblusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tblmessages`
--

LOCK TABLES `Tblmessages` WRITE;
/*!40000 ALTER TABLE `Tblmessages` DISABLE KEYS */;
/*!40000 ALTER TABLE `Tblmessages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tblpendingfriendrequest`
--

DROP TABLE IF EXISTS `Tblpendingfriendrequest`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tblpendingfriendrequest` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FromUserId` int NOT NULL,
  `ToUserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Tblpendingfriendrequest_FromUserId` (`FromUserId`),
  KEY `IX_Tblpendingfriendrequest_ToUserId` (`ToUserId`),
  CONSTRAINT `FK_Tblpendingfriendrequest_Tblusers_FromUserId` FOREIGN KEY (`FromUserId`) REFERENCES `Tblusers` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Tblpendingfriendrequest_Tblusers_ToUserId` FOREIGN KEY (`ToUserId`) REFERENCES `Tblusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tblpendingfriendrequest`
--

LOCK TABLES `Tblpendingfriendrequest` WRITE;
/*!40000 ALTER TABLE `Tblpendingfriendrequest` DISABLE KEYS */;
INSERT INTO `Tblpendingfriendrequest` VALUES (6,1,3);
/*!40000 ALTER TABLE `Tblpendingfriendrequest` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tblpendingpassword`
--

DROP TABLE IF EXISTS `Tblpendingpassword`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tblpendingpassword` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` int NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ExpireTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tblpendingpassword`
--

LOCK TABLES `Tblpendingpassword` WRITE;
/*!40000 ALTER TABLE `Tblpendingpassword` DISABLE KEYS */;
INSERT INTO `Tblpendingpassword` VALUES (1,23480,'richard.kamenistak@gmail.com','2025-03-28 21:48:25.676602'),(2,50071,'richard.kamenistak@gmail.com','2025-03-28 21:50:42.604982');
/*!40000 ALTER TABLE `Tblpendingpassword` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tblpendingusers`
--

DROP TABLE IF EXISTS `Tblpendingusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tblpendingusers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `StoredSalt` longblob NOT NULL,
  `VerificationCode` int NOT NULL,
  `ExpireTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tblpendingusers`
--

LOCK TABLES `Tblpendingusers` WRITE;
/*!40000 ALTER TABLE `Tblpendingusers` DISABLE KEYS */;
INSERT INTO `Tblpendingusers` VALUES (2,'dad','ris@gmail.com','FMd3Y85crOKk6Qu2L+BQ4SQakQYJ0hZ1owZJq5iviOQ=',_binary 'gŸ;\áŠ\ÅO1`ceµx\örœ_1o0ß‹£mO¼\ö',64473,'2025-04-06 14:28:53.558654');
/*!40000 ALTER TABLE `Tblpendingusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tbltwofactorauth`
--

DROP TABLE IF EXISTS `Tbltwofactorauth`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tbltwofactorauth` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Code` int NOT NULL,
  `ExpireTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Tbltwofactorauth_UserId` (`UserId`),
  CONSTRAINT `FK_Tbltwofactorauth_Tblusers_UserId` FOREIGN KEY (`UserId`) REFERENCES `Tblusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tbltwofactorauth`
--

LOCK TABLES `Tbltwofactorauth` WRITE;
/*!40000 ALTER TABLE `Tbltwofactorauth` DISABLE KEYS */;
/*!40000 ALTER TABLE `Tbltwofactorauth` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tbluserfriends`
--

DROP TABLE IF EXISTS `Tbluserfriends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tbluserfriends` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `FriendId` int NOT NULL,
  `NickName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Tbluserfriends_FriendId` (`FriendId`),
  KEY `IX_Tbluserfriends_UserId` (`UserId`),
  CONSTRAINT `FK_Tbluserfriends_Tblusers_FriendId` FOREIGN KEY (`FriendId`) REFERENCES `Tblusers` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Tbluserfriends_Tblusers_UserId` FOREIGN KEY (`UserId`) REFERENCES `Tblusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tbluserfriends`
--

LOCK TABLES `Tbluserfriends` WRITE;
/*!40000 ALTER TABLE `Tbluserfriends` DISABLE KEYS */;
/*!40000 ALTER TABLE `Tbluserfriends` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tblusers`
--

DROP TABLE IF EXISTS `Tblusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tblusers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `StoredSalt` longblob NOT NULL,
  `AvatarSourceName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AvatarVersion` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Bio` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsOnline` tinyint(1) NOT NULL,
  `LastLogonTime` datetime(6) NOT NULL,
  `CreationDate` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tblusers`
--

LOCK TABLES `Tblusers` WRITE;
/*!40000 ALTER TABLE `Tblusers` DISABLE KEYS */;
INSERT INTO `Tblusers` VALUES (1,'skrinal','vNMurCf3LgCJy81nMVNkJW4yawVorbokyohySNJBAFI=','richard.kamenistak@gmail.com',_binary '8¾¤Tm-Rf.$~<7\Û>LÁY7 \é¿€Üˆ\ÛS','defaulticon.png','','',0,'2025-03-28 21:20:52.421830','2025-03-28 21:20:52.421845'),(3,'test','sDiFAwHNX/0w3xURqeDFFrESQpERqnYu+f3KWz8gARk=','skrinalghost@gmail.com',_binary 'rŠ\ï\ê\\>r\Ë×¤OÎŸi\'§*ºl&\æ|KšqEú@','defaulticon.png','','',0,'2025-04-07 08:23:04.364990','2025-04-07 08:23:04.364990');
/*!40000 ALTER TABLE `Tblusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tblusersettings`
--

DROP TABLE IF EXISTS `Tblusersettings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tblusersettings` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LoginAlertsEnabled` tinyint(1) NOT NULL,
  `WhereIsUserLoggedIn` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Tblusersettings_UserId` (`UserId`),
  CONSTRAINT `FK_Tblusersettings_Tblusers_UserId` FOREIGN KEY (`UserId`) REFERENCES `Tblusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tblusersettings`
--

LOCK TABLES `Tblusersettings` WRITE;
/*!40000 ALTER TABLE `Tblusersettings` DISABLE KEYS */;
INSERT INTO `Tblusersettings` VALUES (1,1,0,0,''),(3,3,0,0,'');
/*!40000 ALTER TABLE `Tblusersettings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20250328205654_xd','8.0.13');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-08 10:40:18
