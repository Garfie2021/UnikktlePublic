USE [UnikktlePayPalListen]
GO
/*
*/
IF OBJECT_ID(N'[pay].[spPayPalIPNlog_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [pay].[spPayPalIPNlog_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [pay].[spPayPalIPNlog_Insert]
	@RegisteredDate		[datetime],
	@RequestBody		[varchar](max),
	@No					bigint OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;

	INSERT INTO [pay].[tPayPalIPNlog] (
		[RegisteredDate],
        [RequestBody]
	) VALUES (
		@RegisteredDate,
		@RequestBody
	);

	SET @No = SCOPE_IDENTITY();

END
GO
/*
*/