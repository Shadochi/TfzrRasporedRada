USE [RasporedRada]
GO
/****** Object:  Table [dbo].[RasporedRada]    Script Date: 9/15/2020 12:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RasporedRada](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](50) NOT NULL,
	[Prezime] [nvarchar](50) NOT NULL,
	[Klijent] [nvarchar](50) NOT NULL,
	[Sati] [int] NOT NULL,
	[Datum] [date] NOT NULL,
	[Posao] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
