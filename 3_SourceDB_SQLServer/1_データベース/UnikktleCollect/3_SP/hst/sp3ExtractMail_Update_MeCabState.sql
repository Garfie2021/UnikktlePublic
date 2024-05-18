USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractMail_Update_MeCabState]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractMail_Update_MeCabState]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractMail_Update_MeCabState]
	@CollectTargetNo	bigint,
	@SendDate			datetime,
	@�o�^����			datetime,
	@MeCabState			tinyint,
	@MeCab����			nvarchar(max)
AS
BEGIN

	UPDATE [hst].[t3ExtractMail]
	SET 
		[MeCabState] = @MeCabState,
		[MeCab����] = @MeCab����
	WHERE
		[CollectTargetNo] = @CollectTargetNo AND
		[SendDate] = @SendDate AND
		[�o�^����] = @�o�^����;

END
GO

