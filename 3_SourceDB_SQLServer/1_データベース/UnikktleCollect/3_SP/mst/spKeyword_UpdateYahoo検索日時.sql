USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateYahoo検索日時]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateYahoo検索日時] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateYahoo検索日時]
	@No				bigint,
	@Yahoo検索日時	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [Yahoo検索日時] = @Yahoo検索日時
	WHERE [No] = @No;

END
GO

