USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spKeyword_GetWithInsert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_GetWithInsert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_GetWithInsert]
	-- in
	@CollectTargetCategory	tinyint,
	@CollectNo				bigint,
	@SearchResultNo			tinyint,
	@SendDate				datetime,
	@名詞区分				tinyint,
	@Word					nvarchar(100),
	@解析元データ			nvarchar(max),

	---- in（WebPage）
	@DomainNo				bigint,
	@UrlNo					bigint,

	-- out
	@No						bigint	output,
	@採用					tinyint	output,
	@採用判定済み			tinyint	output
AS
BEGIN

	/*
	DECLARE @Word	nvarchar(100) = 'C#' ;
	DECLARE @No						bigint	;
	DECLARE @採用					tinyint	;
	DECLARE @採用判定済み			tinyint	;
	*/

	SELECT @No = [No], @採用 = [採用], @採用判定済み = [採用判定済み]
	FROM [mst].[tKeyword]
	WHERE [Word] = @Word;
	
	IF @No IS NULL
	BEGIN
		INSERT INTO [mst].[tKeyword] (
			[CollectTargetCategory],
			[CollectNo],
			[SearchResultNo],
			[SendDate],
			[DomainNo],
			[UrlNo],
            [名詞区分],
			[r_w],
            [Word],
			[解析元データ]
	    ) VALUES (
			@CollectTargetCategory,
			@CollectNo,
			@SearchResultNo,
			@SendDate,
			@DomainNo,
			@UrlNo,
            @名詞区分,
			UnikktleCmn.calc.fTextWidth(@Word),
            @Word,
			@解析元データ
		);

		SELECT @No = [No], @採用 = [採用], @採用判定済み = [採用判定済み]
		FROM [mst].[tKeyword]
		WHERE [Word] = @Word;
	END;

END
GO
/*
*/

