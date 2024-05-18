USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractWebPage_Update_英語連結名詞]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractWebPage_Update_英語連結名詞] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractWebPage_Update_英語連結名詞]
	@DomainNo			bigint,
	@UrlNo				bigint,
	@更新日時			datetime,
	@言語判定			tinyint,
	@HtmlTag除外後		nvarchar(max),
	@不要文字列除外後	nvarchar(max),
	@英語連結名詞		nvarchar(max),
	@英語連結名詞除外後	nvarchar(max)
	--@MeCab名詞			nvarchar(max)
AS
BEGIN

	UPDATE [hst].[t3ExtractWebPage]
	SET [更新日時]				= @更新日時,
		--[MeCabState]			= @MeCabState,
		[言語判定]				= @言語判定,
		[HtmlTag除外後]			= @HtmlTag除外後,
		[不要文字列除外後]		= @不要文字列除外後,
		[英語連結名詞]			= @英語連結名詞,
		[英語連結名詞除外後]	= @英語連結名詞除外後
		--[MeCab名詞]				= @MeCab名詞
	WHERE
		[DomainNo]	= @DomainNo AND
		[UrlNo]		= @UrlNo

END
GO

