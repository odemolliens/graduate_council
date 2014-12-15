USE [master]
GO
/****** 对象:  Database [DB_Graduate_Council]    脚本日期: 12/15/2014 23:05:31 ******/
CREATE DATABASE [DB_Graduate_Council] ON  PRIMARY 
( NAME = N'DB_Graduate_Council', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\DB_Graduate_Council.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB_Graduate_Council_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\DB_Graduate_Council_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'DB_Graduate_Council', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_Graduate_Council].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [DB_Graduate_Council] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET ARITHABORT OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DB_Graduate_Council] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DB_Graduate_Council] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DB_Graduate_Council] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET  ENABLE_BROKER
GO
ALTER DATABASE [DB_Graduate_Council] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DB_Graduate_Council] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DB_Graduate_Council] SET  READ_WRITE
GO
ALTER DATABASE [DB_Graduate_Council] SET RECOVERY SIMPLE
GO
ALTER DATABASE [DB_Graduate_Council] SET  MULTI_USER
GO
ALTER DATABASE [DB_Graduate_Council] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DB_Graduate_Council] SET DB_CHAINING OFF
GO
USE [DB_Graduate_Council]
GO
/****** 对象:  Table [dbo].[T_CouncilDynamicNews]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_CouncilDynamicNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[PageView] [int] NOT NULL,
 CONSTRAINT [PK_T_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[T_FrontNews]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_FrontNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[PageView] [int] NOT NULL,
 CONSTRAINT [PK_T_FrontNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[T_NoticeNews]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_NoticeNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[PageView] [int] NOT NULL,
 CONSTRAINT [PK_T_NoticeNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[T_CampusEssay]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_CampusEssay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[PageView] [int] NOT NULL,
 CONSTRAINT [PK_T_CampusEssay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[T_BranchNews]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_BranchNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[PageView] [int] NOT NULL,
 CONSTRAINT [PK_T_BranchNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[T_JobNews]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_JobNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[PageView] [int] NOT NULL,
 CONSTRAINT [PK_T_JobNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[T_CouncilExpNews]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_CouncilExpNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[PageView] [int] NOT NULL,
 CONSTRAINT [PK_T_CouncilExpNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[T_LinkInfo]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_LinkInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Link] [varchar](200) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_LinkInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[T_BannerImg]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_BannerImg](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Path] [varchar](200) NOT NULL,
	[IsVisible] [bit] NOT NULL,
 CONSTRAINT [PK_T_BannerImg] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[T_UserInfo]    脚本日期: 12/15/2014 23:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
