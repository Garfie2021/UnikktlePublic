USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[mst].[sp7Keyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[sp7Keyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[mst].[sp6Keyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[sp6Keyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[mst].[spKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_ExportToUnikktleWeb] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �v�Z���ʂ�WEB�f�[�^�x�[�X�֏o��
-- UnikktleCollect�f�[�^�x�[�X��UnikktleWeb�f�[�^�x�[�X���A�ʃT�[�o��ŉғ����Ă��āA
-- UnikktleWebCollectWork�f�[�^�x�[�X�𒆊ԃf�[�^�x�[�X�Ƃ��Ďg�p����{�Ԋ��Ŏg���B
CREATE PROCEDURE [mst].[spKeyword_ExportToUnikktleWeb]
AS
BEGIN

	SET NOCOUNT ON;


	declare cursor_tKeyword cursor for
	SELECT [No]
	FROM [UnikktleWebCollectWork].[mst].[tKeyword];

	open cursor_tKeyword;

	declare @No bigint;
	FETCH NEXT FROM cursor_tKeyword INTO @No;

	declare @Cnt int;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		SELECT @Cnt = COUNT(*)
		FROM [UnikktleWeb].[clt].[tKeyword]
		WHERE [No] = @No ;

		IF @Cnt < 1
		BEGIN
			-- ���o�^�̃L�[���[�h

			--print 'insert';
			--print @No;

			INSERT INTO [UnikktleWeb].[clt].[tKeyword]
			(
				[No],
				[r_w],
				[Word],
				[FullTextSupple]
			)
			SELECT
				[No],
				[r_w],
				[Word],
				[FullTextSupple]
			FROM
				[UnikktleWebCollectWork].[mst].[tKeyword]
			WHERE
				[No] = @No ;

		END
		ELSE
		BEGIN
			-- �o�^�ς݂̃L�[���[�h

			--print 'update';
			--print @No;

			UPDATE
				[UnikktleWeb].[clt].[tKeyword]
			SET
				[r_w] = B.[r_w],
				[Word] = B.[Word],
				[FullTextSupple] = B.[FullTextSupple]
			FROM
				[UnikktleWeb].[clt].[tKeyword] AS A INNER JOIN [UnikktleWebCollectWork].[mst].[tKeyword] AS B
				ON A.[No] = B.[No]
			WHERE
				A.[No] = @No ;
			;

		END;


		FETCH NEXT FROM cursor_tKeyword INTO @No;
	END;

	CLOSE cursor_tKeyword;
	DEALLOCATE cursor_tKeyword;


	-- �f�o�b�O�p�Ƀf�[�^�͎c���Ă���
	--TRUNCATE TABLE [UnikktleWebCollectWork].[mst].[tKeyword] ;

/*
*/
END
GO

GO