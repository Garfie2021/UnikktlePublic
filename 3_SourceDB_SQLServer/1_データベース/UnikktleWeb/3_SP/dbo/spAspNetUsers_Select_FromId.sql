USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[dbo].[spAspNetUsers_Select_FromId]', N'P') IS NOT NULL
	DROP PROCEDURE [dbo].[spAspNetUsers_Select_FromId] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAspNetUsers_Select_FromId]
	@Id		nvarchar(450)
AS
BEGIN

	SELECT
		[Id],
		[No]
	FROM
		[dbo].[AspNetUsers]
  	WHERE
		[Id] = @Id
	;

END
GO
/*
*/