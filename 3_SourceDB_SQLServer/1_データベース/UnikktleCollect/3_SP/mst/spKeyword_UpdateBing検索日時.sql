USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateBing検索日時]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateBing検索日時] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateBing検索日時]
	@No				bigint,
	@Bing検索日時	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [Bing検索日時] = @Bing検索日時
	WHERE [No] = @No;

END
GO

