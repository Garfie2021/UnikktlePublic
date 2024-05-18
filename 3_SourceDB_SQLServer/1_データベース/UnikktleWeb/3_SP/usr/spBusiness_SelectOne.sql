USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spBusiness_SelectOne]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spBusiness_SelectOne] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spBusiness_SelectOne]
	@UserNo	bigint,
	@No		smallint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] AS Id,
		[Category],
		[OrganizationName],
		[OrganizationURL]
	FROM 
		[usr].[tBusiness]
	WHERE
		[UserNo] = @UserNo AND
		[No] = @No
	;

END
GO
/*
*/