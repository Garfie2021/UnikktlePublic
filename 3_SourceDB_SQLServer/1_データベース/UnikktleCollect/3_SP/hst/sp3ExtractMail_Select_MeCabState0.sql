USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractMail_Select_MeCabState0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractMail_Select_MeCabState0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractMail_Select_MeCabState0]
AS
BEGIN

	-- ����͂œ��{��̃��R�[�h���擾
	SELECT
		 [CollectTargetNo]
		,[SendDate]
		,[�o�^����]
		,[�p��A���������O��]
	FROM [hst].[t3ExtractMail]
	WHERE [MeCabState] = 0 AND [���ꔻ��] = 1; 

END
GO

