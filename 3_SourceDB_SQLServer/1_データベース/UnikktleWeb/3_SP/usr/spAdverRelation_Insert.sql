USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverRelation_Insert]
	@UserNo				bigint,
	@BusinessNo			smallint,
	@Valid				tinyint,
	@Category			tinyint,
	@AdverName			nvarchar(50),
	@AdverTitle1		nvarchar(50),
	@AdverTitle2		nvarchar(50),
	@AdverURL			varchar(200),
	@AdvertisingBudget	int,
	@No					int				OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;


	INSERT INTO [usr].[tAdverRelation] (
        [UserNo],
        [BusinessNo],
        [No],
        [Valid],
        [Category],
        [AdverName],
		[AdverTitle1],
		[AdverTitle2],
		[AdverTitle_r_w],
        [AdverURL],
		[AdvertisingBudget]
    ) VALUES (
        @UserNo,
		@BusinessNo,
        (	SELECT 	
				CASE
				  WHEN COUNT([No])=0 THEN 1
				  ELSE CAST(MAX([No])+1 AS smallint)
				END
			FROM
				[usr].[tAdverRelation]
			where
				[UserNo] = @UserNo
		),
		@Valid,
		@Category,
		@AdverName,
		@AdverTitle1,
		@AdverTitle2,
		mst.fPRTextWidth(@AdverTitle1, @AdverTitle2),
		@AdverURL,
		@AdvertisingBudget
	);

	--declare	@UserNo		bigint = 40;
	--declare	@BusinessNo	smallint = 1;
	

	--SELECT *
	SELECT @No = MAX([No])
	FROM [usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
        [BusinessNo] = @BusinessNo ;

END
GO
/*
*/