USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spCollaborateKeyword_Select_ExcludeNo]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spCollaborateKeyword_Select_ExcludeNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spCollaborateKeyword_Select_ExcludeNo]
	@No			bigint,
	@ExcludeNo	bigint,
	@AfterNum	bigint,
	@AllCnt		bigint OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	--declare	@No	bigint = 1
	--declare	@ExcludeNo	bigint = 1
	--declare	@afterNum	bigint = 20;

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
					[KeywordNo_��] = @No AND [KeywordNo_��] <> @ExcludeNo					
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
		[KeywordNo_��] = @No AND [KeywordNo_��] <> @ExcludeNo
	;

END
GO
/*
*/

