USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[dbo].[spAspNetUsers_Select_FromUserName]', N'P') IS NOT NULL
	DROP PROCEDURE [dbo].[spAspNetUsers_Select_FromUserName] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAspNetUsers_Select_FromUserName]
	@UserName		nvarchar(256)
AS
BEGIN

	SELECT
		[Id],
		[No]
	FROM
		[dbo].[AspNetUsers]
  	WHERE
		[UserName] = @UserName
	;

END
GO
/*
*/