USE [master]
GO
/****** Object:  Database [DataFillingDb]    Script Date: 10/31/2021 12:15:56 AM ******/
CREATE DATABASE [DataFillingDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataFillingDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.LOCAL\MSSQL\DATA\DataFillingDb.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DataFillingDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.LOCAL\MSSQL\DATA\DataFillingDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataFillingDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataFillingDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataFillingDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataFillingDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataFillingDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataFillingDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataFillingDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DataFillingDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataFillingDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataFillingDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataFillingDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataFillingDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataFillingDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataFillingDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataFillingDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataFillingDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DataFillingDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataFillingDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataFillingDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataFillingDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataFillingDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataFillingDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataFillingDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataFillingDb] SET RECOVERY FULL 
GO
ALTER DATABASE [DataFillingDb] SET  MULTI_USER 
GO
ALTER DATABASE [DataFillingDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataFillingDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataFillingDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataFillingDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DataFillingDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DataFillingDb', N'ON'
GO
USE [DataFillingDb]
GO
/****** Object:  Table [dbo].[FormData]    Script Date: 10/31/2021 12:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormData](
	[FormSerial] [int] IDENTITY(1,1) NOT NULL,
	[FormNo] [nvarchar](50) NULL,
	[CompanyCode] [nvarchar](200) NULL,
	[CompanyName] [nvarchar](1500) NULL,
	[CompanyAddress] [nvarchar](max) NULL,
	[ZipCode] [nvarchar](100) NULL,
	[Fax] [nvarchar](250) NULL,
	[Website] [nvarchar](max) NULL,
	[Email] [nvarchar](2000) NULL,
	[ContactNo] [nvarchar](15) NULL,
	[State] [nvarchar](600) NULL,
	[Country] [nvarchar](500) NULL,
	[Headquarter] [nvarchar](max) NULL,
	[NoOfEmployees] [int] NULL,
	[Industry] [nvarchar](max) NULL,
	[BrandAmbassador] [nvarchar](max) NULL,
	[MediaPartner] [nvarchar](max) NULL,
	[SocialMedia] [nvarchar](max) NULL,
	[FrenchiesPartner] [nvarchar](max) NULL,
	[Investor] [nvarchar](max) NULL,
	[AdvertisingPartner] [nvarchar](max) NULL,
	[Product] [nvarchar](max) NULL,
	[Services] [nvarchar](max) NULL,
	[Manager] [nvarchar](max) NULL,
	[RegistrationDate] [nvarchar](max) NULL,
	[YearlyRevenue] [nvarchar](max) NULL,
	[Subclassification] [nvarchar](max) NULL,
	[Landmark] [nvarchar](max) NULL,
	[AccoutAudit] [nvarchar](max) NULL,
	[Currency] [nvarchar](max) NULL,
	[YearlyExpense] [nvarchar](max) NULL,
	[FileName] [nvarchar](1000) NULL,
	[AuthenticationKey] [nvarchar](max) NULL,
	[EntryTime] [nvarchar](max) NULL,
 CONSTRAINT [PK_FormData] PRIMARY KEY CLUSTERED 
(
	[FormSerial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/31/2021 12:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](1000) NOT NULL,
	[LastName] [nvarchar](1000) NOT NULL,
	[Email] [nvarchar](1000) NOT NULL,
	[MobileNo] [nvarchar](1000) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Gender] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[FormNo] [int] NULL,
	[UserName] [nvarchar](250) NULL,
	[DesktopPassword] [nvarchar](500) NULL,
	[MacAddress] [nvarchar](100) NULL,
	[AuthenticationKey] [nvarchar](50) NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[UserStatus] [nvarchar](1) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [DataFillingDb] SET  READ_WRITE 
GO
