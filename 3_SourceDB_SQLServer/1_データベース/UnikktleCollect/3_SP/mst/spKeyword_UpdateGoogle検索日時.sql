USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateGoogle検索日時]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateGoogle検索日時] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateGoogle検索日時]
	@No				bigint,
	@Google検索日時	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [Google検索日時] = @Google検索日時
	WHERE [No] = @No;

END
GO

