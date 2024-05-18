USE [Profile]
GO
/*
*/
IF OBJECT_ID(N'[test].[spDebugLog_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [test].[spDebugLog_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [test].[spDebugLog_Insert]
	@Log	varchar(max)
AS
BEGIN

	INSERT INTO [test].[tDebugLog] ([Log]) VALUES (@Log);

END
GO

