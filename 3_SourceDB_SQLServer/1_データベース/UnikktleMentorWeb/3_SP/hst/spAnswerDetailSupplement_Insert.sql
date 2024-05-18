USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerDetailSupplement_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerDetailSupplement_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [hst].[spAnswerDetailSupplement_Insert]
	@UserNo				bigint,
	@AnswerId			int,
	@Gender				tinyint,
	@Career				int,
	@RecentHappenings	ntext
AS
BEGIN

	--declare	@No		bigint = 1;

	INSERT INTO [hst].[tAnswerDetailSupplement] (
		[UserNo],
		[AnswerId],
		[Gender],
		[Career],
		[RecentHappenings]
    ) VALUES (
		@UserNo,
		@AnswerId,
		@Gender,
		@Career,
		@RecentHappenings
	);

END
GO
/*
*/