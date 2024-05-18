USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateYahooŒŸõ“ú]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateYahooŒŸõ“ú] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateYahooŒŸõ“ú]
	@No				bigint,
	@YahooŒŸõ“ú	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [YahooŒŸõ“ú] = @YahooŒŸõ“ú
	WHERE [No] = @No;

END
GO

