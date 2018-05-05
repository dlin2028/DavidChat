USE [DavidChat]
GO
/****** Object:  StoredProcedure [client].[GetMessages]    Script Date: 5/5/2018 12:31:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [client].[GetMessages] 
	-- Add the parameters for the stored procedure here
	@roomID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Users.Name, Messages.Text, Messages.Time
	FROM Users
	INNER JOIN Messages ON Users.ID = Messages.UserID AND Messages.RoomID = @roomID
END

GO
