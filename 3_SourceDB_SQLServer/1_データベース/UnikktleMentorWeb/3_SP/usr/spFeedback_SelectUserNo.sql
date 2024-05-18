USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spFeedback_SelectUserNo]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spFeedback_SelectUserNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spFeedback_SelectUserNo]
	@UserNo		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] AS Id,
		[Category],
		[Subject],
		[Text],
		[“o˜^“úŽž] AS SendDate
	FROM
		[usr].[tFeedback]
	WHERE
		[UserNo] = @UserNo
	;


END
GO
/*
*/