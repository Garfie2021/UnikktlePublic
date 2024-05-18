USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_SelectNo]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_SelectNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMind_SelectNo]
	@No		bigint
AS
BEGIN

	SELECT 
		[No] as Id,
		[UserNo],
		[Title],
		[Explanation],
		[Item_SpaceSeparator],
		[ItemRelation],
		[PublishOnlyToMe],
		[AllowOtherEdit],
		[LastUpdate]
	FROM
		[mst].[tMind]
	WHERE
		[No] = @No
	;

END
GO
/*
*/

