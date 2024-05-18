USE [UnikktlePayPalListen]
GO
/*
*/
IF OBJECT_ID(N'[pay].[spPayPalIPN_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [pay].[spPayPalIPN_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [pay].[spPayPalIPN_GetCount]
	@txn_id					[varchar](50),
	@payer_email			[varchar](256),
	@Cnt					int OUTPUT
AS
BEGIN

	SELECT
		@Cnt = COUNT(*)
	FROM
		[pay].[tPayPalIPN]
	WHERE
		[txn_id] = @txn_id AND
		[payer_email] = @payer_email
	;

END
GO
/*
*/