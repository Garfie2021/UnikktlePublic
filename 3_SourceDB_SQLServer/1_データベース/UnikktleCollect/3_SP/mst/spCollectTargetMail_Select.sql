USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spCollectTargetMail_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spCollectTargetMail_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spCollectTargetMail_Select]
AS
BEGIN

	SELECT [No]
		,[����]
		,[From_MailAddress]
		--,[�o�^����]
		--,[�X�V����]
	FROM [mst].[tCollectTargetMail]

END
GO

