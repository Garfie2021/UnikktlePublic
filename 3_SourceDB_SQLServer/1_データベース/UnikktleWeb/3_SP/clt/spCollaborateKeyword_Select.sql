USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spCollaborateKeyword_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spCollaborateKeyword_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spCollaborateKeyword_Select]
	@No			bigint,
	@AfterNum	bigint,
	@AllCnt		bigint OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	--declare	@No	bigint = 1
	--declare	@AfterNum	bigint = 0;
	--declare	@AllCnt		bigint

	SELECT Top 30
		--RowNumber,
		Id,
		r_w,
		Word
	FROM (
  		SELECT
			ROW_NUMBER() OVER (ORDER BY 同時出現ドキュメント数 DESC) AS RowNumber,
			A.[KeywordNo_先] AS Id,
			C.r_w AS r_w,
			C.Word AS Word
		FROM
			(
				SELECT
					[KeywordNo_先],
					[同時出現ドキュメント数]
				FROM
					[clt].[tCollaborateKeyword]
				WHERE
					[KeywordNo_元] = @No
			) AS A 
			INNER JOIN
			[clt].[tKeyword] AS C
			ON A.[KeywordNo_先] = C.[No]
		--ORDER BY
		--	同時出現ドキュメント数 DESC
	) AS T
	WHERE
		RowNumber > @AfterNum	-- ページング
	;

  	SELECT
		@AllCnt = Count(*)
	FROM
		[clt].[tCollaborateKeyword]
	WHERE
		[KeywordNo_元] = @No
	;

	--select @AllCnt as AllCnt;

END
GO
/*
*/

