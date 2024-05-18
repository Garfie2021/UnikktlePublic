USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractWebPage_Update_�p��A������]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractWebPage_Update_�p��A������] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractWebPage_Update_�p��A������]
	@DomainNo			bigint,
	@UrlNo				bigint,
	@�X�V����			datetime,
	@���ꔻ��			tinyint,
	@HtmlTag���O��		nvarchar(max),
	@�s�v�����񏜊O��	nvarchar(max),
	@�p��A������		nvarchar(max),
	@�p��A���������O��	nvarchar(max)
	--@MeCab����			nvarchar(max)
AS
BEGIN

	UPDATE [hst].[t3ExtractWebPage]
	SET [�X�V����]				= @�X�V����,
		--[MeCabState]			= @MeCabState,
		[���ꔻ��]				= @���ꔻ��,
		[HtmlTag���O��]			= @HtmlTag���O��,
		[�s�v�����񏜊O��]		= @�s�v�����񏜊O��,
		[�p��A������]			= @�p��A������,
		[�p��A���������O��]	= @�p��A���������O��
		--[MeCab����]				= @MeCab����
	WHERE
		[DomainNo]	= @DomainNo AND
		[UrlNo]		= @UrlNo

END
GO

