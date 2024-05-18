USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[dbo].[spAspNetUsers_GetNo_FromEmail]', N'P') IS NOT NULL
	DROP PROCEDURE [dbo].[spAspNetUsers_GetNo_FromEmail] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAspNetUsers_GetNo_FromEmail]
	@Email	varchar(256),
	@No		bigint OUTPUT
AS
BEGIN

	SELECT
		@No = [No]
	FROM
		[dbo].[AspNetUsers]
	WHERE
		UPPER([Email]) = UPPER(@Email)
	;

END
GO
/*
*/