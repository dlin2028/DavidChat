USE [DavidChat]
GO
/****** Object:  StoredProcedure [client].[Register]    Script Date: 5/12/2018 12:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [client].[Register] 
	-- Add the parameters for the stored procedure here
	@name nchar(10),
	@color int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @userID uniqueidentifier
	SET @userID = (NewID())

	INSERT INTO Users(ID, Name, Color)
	VALUES (@userID, @name, @color)

	SELECT @userID AS 'UserID'
END

GO
