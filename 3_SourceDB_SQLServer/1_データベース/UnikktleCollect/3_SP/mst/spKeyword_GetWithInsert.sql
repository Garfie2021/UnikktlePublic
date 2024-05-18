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
	@Όζͺ				tinyint,
	@Word					nvarchar(100),
	@πΝ³f[^			nvarchar(max),

	---- iniWebPagej
	@DomainNo				bigint,
	@UrlNo					bigint,

	-- out
	@No						bigint	output,
	@Μp					tinyint	output,
	@Μp»θΟέ			tinyint	output
AS
BEGIN

	/*
	DECLARE @Word	nvarchar(100) = 'C#' ;
	DECLARE @No						bigint	;
	DECLARE @Μp					tinyint	;
	DECLARE @Μp»θΟέ			tinyint	;
	*/

	SELECT @No = [No], @Μp = [Μp], @Μp»θΟέ = [Μp»θΟέ]
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
            [Όζͺ],
			[r_w],
            [Word],
			[πΝ³f[^]
	    ) VALUES (
			@CollectTargetCategory,
			@CollectNo,
			@SearchResultNo,
			@SendDate,
			@DomainNo,
			@UrlNo,
            @Όζͺ,
			UnikktleCmn.calc.fTextWidth(@Word),
            @Word,
			@πΝ³f[^
		);

		SELECT @No = [No], @Μp = [Μp], @Μp»θΟέ = [Μp»θΟέ]
		FROM [mst].[tKeyword]
		WHERE [Word] = @Word;
	END;

END
GO
/*
*/

