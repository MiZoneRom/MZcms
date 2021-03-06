USE [master]
GO
/****** Object:  Database [mzcms]    Script Date: 07/13/2019 16:06:06 ******/
CREATE DATABASE [mzcms] ON  PRIMARY 
( NAME = N'mzcms', FILENAME = N'D:\Git\MZcms Git\Database\mzcms.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'mzcms_log', FILENAME = N'D:\Git\MZcms Git\Database\mzcms_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [mzcms] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mzcms].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mzcms] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [mzcms] SET ANSI_NULLS OFF
GO
ALTER DATABASE [mzcms] SET ANSI_PADDING OFF
GO
ALTER DATABASE [mzcms] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [mzcms] SET ARITHABORT OFF
GO
ALTER DATABASE [mzcms] SET AUTO_CLOSE ON
GO
ALTER DATABASE [mzcms] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [mzcms] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [mzcms] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [mzcms] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [mzcms] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [mzcms] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [mzcms] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [mzcms] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [mzcms] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [mzcms] SET  DISABLE_BROKER
GO
ALTER DATABASE [mzcms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [mzcms] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [mzcms] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [mzcms] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [mzcms] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [mzcms] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [mzcms] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [mzcms] SET  READ_WRITE
GO
ALTER DATABASE [mzcms] SET RECOVERY SIMPLE
GO
ALTER DATABASE [mzcms] SET  MULTI_USER
GO
ALTER DATABASE [mzcms] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [mzcms] SET DB_CHAINING OFF
GO
USE [mzcms]
GO
/****** Object:  Table [dbo].[UserGroups]    Script Date: 07/13/2019 16:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroups](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Grade] [int] NULL,
	[UpgradeExp] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Point] [int] NULL,
	[IsDefault] [bit] NOT NULL,
	[IsUpgrade] [bit] NOT NULL,
	[IsLock] [bit] NOT NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserGroups] ON
INSERT [dbo].[UserGroups] ([Id], [Title], [Grade], [UpgradeExp], [Amount], [Point], [IsDefault], [IsUpgrade], [IsLock]) VALUES (1, N'注册会员', 1, 0, CAST(0.00 AS Decimal(18, 2)), 0, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[UserGroups] OFF
/****** Object:  Table [dbo].[SiteSettings]    Script Date: 07/13/2019 16:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NULL,
	[Value] [text] NULL,
 CONSTRAINT [PK_SiteSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SiteSettings] ON
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (1, N'SiteName', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (2, N'ICPNubmer', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (3, N'SiteIsClose', N'False')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (4, N'RegisterType', N'0')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (5, N'MobileVerifOpen', N'False')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (6, N'RegisterEmailRequired', N'True')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (7, N'EmailVerifOpen', N'True')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (8, N'Logo', N'/Storage/Plat/Site/logo.png')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (9, N'Keyword', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (10, N'Hotkeywords', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (11, N'PageFoot', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (12, N'WeixinAppId', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (13, N'WeixinAppSecret', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (14, N'WeixinAppletId', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (15, N'WeixinAppletSecret', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (16, N'UserCookieKey', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (17, N'QRCode', N'/Storage/Plat/Site/qrCode.png')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (18, N'FlowScript', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (19, N'Site_SEOTitle', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (20, N'Site_SEOKeywords', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (21, N'Site_SEODescription', N'')
INSERT [dbo].[SiteSettings] ([Id], [Key], [Value]) VALUES (22, N'SitePhone', N'123456')
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF
/****** Object:  Table [dbo].[ManagersRoles]    Script Date: 07/13/2019 16:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManagersRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_ManagersRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 07/13/2019 16:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Salt] [nvarchar](100) NOT NULL,
	[Avatar] [nvarchar](200) NULL,
	[RealName] [nvarchar](50) NULL,
	[Mobile] [nvarchar](100) NULL,
	[AddDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Managers] ON
INSERT [dbo].[Managers] ([Id], [RoleId], [UserName], [Password], [Salt], [Avatar], [RealName], [Mobile], [AddDate]) VALUES (2, 0, N'admin', N'e469bb0cd9c884d31fa2c0d5f0c37015', N'7dbcd316-fbdb-4429-a1b9-548fc82d3d58', NULL, NULL, NULL, CAST(0x0000AA8600000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Managers] OFF
/****** Object:  Table [dbo].[Users]    Script Date: 07/13/2019 16:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Salt] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Mobile] [nvarchar](100) NULL,
	[Email] [nvarchar](200) NULL,
	[Avatar] [nvarchar](200) NULL,
	[NickName] [nvarchar](200) NOT NULL,
	[IsMale] [bit] NOT NULL,
	[Birthday] [datetime] NULL,
	[Area] [nvarchar](100) NULL,
	[Address] [nvarchar](300) NULL,
	[QQ] [nvarchar](100) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Point] [int] NULL,
	[Exp] [int] NULL,
	[Status] [int] NULL,
	[RegDate] [datetime] NOT NULL,
	[RegIP] [nvarchar](100) NULL,
	[LastLoginDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManagerPrivileges]    Script Date: 07/13/2019 16:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManagerPrivileges](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Privilege] [int] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_ManagerPrivileges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ManagerPrivileges] ON [dbo].[ManagerPrivileges] 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManagerLog]    Script Date: 07/13/2019 16:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManagerLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[ActionType] [nvarchar](100) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[UserIP] [nvarchar](50) NULL,
	[AddDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ManagerLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Users_UserGroups]    Script Date: 07/13/2019 16:06:07 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserGroups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[UserGroups] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserGroups]
GO
/****** Object:  ForeignKey [FK_ManagerPrivileges_ManagersRoles]    Script Date: 07/13/2019 16:06:07 ******/
ALTER TABLE [dbo].[ManagerPrivileges]  WITH CHECK ADD  CONSTRAINT [FK_ManagerPrivileges_ManagersRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[ManagersRoles] ([Id])
GO
ALTER TABLE [dbo].[ManagerPrivileges] CHECK CONSTRAINT [FK_ManagerPrivileges_ManagersRoles]
GO
/****** Object:  ForeignKey [FK_ManagerLog_Managers]    Script Date: 07/13/2019 16:06:07 ******/
ALTER TABLE [dbo].[ManagerLog]  WITH CHECK ADD  CONSTRAINT [FK_ManagerLog_Managers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Managers] ([Id])
GO
ALTER TABLE [dbo].[ManagerLog] CHECK CONSTRAINT [FK_ManagerLog_Managers]
GO
