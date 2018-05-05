USE [DavidChat]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/5/2018 12:31:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[Color] [int] NULL,
	[RoomID] [uniqueidentifier] NULL,
	[PublicID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
