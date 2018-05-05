USE [DavidChat]
GO
/****** Object:  StoredProcedure [client].[JoinRoom]    Script Date: 5/5/2018 12:31:53 PM ******/
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
	@roomID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT @userID, @roomID
END

GO
