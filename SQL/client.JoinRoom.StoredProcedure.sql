USE [DavidChat]
GO
/****** Object:  StoredProcedure [client].[JoinRoom]    Script Date: 5/12/2018 12:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [client].[JoinRoom] 
	-- Add the parameters for the stored procedure here
	@userID uniqueidentifier,
	@roomName nchar(10),
	@password nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @roomID uniqueidentifier
	SET @roomID = (SELECT RoomID FROM Rooms WHERE @password = Password AND Name = @roomName)

	INSERT INTO RoomUsers (UserID, RoomID)
	VALUES (@userID, @roomID)

	SELECT @roomID AS 'RoomID'
END



GO
