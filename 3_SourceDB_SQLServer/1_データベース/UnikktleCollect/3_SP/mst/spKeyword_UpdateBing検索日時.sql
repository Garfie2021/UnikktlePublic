USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateBing��������]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateBing��������] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateBing��������]
	@No				bigint,
	@Bing��������	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [Bing��������] = @Bing��������
	WHERE [No] = @No;

END
GO

