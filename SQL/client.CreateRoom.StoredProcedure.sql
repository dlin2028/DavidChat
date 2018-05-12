USE [DavidChat]
GO
/****** Object:  StoredProcedure [client].[CreateRoom]    Script Date: 5/12/2018 12:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [client].[CreateRoom] 
	-- Add the parameters for the stored procedure here
	@userID nchar(10) = 0,
	@name nchar(10) = 0, 
	@password nchar(10) = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT * FROM Rooms WHERE Name = @name)
	BEGIN
		INSERT INTO Rooms (Name, Password)
		VALUES (@name, @password)

		DECLARE @roomID uniqueidentifier
		SET @roomID = (SELECT TOP 1 RoomID FROM Rooms WHERE Name = @name)
		
		INSERT INTO RoomUsers (UserID, RoomID)
		VALUES (@userID, @roomID)

		SELECT @roomID AS 'RoomID'
	END
END



GO
