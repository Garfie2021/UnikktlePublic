USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractGoogle_Select_MeCabState0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractGoogle_Select_MeCabState0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractGoogle_Select_MeCabState0]
	@CntMeCabState0			bigint output,
	@Cnt�֘A�L�[���[�h�ȊO	bigint output,
	@Cnt���{��				bigint output,
	@Cnt�p��				bigint output
AS
BEGIN

	-- ����͂̓��{�ꃌ�R�[�h���擾
	SELECT
		[SearchKeywordNo],
		[SearchDate],
		[SearchResultNo],
		[�p��A���������O��]
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [���ꔻ��] = 1 AND [�֘A�L�[���[�h] = 0; 

	-- MeCabState0 �̌����B
	SELECT
		@CntMeCabState0 = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0; 

	-- �֘A�L�[���[�h�ȊO�B
	SELECT
		@Cnt�֘A�L�[���[�h�ȊO = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [�֘A�L�[���[�h] = 0 ; 

	-- ���{��f�[�^�̌����B
	SELECT
		@Cnt���{�� = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [���ꔻ��] = 1 AND [�֘A�L�[���[�h] = 0; 

	-- �p��f�[�^�̌����B
	SELECT
		@Cnt�p�� = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [���ꔻ��] = 2 AND [�֘A�L�[���[�h] = 0; 

END
GO

