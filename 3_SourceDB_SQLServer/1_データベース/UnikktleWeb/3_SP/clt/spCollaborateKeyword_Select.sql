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
			ROW_NUMBER() OVER (ORDER BY �����o���h�L�������g�� DESC) AS RowNumber,
			A.[KeywordNo_��] AS Id,
			C.r_w AS r_w,
			C.Word AS Word
		FROM
			(
				SELECT
					[KeywordNo_��],
					[�����o���h�L�������g��]
				FROM
					[clt].[tCollaborateKeyword]
				WHERE
					[KeywordNo_��] = @No
			) AS A 
			INNER JOIN
			[clt].[tKeyword] AS C
			ON A.[KeywordNo_��] = C.[No]
		--ORDER BY
		--	�����o���h�L�������g�� DESC
	) AS T
	WHERE
		RowNumber > @AfterNum	-- �y�[�W���O
	;

  	SELECT
		@AllCnt = Count(*)
	FROM
		[clt].[tCollaborateKeyword]
	WHERE
		[KeywordNo_��] = @No
	;

	--select @AllCnt as AllCnt;

END
GO
/*
*/

