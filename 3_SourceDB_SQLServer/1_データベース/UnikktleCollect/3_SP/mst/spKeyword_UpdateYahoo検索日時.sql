USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_UpdateYahoo��������]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateYahoo��������] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_UpdateYahoo��������]
	@No				bigint,
	@Yahoo��������	DateTime
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET [Yahoo��������] = @Yahoo��������
	WHERE [No] = @No;

END
GO

