USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spFeedback_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spFeedback_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spFeedback_Insert]
	@UserNo		bigint,
	@Category	tinyint,
	@Subject	[nvarchar](100),
	@Text		[nvarchar](500)
AS
BEGIN

	--declare	@No		bigint = 1;

	INSERT INTO [usr].[tFeedback] (
        [UserNo],
        [No],
        [Category],
        [Subject],
        [Text]
    ) VALUES (
        @UserNo,
        (
			SELECT 	
				CASE
				  WHEN COUNT([No])=0 THEN 1
				  ELSE MAX([No])+1
				END
			FROM
				[usr].[tFeedback]
			where
				[UserNo] = @UserNo
		),
        @Category,
        @Subject,
        @Text
	);

END
GO
/*
*/