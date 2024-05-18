USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectMail_Select_State0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectMail_Select_State0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectMail_Select_State0]
AS
BEGIN

	-- –¢‰ğÍ‚ÌGoogleŒŸõŒ‹‰Ê‚ğæ“¾
	SELECT
		[CollectTargetNo],
		[SendDate],
		[“o˜^“ú],
		[CurrentSubject],
		[CurrentBody]
	FROM [hst].[t1CollectMail]
	WHERE [State] = 0; 

END
GO

