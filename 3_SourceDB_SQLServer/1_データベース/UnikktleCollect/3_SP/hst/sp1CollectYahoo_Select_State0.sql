USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectYahoo_Select_State0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectYahoo_Select_State0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectYahoo_Select_State0]
AS
BEGIN

	-- Html��͂��I����Ă��Ȃ����WWeb���擾�B
	SELECT
		[SearchKeywordNo],
		[SearchDate],
		[��������Html]
	FROM [hst].[t1CollectYahoo]
	WHERE [State] = 0; 

END
GO

