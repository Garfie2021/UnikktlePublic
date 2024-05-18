USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateGoogleŒŸõ“ú]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateGoogleŒŸõ“ú] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateGoogleŒŸõ“ú]
	@No				bigint,
	@GoogleŒŸõ“ú	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [GoogleŒŸõ“ú] = @GoogleŒŸõ“ú
	WHERE [No] = @No;

END
GO

