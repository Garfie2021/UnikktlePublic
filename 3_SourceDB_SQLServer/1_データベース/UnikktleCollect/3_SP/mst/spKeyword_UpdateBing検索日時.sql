USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateBingŒŸõ“ú]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateBingŒŸõ“ú] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateBingŒŸõ“ú]
	@No				bigint,
	@BingŒŸõ“ú	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [BingŒŸõ“ú] = @BingŒŸõ“ú
	WHERE [No] = @No;

END
GO

