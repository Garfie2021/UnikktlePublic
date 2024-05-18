USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp5CollaborateKeywordBing_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp5CollaborateKeywordBing_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �Čv�Z�X�g�A�h
CREATE PROCEDURE [hst].[sp5CollaborateKeywordBing_Insert]
AS
BEGIN

	SET NOCOUNT ON;

	---- �v�Z�Ώۂ̃L�[���[�h�𒊏o�B
	---- ���e�X�g�^�p���Ȃ̂�1��/10�L�[���[�h�Âv�Z���Ă�B
	--declare cursor_tKeyword cursor for
	--SELECT Top 10000
	--	[No]
	--FROM 
	--	[mst].[tKeyword]
	--WHERE 
	--	([�̗p����ς�] = 0 OR ([�̗p] = 1 AND [�̗p����ς�] = 1)) AND
	--	[Bing��������] IS NOT NULL
	--ORDER BY
	--	[Collaborate�X�V����],
	--	[No];

	--open cursor_tKeyword;

	--declare @No bigint;

	--FETCH NEXT FROM cursor_tKeyword INTO @No;

	---- �v�Z�Ώۂ̃L�[���[�h�����[�v
	--WHILE @@FETCH_STATUS = 0
	--BEGIN

		-- �Â��f�[�^�͍폜
		DELETE FROM [hst].[t5CollaborateKeyword_Bing]
		--WHERE [KeywordNo_��] = @No;


		-- �L�[���[�h�̐V�����֘A��Insert
		INSERT INTO [hst].[t5CollaborateKeyword_Bing]
		(
			[KeywordNo_��],
			[KeywordNo_��],
			[�����o���h�L�������g��]
		)
		---- �L�[���[�h���ɍő�100���̊֘A�L�[���[�h�𒊏o
		--SELECT  --TOP(100)
		--	��No, 
		--	��No, 
		--	SUM(�����o���h�L�������g��) as �����o���h�L�������g��
		--FROM (
		--	SELECT
		--		��.[KeywordNo] AS ��No,
		--		��.[KeywordNo] AS ��No,
		--		COUNT(*) AS �����o���h�L�������g��
		--	FROM [hst].[t4CollectTargetKeyword_Bing] AS �� INNER JOIN [hst].[t4CollectTargetKeyword_Bing] AS ��
		--		ON ��.[SearchKeywordNo] = ��.[SearchKeywordNo] --AND ��.[SearchDate] = ��.[SearchDate] AND ��.[SearchResultNo] = ��.[SearchResultNo]
		--		--INNER JOIN [mst].[tKeyword] AS B
		--		--ON ��.[KeywordNo] = B.[No]
		--		--INNER JOIN [mst].[tKeyword] AS C
		--		--ON ��.[KeywordNo] = C.[No]
		--	--WHERE
		--		--��.[SearchKeywordNo] = @No --AND
		--		--(C.[�̗p����ς�] = 0 or (C.[�̗p] = 1 AND C.[�̗p����ς�] = 1))
		--	GROUP BY
		--		��.[KeywordNo],
		--		��.[KeywordNo]
		--	--ORDER BY
		--	--	��.[KeywordNo],
		--	--	��.[KeywordNo]

		--) AS t
		--WHERE
		--	[��No] <> [��No]
		--GROUP BY
		--	��No,
		--	��No
		----ORDER BY
		----	�����o���h�L�������g�� DESC

		SELECT
			[SearchKeywordNo],
			[KeywordNo],
			COUNT(*) AS �����o���h�L�������g��
		FROM
			[hst].[t4CollectTargetKeyword_Bing]
		GROUP BY
			[SearchKeywordNo],
			[KeywordNo]
		--ORDER BY
		--	[SearchKeywordNo],
		--	[KeywordNo]

		;


		--UPDATE [mst].[tKeyword]
		--SET	[Collaborate�X�V����] = GETDATE()
		--WHERE [No] = @No;

	--	FETCH NEXT FROM cursor_tKeyword INTO @No;
	--END

	--CLOSE cursor_tKeyword;
	--DEALLOCATE cursor_tKeyword;

END
GO
/*
*/

