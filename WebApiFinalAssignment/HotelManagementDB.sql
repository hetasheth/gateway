USE [master]
GO

/****** Object:  Database [HotelManageSystemDB]    Script Date: 2021-01-14 18:35:04 ******/
CREATE DATABASE [HotelManageSystemDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HotelManageSystemDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\HotelManageSystemDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HotelManageSystemDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\HotelManageSystemDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [HotelManageSystemDB] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HotelManageSystemDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [HotelManageSystemDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [HotelManageSystemDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [HotelManageSystemDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [HotelManageSystemDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [HotelManageSystemDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [HotelManageSystemDB] SET  MULTI_USER 
GO

ALTER DATABASE [HotelManageSystemDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [HotelManageSystemDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [HotelManageSystemDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [HotelManageSystemDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [HotelManageSystemDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [HotelManageSystemDB] SET  READ_WRITE 
GO

