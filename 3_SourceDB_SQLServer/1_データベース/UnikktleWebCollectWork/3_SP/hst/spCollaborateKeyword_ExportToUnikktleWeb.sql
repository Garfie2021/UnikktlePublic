USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp7CollaborateKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp7CollaborateKeyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[hst].[sp6CollaborateKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp6CollaborateKeyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[hst].[spCollaborateKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeyword_ExportToUnikktleWeb] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �v�Z���ʂ�WEB�f�[�^�x�[�X�֏o��
-- UnikktleCollect�f�[�^�x�[�X��UnikktleWeb�f�[�^�x�[�X���A�ʃT�[�o��ŉғ����Ă��āA
-- UnikktleWebCollectWork�f�[�^�x�[�X�𒆊ԃf�[�^�x�[�X�Ƃ��Ďg�p����{�Ԋ��Ŏg���B
CREATE PROCEDURE [hst].[spCollaborateKeyword_ExportToUnikktleWeb]
AS
BEGIN

	SET NOCOUNT ON;

	declare cursor_tCollaborateKeyword cursor for
	SELECT DISTINCT [KeywordNo_��]
	FROM [UnikktleWebCollectWork].[hst].[tCollaborateKeyword];

	open cursor_tCollaborateKeyword;

	declare @KeywordNo_�� bigint;
	FETCH NEXT FROM cursor_tCollaborateKeyword INTO @KeywordNo_��;

	declare @Cnt int;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		SELECT @Cnt = Count(*)
		FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
		WHERE [KeywordNo_��] = @KeywordNo_�� ;

		BEGIN TRANSACTION;

		IF @Cnt > 0
		BEGIN

			-- �Â��f�[�^�͍폜
			-- �����R�[�h�������s��Ȃ̂�Update�ł͂Ȃ��ADELETE�����Ȃ��Ƃ����Ȃ��B
			DELETE FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
			WHERE [KeywordNo_��] = @KeywordNo_��;

		END;

		-- �L�[���[�h�̐V�����֘A��Insert
		INSERT INTO [UnikktleWeb].[clt].[tCollaborateKeyword]
		(
			[KeywordNo_��],
			[KeywordNo_��],
			[�����o���h�L�������g��]
		)
		SELECT
			[KeywordNo_��],
			[KeywordNo_��],
			[�����o���h�L�������g��]
		FROM
			[UnikktleWebCollectWork].[hst].[tCollaborateKeyword]
		WHERE
			[KeywordNo_��] = @KeywordNo_�� ;

		COMMIT; 


		FETCH NEXT FROM cursor_tCollaborateKeyword INTO @KeywordNo_��;
	END;


	CLOSE cursor_tCollaborateKeyword;
	DEALLOCATE cursor_tCollaborateKeyword;

	-- �f�o�b�O�p�Ƀf�[�^�͎c���Ă���
	--TRUNCATE TABLE [UnikktleWebCollectWork].[hst].[tCollaborateKeyword] ;

END
GO
/*
*/

