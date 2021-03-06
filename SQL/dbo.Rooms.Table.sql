USE [DavidChat]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 5/19/2018 12:32:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[Password] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Rooms] ADD  CONSTRAINT [DF_Rooms_RoomID]  DEFAULT (newid()) FOR [RoomID]
GO
