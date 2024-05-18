USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerSupplement_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerSupplement_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [hst].[spAnswerSupplement_Insert]
	@UserNo				bigint,
	@Now				datetime,
	@RecentHappenings	ntext
AS
BEGIN

	--declare	@No		bigint = 1;

	INSERT INTO [hst].[tAnswerSupplement] (
           [UserNo],
           [AnswerDate],
           [RecentHappenings]
    ) VALUES (
           @UserNo,
		   @Now,
		   @RecentHappenings
	);

END
GO
/*
*/