USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[dbo].[spAspNetUsers_GetNo]', N'P') IS NOT NULL
	DROP PROCEDURE [dbo].[spAspNetUsers_GetNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAspNetUsers_GetNo]
	@Id		nvarchar(450),
	@No		bigint OUTPUT
AS
BEGIN

	SELECT
		@No = [No]
	FROM
		[dbo].[AspNetUsers]
  	WHERE
		[Id] = @Id
	;

END
GO
/*
*/