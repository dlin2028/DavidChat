USE [DavidChat]
GO
/****** Object:  Table [dbo].[RoomUsers]    Script Date: 5/12/2018 12:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomUsers](
	[UserID] [uniqueidentifier] NOT NULL,
	[RoomID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[RoomUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoomUsers_Rooms] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO
ALTER TABLE [dbo].[RoomUsers] CHECK CONSTRAINT [FK_RoomUsers_Rooms]
GO
ALTER TABLE [dbo].[RoomUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoomUsers_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[RoomUsers] CHECK CONSTRAINT [FK_RoomUsers_Users]
GO
