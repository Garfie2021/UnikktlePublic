USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_Select_更新日時]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Select_更新日時] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Select_更新日時]
	@更新日時End	DateTime,
	@更新日時Start	DateTime
AS
BEGIN

	SELECT
		[No],
		[r_w],
		[Word],
		[解析元データ]
	FROM
		[mst].[tKeyword]
	WHERE
		@更新日時End >= [更新日時] AND [更新日時] >= @更新日時Start

END
GO

