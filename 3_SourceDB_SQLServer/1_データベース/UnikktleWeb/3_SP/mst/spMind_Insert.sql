USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMind_Insert]
	@UserNo					bigint,
	@Title					ntext,
	@Explanation			ntext,
	@Item_SpaceSeparator	ntext,
	@ItemRelation			ntext,
	@PublishOnlyToMe		bit,
	@AllowOtherEdit			bit,
	@JsonViewModel			ntext,
	@MindNo					bigint OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;

	INSERT INTO [mst].[tMind] (
		UserNo,
		Title,
		Explanation,
		Item_SpaceSeparator,
		ItemRelation,
		PublishOnlyToMe,
		AllowOtherEdit,
		JsonViewModel,
		[LastUpdate]
	) VALUES (
		@UserNo,
		@Title,
		@Explanation,
		@Item_SpaceSeparator,
		@ItemRelation,
		@PublishOnlyToMe,
		@AllowOtherEdit,
		@JsonViewModel,
		GETDATE()
	);

	SET @MindNo = SCOPE_IDENTITY();
	
END
GO
/*
*/