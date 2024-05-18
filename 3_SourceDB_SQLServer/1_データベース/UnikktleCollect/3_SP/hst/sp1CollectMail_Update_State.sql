USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectMail_Update_State]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectMail_Update_State] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectMail_Update_State]
	@CollectTargetNo	bigint,
	@SendDate			datetime,
	@“o˜^“úŽž			datetime,
	@State				tinyint
AS
BEGIN


	UPDATE [hst].[t1CollectMail]
	SET
		[State] = @State
	WHERE
		[CollectTargetNo] = @CollectTargetNo AND
		[SendDate] = @SendDate AND
		[“o˜^“úŽž] = @“o˜^“úŽž;

END
GO

