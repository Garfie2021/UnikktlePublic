USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spBusiness_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spBusiness_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spBusiness_Insert]
	@UserNo				bigint,
	@Category			tinyint,
	@OrganizationName	nvarchar(50),
	@OrganizationURL	varchar(200)
AS
BEGIN

	--declare	@No		bigint = 1;

	INSERT INTO [usr].[tBusiness] (
        [UserNo],
        [No],
        [Category],
        [OrganizationName],
        [OrganizationURL]
    ) VALUES (
        @UserNo,
        (	SELECT 	
				CASE
				  WHEN COUNT([No])=0 THEN 1
				  ELSE CAST(MAX([No])+1 AS smallint)
				END
			FROM
				[usr].[tBusiness]
			where
				[UserNo] = @UserNo
		),
		@Category,
		@OrganizationName,
		@OrganizationURL
	);


END
GO
/*
*/