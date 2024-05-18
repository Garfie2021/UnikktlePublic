USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp5CollaborateKeywordBing_���ʊm�F]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp5CollaborateKeywordBing_���ʊm�F] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �Čv�Z�X�g�A�h
CREATE PROCEDURE [hst].[sp5CollaborateKeywordBing_���ʊm�F]
	@SearchKeywordNo	bigint,
	@KeywordNo			bigint
AS
BEGIN

	SET NOCOUNT ON;

	--declare	@SearchKeywordNo	bigint = 1;
	--declare	@KeywordNo	bigint = 1449;

	--SELECT  *
	SELECT
		COUNT(*) AS t4CollectTargetKeyword_Bing_����
	FROM
		[hst].[t4CollectTargetKeyword_Bing]
	WHERE
		[SearchKeywordNo] = @SearchKeywordNo AND [KeywordNo] = @KeywordNo
	;

	--SELECT  *
	SELECT
		�����o���h�L�������g��
	FROM
		[UnikktleCollect].[hst].[t5CollaborateKeyword_Bing]
	where
		[KeywordNo_��] = @SearchKeywordNo AND [KeywordNo_��] = @KeywordNo
	;


END
GO
/*
*/

