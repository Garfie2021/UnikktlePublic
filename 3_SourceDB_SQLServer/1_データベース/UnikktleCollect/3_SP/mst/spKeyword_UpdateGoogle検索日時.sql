USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateGoogle��������]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateGoogle��������] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateGoogle��������]
	@No				bigint,
	@Google��������	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [Google��������] = @Google��������
	WHERE [No] = @No;

END
GO

