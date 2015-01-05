USE [fifthweek-db]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
DROP TABLE [dbo].[New_AspNetUserLogins]
DROP TABLE [dbo].[New_AspNetUserClaims]
DROP TABLE [dbo].[New_AspNetUserRoles]
DROP TABLE [dbo].[New_AspNetUsers]
DROP TABLE [dbo].[New_AspNetRoles]
*/


/****** Object:  Table [dbo].[New_AspNetRoles]    Script Date: 05/01/2015 15:30:02 ******/

CREATE TABLE [dbo].[New_AspNetRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.New_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO


/****** Object:  Table [dbo].[New_AspNetUsers]    Script Date: 05/01/2015 15:35:21 ******/

CREATE TABLE [dbo].[New_AspNetUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[ExampleWork] [nvarchar](max) NULL,
	[RegistrationDate] [datetime] NOT NULL DEFAULT ('1900-01-01T00:00:00.000'),
	[LastSignInDate] [datetime] NOT NULL DEFAULT ('1900-01-01T00:00:00.000'),
	[LastAccessTokenDate] [datetime] NOT NULL DEFAULT ('1900-01-01T00:00:00.000'),
 CONSTRAINT [PK_dbo.New_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO


/****** Object:  Table [dbo].[New_AspNetUserRoles]    Script Date: 05/01/2015 15:35:07 ******/

CREATE TABLE [dbo].[New_AspNetUserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.New_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[New_AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.New_AspNetUserRoles_dbo.New_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[New_AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[New_AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.New_AspNetUserRoles_dbo.New_AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[New_AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.New_AspNetUserRoles_dbo.New_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[New_AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[New_AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.New_AspNetUserRoles_dbo.New_AspNetUsers_UserId]
GO






/****** Object:  Table [dbo].[New_AspNetUserClaims]    Script Date: 05/01/2015 15:34:10 ******/

CREATE TABLE [dbo].[New_AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.New_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[New_AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.New_AspNetUserClaims_dbo.New_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[New_AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[New_AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.New_AspNetUserClaims_dbo.New_AspNetUsers_UserId]
GO


/****** Object:  Table [dbo].[New_AspNetUserLogins]    Script Date: 05/01/2015 15:34:47 ******/
SET ANSI_NULLS ON

CREATE TABLE [dbo].[New_AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.New_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[New_AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.New_AspNetUserLogins_dbo.New_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[New_AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[New_AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.New_AspNetUserLogins_dbo.New_AspNetUsers_UserId]
GO


INSERT INTO dbo.New_AspNetUsers SELECT * FROM dbo.AspNetUsers

GO


sp_rename N'[dbo].[AspNetRoles]', N'Old_AspNetRoles'
GO
sp_rename N'[dbo].[AspNetUserRoles]', N'Old_AspNetUserRoles'
GO
sp_rename N'[dbo].[AspNetUsers]', N'Old_AspNetUsers'
GO
sp_rename N'[dbo].[AspNetUserClaims]', N'Old_AspNetUserClaims'
GO
sp_rename N'[dbo].[AspNetUserLogins]', N'Old_AspNetUserLogins'
GO

sp_rename N'[dbo].[New_AspNetRoles]', N'AspNetRoles'
GO
sp_rename N'[dbo].[New_AspNetUserRoles]', N'AspNetUserRoles'
GO
sp_rename N'[dbo].[New_AspNetUsers]', N'AspNetUsers'
GO
sp_rename N'[dbo].[New_AspNetUserClaims]', N'AspNetUserClaims'
GO
sp_rename N'[dbo].[New_AspNetUserLogins]', N'AspNetUserLogins'
GO


DROP TABLE [dbo].[Old_AspNetUserLogins]
DROP TABLE [dbo].[Old_AspNetUserClaims]
DROP TABLE [dbo].[Old_AspNetUserRoles]
DROP TABLE [dbo].[Old_AspNetUsers]
DROP TABLE [dbo].[Old_AspNetRoles]


sp_rename N'[dbo].[PK_dbo.New_AspNetRoles]', N'PK_dbo.AspNetRoles', N'OBJECT'
GO
sp_rename N'[dbo].[PK_dbo.New_AspNetUserRoles]', N'PK_dbo.AspNetUserRoles', N'OBJECT'
GO

sp_rename N'[dbo].[PK_dbo.New_AspNetUsers]', N'PK_dbo.AspNetUsers', N'OBJECT'
GO
sp_rename N'[dbo].[PK_dbo.New_AspNetUserClaims]', N'PK_dbo.AspNetUserClaims', N'OBJECT'
GO
sp_rename N'[dbo].[PK_dbo.New_AspNetUserLogins]', N'PK_dbo.AspNetUserLogins', N'OBJECT'
GO


sp_rename N'[dbo].[FK_dbo.New_AspNetUserRoles_dbo.New_AspNetRoles_RoleId]', N'FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId', N'OBJECT'
GO
sp_rename N'[dbo].[FK_dbo.New_AspNetUserRoles_dbo.New_AspNetUsers_UserId]', N'FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId', N'OBJECT'
GO
sp_rename N'[dbo].[FK_dbo.New_AspNetUserClaims_dbo.New_AspNetUsers_UserId]', N'FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]', N'OBJECT'
GO
sp_rename N'[dbo].[FK_dbo.New_AspNetUserLogins_dbo.New_AspNetUsers_UserId]', N'FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId', N'OBJECT'
GO


