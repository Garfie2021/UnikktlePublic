USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Select_Author]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Select_Author] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- そのユーザが所有しているMindリスト
CREATE PROCEDURE [mst].[spMind_Select_Author]
	@UserNo		bigint
AS
BEGIN

	SELECT
		[No] as Id,
		[Title],
		[Explanation],
		[PublishOnlyToMe],
		[AllowOtherEdit],
		[LastUpdate]
	FROM
		[mst].[tMind]
	WHERE
		[UserNo] = @UserNo
	;

END
GO
/*
*/