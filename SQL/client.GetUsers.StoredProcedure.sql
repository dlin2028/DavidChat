USE [DavidChat]
GO
/****** Object:  StoredProcedure [client].[GetUsers]    Script Date: 5/5/2018 12:31:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [client].[GetUsers] 
	-- Add the parameters for the stored procedure here
	@roomID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Name, Color, PublicID FROM Users WHERE @roomID = RoomID 
END



GO
