USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Update]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Update] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMind_Update]
	@No						bigint,
	@Title					ntext,
	@Explanation			ntext,
	@Item_SpaceSeparator	ntext,
	@ItemRelation			ntext,
	@PublishOnlyToMe		bit,
	@AllowOtherEdit			bit,
	@JsonViewModel			ntext
AS
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[mst].[tMind]
	SET
		[Title] = @Title,
		[Explanation] = @Explanation,
		[Item_SpaceSeparator] = @Item_SpaceSeparator,
		[ItemRelation] = @ItemRelation,
		[PublishOnlyToMe] = @PublishOnlyToMe,
		[AllowOtherEdit] = @AllowOtherEdit,
		[JsonViewModel] = @JsonViewModel,
		[LastUpdate] = GETDATE()
	WHERE
		[No] = @No
	;

END
GO
/*
*/