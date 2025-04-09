-- MySQL dump 10.13  Distrib 8.0.37, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: recipe_nest
-- ------------------------------------------------------
-- Server version	8.0.37

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
-- Table structure for table `cuisines`
--
DROP DATABASE recipe_nest;
CREATE DATABASE recipe_nest;
USE recipe_nest;

DROP TABLE IF EXISTS `cuisines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cuisines` (
                            `id` int NOT NULL AUTO_INCREMENT,
                            `name` varchar(100) NOT NULL,
                            `image_url` varchar(255) DEFAULT NULL,
                            `is_active` tinyint(1) NOT NULL DEFAULT '1',
                            `created_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                            `updated_date` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
                            PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=135047 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cuisines`
--

LOCK TABLES `cuisines` WRITE;
/*!40000 ALTER TABLE `cuisines` DISABLE KEYS */;
INSERT INTO `cuisines` VALUES (1,'Nepali','aaa',1,'2025-03-14 11:56:13','2025-04-03 06:59:42'),(2,'aaaaaaaaaaaa','aaa',0,'2025-04-03 06:58:25','2025-04-03 07:00:17'),(3,'NKhaja','new',1,'2025-04-03 10:19:55','2025-04-05 08:11:48'),(4,'add','asd',1,'2025-04-04 06:52:39',NULL),(5,'NK','asd',1,'2025-04-05 08:11:25',NULL),(6,'NK','asd',1,'2025-04-05 08:11:25',NULL),(7,'NK','asd',1,'2025-04-05 08:11:25',NULL),(8,'NK','asd',1,'2025-04-05 08:11:25',NULL),(9,'NK','asd',1,'2025-04-05 08:11:25',NULL),(10,'NK','asd',1,'2025-04-05 08:11:25',NULL);
/*!40000 ALTER TABLE `cuisines` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `favorites`
--

DROP TABLE IF EXISTS `favorites`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `favorites` (
                             `id` int NOT NULL AUTO_INCREMENT,
                             `recipe_id` int DEFAULT NULL,
                             `user_id` int DEFAULT NULL,
                             `is_active` tinyint(1) NOT NULL DEFAULT '1',
                             `created_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                             `updated_date` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
                             PRIMARY KEY (`id`),
                             KEY `fk_favorites_users_id` (`user_id`),
                             KEY `fk_favorites_recipes_id` (`recipe_id`),
                             CONSTRAINT `fk_favorites_recipes_id` FOREIGN KEY (`recipe_id`) REFERENCES `recipes` (`id`),
                             CONSTRAINT `fk_favorites_users_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `favorites`
--

LOCK TABLES `favorites` WRITE;
/*!40000 ALTER TABLE `favorites` DISABLE KEYS */;
/*!40000 ALTER TABLE `favorites` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ratings`
--

DROP TABLE IF EXISTS `ratings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ratings` (
                           `id` int NOT NULL AUTO_INCREMENT,
                           `rating` enum('0','1','2','3','4','5','6','7','8','9','10') DEFAULT '0',
                           `recipe_id` int DEFAULT NULL,
                           `user_id` int DEFAULT NULL,
                           `is_active` tinyint(1) NOT NULL DEFAULT '1',
                           `created_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                           `updated_date` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
                           PRIMARY KEY (`id`),
                           KEY `fk_ratings_users_id` (`user_id`),
                           KEY `fk_ratings_recipes_id` (`recipe_id`),
                           CONSTRAINT `fk_ratings_recipes_id` FOREIGN KEY (`recipe_id`) REFERENCES `recipes` (`id`),
                           CONSTRAINT `fk_ratings_users_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ratings`
--

LOCK TABLES `ratings` WRITE;
/*!40000 ALTER TABLE `ratings` DISABLE KEYS */;
/*!40000 ALTER TABLE `ratings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recipes`
--

DROP TABLE IF EXISTS `recipes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipes` (
                           `id` int NOT NULL AUTO_INCREMENT,
                           `image_url` varchar(255) DEFAULT NULL,
                           `title` varchar(100) NOT NULL,
                           `description` text,
                           `recipe` text NOT NULL,
                           `ingredients` text NOT NULL,
                           `is_active` tinyint(1) NOT NULL DEFAULT '1',
                           `recipe_by` int DEFAULT NULL,
                           `cuisine` int DEFAULT NULL,
                           `created_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                           `updated_date` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
                           PRIMARY KEY (`id`),
                           KEY `fk_recipes_cuisine_id` (`cuisine`),
                           KEY `fk_recipes_users_id` (`recipe_by`),
                           CONSTRAINT `fk_recipes_cuisine_id` FOREIGN KEY (`cuisine`) REFERENCES `cuisines` (`id`),
                           CONSTRAINT `fk_recipes_users_id` FOREIGN KEY (`recipe_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recipes`
--

LOCK TABLES `recipes` WRITE;
/*!40000 ALTER TABLE `recipes` DISABLE KEYS */;
INSERT INTO `recipes` VALUES (1,'http://example.com/images/updated_recipe.jpg','aaaaaaaaaaaaa Delicious Test Recipe','An updated description for the test recipe.','Step 1: Preheat oven. Step 2: Combine ingredients gently. Step 3: Bake for 30 mins.','Ingredient A (modified), Ingredient B, New Ingredient D',1,1,3,'2025-03-14 11:56:22','2025-04-03 15:41:03'),(2,'http://example.com/images/updated_recipe.jpg','aaaaaaaaaaaaa ','An updated description for the test recipe.','Step 1: Preheat oven. Step 2: Combine ingredients gently. Step 3: Bake for 30 mins.','Ingredient A (modified), Ingredient B, New Ingredient D',1,1,3,'2025-04-03 15:17:11','2025-04-03 15:41:20'),(3,'http://example.com/images/your_recipe.jpg','Delicious Test Recipe','A simple recipe created for testing purposes.','Step 1: Gather ingredients. Step 2: Mix them. Step 3: Cook.','Ingredient A, Ingredient B, Ingredient C',0,1,3,'2025-04-03 15:40:33','2025-04-03 15:42:28'),(4,'http://example.com/images/updated_recipe.jpg','rec ','An updated description for the test recipe.','Step 1: Preheat oven. Step 2: Combine ingredients gently. Step 3: Bake for 30 mins.','Ingredient A (modified), Ingredient B, New Ingredient D',0,1,3,'2025-04-05 08:12:55','2025-04-05 08:14:49');
/*!40000 ALTER TABLE `recipes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
                         `id` int NOT NULL AUTO_INCREMENT,
                         `name` varchar(100) NOT NULL,
                         `created_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                         `updated_date` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
                         `is_active` tinyint(1) NOT NULL DEFAULT '1',
                         PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'ADMIN','2025-03-14 11:54:43',NULL,1),(2,'CHEF','2025-03-14 11:54:43',NULL,1),(3,'FOOD_LOVER','2025-03-14 11:54:43',NULL,1);
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
                         `id` int NOT NULL AUTO_INCREMENT,
                         `first_name` varchar(100) NOT NULL,
                         `last_name` varchar(100) NOT NULL,
                         `phone_number` varchar(15) NOT NULL,
                         `image_url` varchar(255) DEFAULT 'https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x',
                         `about` text,
                         `email` varchar(100) NOT NULL,
                         `password` varchar(255) NOT NULL,
                         `role_id` int NOT NULL,
                         `created_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                         `updated_date` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
                         `is_active` tinyint(1) NOT NULL DEFAULT '1',
                         PRIMARY KEY (`id`),
                         UNIQUE KEY `email` (`email`),
                         KEY `fk_users_role_id` (`role_id`),
                         CONSTRAINT `fk_users_role_id` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Ram','Karki','9808546858','https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x','Software developer testing the API.','ram@mail.cocm','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',3,'2025-03-14 11:54:54','2025-04-09 08:19:28',1),(2,'Shriyan','Hero','1234567890','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','hregreg.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-03-14 11:54:54','2025-04-09 08:19:28',1),(3,'Biryan','Sarki','9808546859','https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x','Software developer testing the API.','ra1b@mail.cocm','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',3,'2025-03-14 11:54:54','2025-04-09 08:19:28',1),(4,'Ream','Sarki','9808546859','https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x','Software developer testing the API.','ra2b@mail.cocm','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',3,'2025-03-14 11:54:54','2025-04-09 08:19:28',0),(5,'Driyam','Sarki','9808546859','https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x','Software developer testing the API.','ra3b@mail.cocm','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',3,'2025-03-14 11:54:54','2025-04-09 08:19:28',0),(6,'Raman','Sarki','9808546859','https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x','Software developer testing the API.','ra4b@mail.cocm','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',3,'2025-03-14 11:54:54','2025-04-09 08:19:28',1),(7,'Pritam','Sarki','9808546859','https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x','Software developer testing the API.','ra5b@mail.cocm','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',3,'2025-03-14 11:54:54','2025-04-09 08:19:28',1),(8,'Chritam','Sarki','9808546859','https://gravatar.com/avatar/3ddc49ac922e89726a2ceb70b3d45564?s=400&d=robohash&r=x','Software developer testing the API.','ra6b@mail.cocm','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',3,'2025-03-14 11:54:54','2025-04-09 08:19:28',1),(9,'ma user','shakya','1234567890','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','sayub.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-01 18:56:59','2025-04-09 08:19:28',0),(11,'bro','shakya','1234567890','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','bro.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-01 19:08:48','2025-04-09 08:19:28',1),(12,'xiiiuuuuuuuuuuuuuuuuuuuu','shakya','1234567890','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','xiiiuuuuuuuuuuuuuuuuuuuu.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-02 04:02:09','2025-04-09 08:19:28',0),(13,'bb','shakya','1234567890','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','bb.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-02 04:18:02','2025-04-09 08:19:28',0),(14,'updated','shakya','1234567890','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','hora.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-02 04:44:10','2025-04-09 08:19:28',1),(16,'aaaa','aaaa','9898878766','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','aaaa.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-02 11:18:00','2025-04-09 08:19:28',1),(17,'bbbbb','bbbbb','9898878766','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','bbbbb.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-02 12:24:39','2025-04-09 08:19:28',1),(18,'new','new','9898878766','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','new.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-05 08:09:50','2025-04-09 08:19:28',1),(19,'register ','user','9898878766','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','register.shakya@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-07 08:20:37','2025-04-09 08:19:28',1),(21,'bbbbb','bbbbb','9898878766','https://plus.unsplash.com/premium_photo-1689530775582-83b8abdb5020?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww','Software developer testing the API.','bbbbb.shakya1@example.com','$2a$11$HgnPTgDX8CVXPwKSDBaEb.vKqZWC9rbdhc3n27oIxIi4WxbyFfY4S',2,'2025-04-09 07:37:38','2025-04-09 08:19:28',1);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-09 22:28:13
