USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp7CollaborateKeyword_Export]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp7CollaborateKeyword_Export] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �v�Z���ʂ�WEB�f�[�^�x�[�X�֏o��
-- UnikktleCollect�f�[�^�x�[�X��UnikktleWeb�f�[�^�x�[�X���A����T�[�o��ŉғ����Ă�����Ŏg���B
-- ��[mst].[tKeyword]�̃G�N�X�|�[�g���ĂȂ��̂ŁA�{�Ԋ��ł͎g���Ȃ��B�J�����p�B
CREATE PROCEDURE [hst].[sp7CollaborateKeyword_Export]
AS
BEGIN

	SET NOCOUNT ON;

	declare cursor_tKeyword cursor for
	SELECT [No]
	FROM [mst].[tKeyword];

	open cursor_tKeyword;

	declare @No bigint;
	FETCH NEXT FROM cursor_tKeyword INTO @No;

	declare @CntCollect bigint;
	declare @CntWeb bigint;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		SELECT @CntCollect = Count(*)
		FROM [hst].[t6CollaborateKeyword]
		WHERE [KeywordNo_��] = @No ;

		SELECT @CntWeb = Count(*)
		FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
		WHERE [KeywordNo_��] = @No ;

		IF @CntCollect <> @CntWeb
		BEGIN
			BEGIN TRANSACTION;  

			-- �Â��f�[�^�͍폜
			DELETE FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
			WHERE [KeywordNo_��] = @No;

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
				[hst].[t6CollaborateKeyword]
			WHERE
				[KeywordNo_��] = @No ;

			COMMIT; 
		END;

		FETCH NEXT FROM cursor_tKeyword INTO @No;
	END;

	CLOSE cursor_tKeyword;
	DEALLOCATE cursor_tKeyword;

END
GO
/*
*/

