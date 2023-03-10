USE [teamcooperationdb]
GO

/****** Object:  Table [dbo].[Task]    Script Date: 09.01.2023 03:00:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[Estimation] [int] NULL,
	[Status] [varchar](50) NULL,
	[BoardId] [int] NULL
) ON [PRIMARY]
GO


