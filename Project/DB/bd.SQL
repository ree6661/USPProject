-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: localhost    Database: phonebd
-- ------------------------------------------------------
-- Server version	8.0.28

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `галерия`
--

DROP TABLE IF EXISTS `галерия`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `галерия` (
  `idГалерия` int unsigned NOT NULL AUTO_INCREMENT,
  `idМодели` int unsigned NOT NULL,
  `idСнимка` int unsigned NOT NULL,
  PRIMARY KEY (`idГалерия`),
  KEY `idМодели_idx` (`idМодели`),
  KEY `idСнимка_idx` (`idСнимка`),
  CONSTRAINT `idМодели` FOREIGN KEY (`idМодели`) REFERENCES `модели` (`idМодели`),
  CONSTRAINT `idСнимка` FOREIGN KEY (`idСнимка`) REFERENCES `снимка` (`idСнимка`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `галерия`
--

LOCK TABLES `галерия` WRITE;
/*!40000 ALTER TABLE `галерия` DISABLE KEYS */;
/*!40000 ALTER TABLE `галерия` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `камери`
--

DROP TABLE IF EXISTS `камери`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `камери` (
  `idКамери` int unsigned NOT NULL AUTO_INCREMENT,
  `idМодели` int unsigned NOT NULL,
  `Предна камера` tinyint unsigned NOT NULL,
  `idМегапиксели` int unsigned NOT NULL,
  PRIMARY KEY (`idКамери`),
  KEY `idМодели_idx` (`idМодели`),
  KEY `idМегапиксели_idx` (`idМегапиксели`),
  CONSTRAINT `idМегапиксели` FOREIGN KEY (`idМегапиксели`) REFERENCES `мегапиксели` (`idМегапиксели`),
  CONSTRAINT `idМоделии` FOREIGN KEY (`idМодели`) REFERENCES `модели` (`idМодели`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `камери`
--

LOCK TABLES `камери` WRITE;
/*!40000 ALTER TABLE `камери` DISABLE KEYS */;
INSERT INTO `камери` VALUES (1,1,0,1),(2,2,1,2),(3,3,0,1),(4,1,1,3),(5,4,1,3),(6,2,0,3);
/*!40000 ALTER TABLE `камери` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `марки`
--

DROP TABLE IF EXISTS `марки`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `марки` (
  `idМарки` int unsigned NOT NULL AUTO_INCREMENT,
  `Марки` varchar(45) NOT NULL,
  PRIMARY KEY (`idМарки`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `марки`
--

LOCK TABLES `марки` WRITE;
/*!40000 ALTER TABLE `марки` DISABLE KEYS */;
INSERT INTO `марки` VALUES (6,'Xiomi'),(7,'Lenovo'),(8,'Samsung');
/*!40000 ALTER TABLE `марки` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `мегапиксели`
--

DROP TABLE IF EXISTS `мегапиксели`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `мегапиксели` (
  `idМегапиксели` int unsigned NOT NULL AUTO_INCREMENT,
  `Мегапиксели` varchar(10) NOT NULL,
  PRIMARY KEY (`idМегапиксели`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `мегапиксели`
--

LOCK TABLES `мегапиксели` WRITE;
/*!40000 ALTER TABLE `мегапиксели` DISABLE KEYS */;
INSERT INTO `мегапиксели` VALUES (1,'16Mp'),(2,'32Mp'),(3,'64Mp');
/*!40000 ALTER TABLE `мегапиксели` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `модели`
--

DROP TABLE IF EXISTS `модели`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `модели` (
  `idМодели` int unsigned NOT NULL AUTO_INCREMENT,
  `idМарки` int unsigned NOT NULL,
  `idПроцесор` int unsigned NOT NULL,
  `idОС` int unsigned NOT NULL,
  `idОперативна памет` int unsigned NOT NULL,
  `idСторидж` int unsigned NOT NULL,
  `Модел` varchar(45) NOT NULL,
  `Описание` longtext,
  PRIMARY KEY (`idМодели`),
  KEY `idМарка_idx` (`idМарки`),
  KEY `idПроцесор_idx` (`idПроцесор`),
  KEY `idОперативна памет _idx` (`idОперативна памет`),
  KEY `idСторидж_idx` (`idСторидж`),
  CONSTRAINT `idМарка` FOREIGN KEY (`idМарки`) REFERENCES `марки` (`idМарки`),
  CONSTRAINT `idОперативна памет ` FOREIGN KEY (`idОперативна памет`) REFERENCES `оперативна памет` (`idОперативна памет`),
  CONSTRAINT `idПроцесор` FOREIGN KEY (`idПроцесор`) REFERENCES `процесор` (`idПроцесор`),
  CONSTRAINT `idСторидж` FOREIGN KEY (`idСторидж`) REFERENCES `сторидж` (`idСторидж`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `модели`
--

LOCK TABLES `модели` WRITE;
/*!40000 ALTER TABLE `модели` DISABLE KEYS */;
INSERT INTO `модели` VALUES (1,8,4,1,3,2,'Galaxy S21',''),(2,7,3,2,2,3,'Legion',NULL),(3,6,3,1,2,3,'Redmi 9',NULL),(4,6,4,1,1,2,'Redmi 10',NULL);
/*!40000 ALTER TABLE `модели` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `оперативна памет`
--

DROP TABLE IF EXISTS `оперативна памет`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `оперативна памет` (
  `idОперативна памет` int unsigned NOT NULL AUTO_INCREMENT,
  `Оперативна памет` varchar(45) NOT NULL,
  PRIMARY KEY (`idОперативна памет`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `оперативна памет`
--

LOCK TABLES `оперативна памет` WRITE;
/*!40000 ALTER TABLE `оперативна памет` DISABLE KEYS */;
INSERT INTO `оперативна памет` VALUES (1,'2Gb'),(2,'4Gb'),(3,'8Gb');
/*!40000 ALTER TABLE `оперативна памет` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ос`
--

DROP TABLE IF EXISTS `ос`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ос` (
  `idОС` int unsigned NOT NULL AUTO_INCREMENT,
  `ОС` varchar(45) NOT NULL,
  PRIMARY KEY (`idОС`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ос`
--

LOCK TABLES `ос` WRITE;
/*!40000 ALTER TABLE `ос` DISABLE KEYS */;
INSERT INTO `ос` VALUES (1,'Android'),(2,'IOS');
/*!40000 ALTER TABLE `ос` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `процесор`
--

DROP TABLE IF EXISTS `процесор`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `процесор` (
  `idПроцесор` int unsigned NOT NULL AUTO_INCREMENT,
  `Процесор` varchar(45) NOT NULL,
  PRIMARY KEY (`idПроцесор`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `процесор`
--

LOCK TABLES `процесор` WRITE;
/*!40000 ALTER TABLE `процесор` DISABLE KEYS */;
INSERT INTO `процесор` VALUES (3,'A 15 Bionic Apple'),(4,'Dimensity 9000 Media Tek'),(5,'A14 Bionic');
/*!40000 ALTER TABLE `процесор` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `снимка`
--

DROP TABLE IF EXISTS `снимка`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `снимка` (
  `idСнимка` int unsigned NOT NULL AUTO_INCREMENT,
  `Път` varchar(50) NOT NULL,
  PRIMARY KEY (`idСнимка`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `снимка`
--

LOCK TABLES `снимка` WRITE;
/*!40000 ALTER TABLE `снимка` DISABLE KEYS */;
/*!40000 ALTER TABLE `снимка` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `сторидж`
--

DROP TABLE IF EXISTS `сторидж`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `сторидж` (
  `idСторидж` int unsigned NOT NULL AUTO_INCREMENT,
  `Размер` varchar(10) NOT NULL,
  PRIMARY KEY (`idСторидж`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `сторидж`
--

LOCK TABLES `сторидж` WRITE;
/*!40000 ALTER TABLE `сторидж` DISABLE KEYS */;
INSERT INTO `сторидж` VALUES (1,'32Gb'),(2,'64Gb'),(3,'128Gb');
/*!40000 ALTER TABLE `сторидж` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-05-22 18:59:24
