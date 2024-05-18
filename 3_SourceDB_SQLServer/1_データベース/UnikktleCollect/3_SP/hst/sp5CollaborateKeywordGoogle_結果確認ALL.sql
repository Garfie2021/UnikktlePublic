USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp5CollaborateKeywordGoogle_Select_���ʊm�F]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp5CollaborateKeywordGoogle_Select_���ʊm�F] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �Čv�Z�X�g�A�h
CREATE PROCEDURE [hst].[sp5CollaborateKeywordGoogle_Select_���ʊm�F]
	@�����o���h�L�������g��	bigint
AS
BEGIN

	SET NOCOUNT ON;

  	SELECT
		A.[KeywordNo_��] AS ��No,
		B.Word AS ��Word,
		A.[KeywordNo_��] AS ��No,
		C.Word AS ��Word,
		A.�����o���h�L�������g�� AS �����o���h�L�������g��
	FROM [hst].[t5CollaborateKeyword_Google] AS A 
		INNER JOIN [mst].[tKeyword] AS B ON A.[KeywordNo_��] = B.[No]
		INNER JOIN [mst].[tKeyword] AS C ON A.[KeywordNo_��] = C.[No]
	WHERE
		�����o���h�L�������g�� > @�����o���h�L�������g��
	ORDER BY
		��No,
		�����o���h�L�������g�� DESC
	;


END
GO
/*
*/

