USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spBusiness_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spBusiness_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spBusiness_Select]
	@UserNo		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] AS Id,
		[Category],
		[OrganizationName]
		--[OrganizationURL],
		--[BusinessURL]
	FROM 
		[usr].[tBusiness]
	WHERE
		[UserNo] = @UserNo
	;

END
GO
/*
*/