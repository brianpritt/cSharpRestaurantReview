USE [restaurant_directory]
GO
/****** Object:  Table [dbo].[cuisines]    Script Date: 12/7/2016 9:44:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
