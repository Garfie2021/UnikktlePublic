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
	@�����敪				tinyint,
	@Word					nvarchar(100),
	@��͌��f�[�^			nvarchar(max),

	---- in�iWebPage�j
	@DomainNo				bigint,
	@UrlNo					bigint,

	-- out
	@No						bigint	output,
	@�̗p					tinyint	output,
	@�̗p����ς�			tinyint	output
AS
BEGIN

	/*
	DECLARE @Word	nvarchar(100) = 'C#' ;
	DECLARE @No						bigint	;
	DECLARE @�̗p					tinyint	;
	DECLARE @�̗p����ς�			tinyint	;
	*/

	SELECT @No = [No], @�̗p = [�̗p], @�̗p����ς� = [�̗p����ς�]
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
            [�����敪],
			[r_w],
            [Word],
			[��͌��f�[�^]
	    ) VALUES (
			@CollectTargetCategory,
			@CollectNo,
			@SearchResultNo,
			@SendDate,
			@DomainNo,
			@UrlNo,
            @�����敪,
			UnikktleCmn.calc.fTextWidth(@Word),
            @Word,
			@��͌��f�[�^
		);

		SELECT @No = [No], @�̗p = [�̗p], @�̗p����ς� = [�̗p����ς�]
		FROM [mst].[tKeyword]
		WHERE [Word] = @Word;
	END;

END
GO
/*
*/

