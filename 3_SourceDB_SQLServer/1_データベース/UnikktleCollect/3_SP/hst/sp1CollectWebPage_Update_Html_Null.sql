USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Update_Html_Null]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Update_Html_Null] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO	

CREATE PROCEDURE [hst].[sp1CollectWebPage_Update_Html_Null]
AS
BEGIN

	UPDATE [hst].[t1CollectWebPage]
	SET
		[Html] = null
	WHERE
		[CutoutStateUrl] = 1 AND
		[CutoutStateHtml] = 1 ;

END
GO

