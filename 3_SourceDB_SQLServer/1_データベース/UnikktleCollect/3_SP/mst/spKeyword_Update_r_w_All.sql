USE UnikktleCollect
GO
/*
*/
IF OBJECT_ID(N'[mst].[spKeyword_UpdateAll_r_w]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_UpdateAll_r_w]

IF OBJECT_ID(N'[mst].[spKeyword_Update_r_w_All]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Update_r_w_All]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Update_r_w_All]
AS
BEGIN

	UPDATE
		[mst].[tKeyword]
	SET
		[r_w] = UnikktleCmn.calc.fTextWidth([Word]),
		[çXêVì˙éû] = getdate()
	;

END
GO
/*
*/