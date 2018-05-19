USE [DavidChat]
GO
/****** Object:  StoredProcedure [client].[LeaveRoom]    Script Date: 5/19/2018 12:32:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [client].[LeaveRoom] 
	-- Add the parameters for the stored procedure here
	@userID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure her
	UPDATE Users SET RoomID = null WHERE @userID = ID
END

GO
