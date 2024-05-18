USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver]
AS
BEGIN

	SET NOCOUNT ON;


	-- [KeywordNo_��]��30���ȏ�d�����Ă���s�𒊏o�B��[KeywordNo_��]�Ɋ֘A����L�[���\�h�i[KeywordNo_��]�j��30���ȏ�́A[KeywordNo_��]�𒊏o���Ă���̂Ɠ����Ӗ��B

	SELECT
		[KeywordNo_��] AS [No]
	FROM
		[hst].[tCollaborateKeywordCount_CollectServer]
	WHERE
		[KeywordNo_��_Count] > 30
	ORDER BY
		[KeywordNo_��]
	;

END
GO
/*
*/

