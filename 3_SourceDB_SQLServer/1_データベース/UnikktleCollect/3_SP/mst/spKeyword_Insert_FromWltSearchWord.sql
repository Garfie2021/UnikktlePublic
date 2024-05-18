USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spKeyword_Insert_FromWltSearchWord]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Insert_FromWltSearchWord] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Insert_FromWltSearchWord]
AS
BEGIN

	INSERT INTO [mst].[tKeyword] (
        --[No],	-- localhost�Ńe�X�g����ۂ͗L���ɂ���B
        [CollectTargetCategory],
		[�����敪],
        [r_w],
        [Word],
        [��͌��f�[�^]
	)
	SELECT
		--100,	-- localhost�Ńe�X�g����ۂ͗L���ɂ���B
		6,	-- [CollectTargetCategory]�ɂ́u6�FWeb�T�[�o��UI���猟�����ꂽ�L�[���[�h�v��ݒ�B
		0,	-- �u0�F�l��Ŗڌ��v�̈����ɂ���B
		UnikktleCmn.calc.fTextWidth([Word]),
		[Word],
		''
	FROM
		[wlt].[tSearchWord]
	WHERE
		[Word] NOT IN (SELECT [Word] FROM [mst].[tKeyword])

END
GO
/*
*/

