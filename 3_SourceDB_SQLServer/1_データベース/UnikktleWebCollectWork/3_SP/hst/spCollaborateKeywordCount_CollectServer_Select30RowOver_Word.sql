USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver_Word]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver_Word] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver_Word]
AS
BEGIN

	SET NOCOUNT ON;


	-- [KeywordNo_��]��30���ȏ�d�����Ă���s�𒊏o�B��[KeywordNo_��]�Ɋ֘A����L�[���\�h�i[KeywordNo_��]�j��30���ȏ�́A[KeywordNo_��]�𒊏o���Ă���̂Ɠ����Ӗ��B

	SELECT 
		[Word]
	FROM
		[UnikktleCollect].[mst].[tKeyword]
	WHERE
		[No] IN (  
  			SELECT
				[KeywordNo_��] AS [No]
			FROM
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_CollectServer]
			WHERE
				[KeywordNo_��_Count] > 30
		)
	ORDER BY
		[No]
	;

END
GO
/*
*/

