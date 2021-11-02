USE [master]
GO
/****** Object:  Database [DataFillingDb]    Script Date: 11/2/2021 12:18:17 PM ******/
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
/****** Object:  Table [dbo].[FormData]    Script Date: 11/2/2021 12:18:18 PM ******/
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
	[NoOfEmployees] [nvarchar](50) NULL,
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
/****** Object:  Table [dbo].[Users]    Script Date: 11/2/2021 12:18:18 PM ******/
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
SET IDENTITY_INSERT [dbo].[FormData] ON 

INSERT [dbo].[FormData] ([FormSerial], [FormNo], [CompanyCode], [CompanyName], [CompanyAddress], [ZipCode], [Fax], [Website], [Email], [ContactNo], [State], [Country], [Headquarter], [NoOfEmployees], [Industry], [BrandAmbassador], [MediaPartner], [SocialMedia], [FrenchiesPartner], [Investor], [AdvertisingPartner], [Product], [Services], [Manager], [RegistrationDate], [YearlyRevenue], [Subclassification], [Landmark], [AccoutAudit], [Currency], [YearlyExpense], [FileName], [AuthenticationKey], [EntryTime]) VALUES (6, N'544644444', N'HMM5544h', N'Shipping Corporation', N'Mumbai', N'400021', N'-', N'info@shipindia.com', N'info@shipindia.com', N'-', N'Mumbai', N'India', N'Mumbai', N'0', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Demo1..JPG', N'SXqZAj0v', N'30/10/2021_08:58:08')
INSERT [dbo].[FormData] ([FormSerial], [FormNo], [CompanyCode], [CompanyName], [CompanyAddress], [ZipCode], [Fax], [Website], [Email], [ContactNo], [State], [Country], [Headquarter], [NoOfEmployees], [Industry], [BrandAmbassador], [MediaPartner], [SocialMedia], [FrenchiesPartner], [Investor], [AdvertisingPartner], [Product], [Services], [Manager], [RegistrationDate], [YearlyRevenue], [Subclassification], [Landmark], [AccoutAudit], [Currency], [YearlyExpense], [FileName], [AuthenticationKey], [EntryTime]) VALUES (7, N'56k8545568', N'HMM34343', N'HMM', N'Mumbai', N'09210', N'-', N'-', N'info@shipindia.com', N'-', N'Mumbai', N'', N'', N'0', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Demo2..JPG', N'SXqZAj0v', N'30/10/2021_09:00:20')
INSERT [dbo].[FormData] ([FormSerial], [FormNo], [CompanyCode], [CompanyName], [CompanyAddress], [ZipCode], [Fax], [Website], [Email], [ContactNo], [State], [Country], [Headquarter], [NoOfEmployees], [Industry], [BrandAmbassador], [MediaPartner], [SocialMedia], [FrenchiesPartner], [Investor], [AdvertisingPartner], [Product], [Services], [Manager], [RegistrationDate], [YearlyRevenue], [Subclassification], [Landmark], [AccoutAudit], [Currency], [YearlyExpense], [FileName], [AuthenticationKey], [EntryTime]) VALUES (8, N'398483904203', N'Hmm129109', N'sasjkajskjk', N'uiuqwiuquwiq', N'1929120', N'-', N'-', N'shf@gmail.com', N'-', N'-', N'', N'', N'0', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Demo2..JPG', N'SXqZAj0v', N'31/10/2021_06:56:00')
INSERT [dbo].[FormData] ([FormSerial], [FormNo], [CompanyCode], [CompanyName], [CompanyAddress], [ZipCode], [Fax], [Website], [Email], [ContactNo], [State], [Country], [Headquarter], [NoOfEmployees], [Industry], [BrandAmbassador], [MediaPartner], [SocialMedia], [FrenchiesPartner], [Investor], [AdvertisingPartner], [Product], [Services], [Manager], [RegistrationDate], [YearlyRevenue], [Subclassification], [Landmark], [AccoutAudit], [Currency], [YearlyExpense], [FileName], [AuthenticationKey], [EntryTime]) VALUES (9, N'45548787', N'Hmm47545', N'HMM', N'sashag', N'454578', N'-', N'-', N'54asa54sa5s4', N'4545787454', N'sagsajh', N'', N'', N'0', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Demo1..JPG', N'SXqZAj0v', N'31/10/2021_08:56:40')
SET IDENTITY_INSERT [dbo].[FormData] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [Email], [MobileNo], [Password], [Address], [Gender], [Age], [FormNo], [UserName], [DesktopPassword], [MacAddress], [AuthenticationKey], [RegistrationDate], [UserStatus]) VALUES (2, N'Sakib', N'Hossain', N'shfsakib@gmail.com', N'01954978560', N'123', N'Ctg', N'Male', 25, 4, N'shf', N'123', N'843A4B24CC18', N'SXqZAj0v', CAST(N'2021-11-05T10:31:33.000' AS DateTime), N'A')
SET IDENTITY_INSERT [dbo].[Users] OFF
USE [master]
GO
ALTER DATABASE [DataFillingDb] SET  READ_WRITE 
GO
