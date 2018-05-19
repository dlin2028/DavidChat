USE [DavidChat]
GO
/****** Object:  StoredProcedure [dbo].[ProcedureName]    Script Date: 5/19/2018 12:32:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcedureName] 
	-- Add the parameters for the stored procedure here
	@userID uniqueidentifier,
	@roomID uniqueidentifier,
	@text char(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Messages (UserID, RoomID, Text, Time)
	VALUES (@userID, @roomID, @text, GETDATE())
END

GO
