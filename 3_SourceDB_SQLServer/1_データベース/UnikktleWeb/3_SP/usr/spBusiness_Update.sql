USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spBusiness_Update]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spBusiness_Update] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spBusiness_Update]
	@UserNo				bigint,
	@No					smallint,
	@Category			tinyint,
	@OrganizationName	nvarchar(50),
	@OrganizationURL	varchar(200)
AS
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tBusiness]
	SET
		[Category] = @Category,
		[OrganizationName] = @OrganizationName,
		[OrganizationURL] = @OrganizationURL
	WHERE
		[UserNo] = @UserNo AND
		[No] = @No
	;

END
GO
/*
*/