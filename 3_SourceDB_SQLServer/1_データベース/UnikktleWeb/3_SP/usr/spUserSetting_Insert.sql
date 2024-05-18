USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_Insert]
	@No			bigint,
	@Gender		tinyint,
	@BirthDate	Date,
	@Career		int,
	@IPv4		varchar(20)
AS
BEGIN

	--declare	@No		bigint = 1;

	INSERT INTO [usr].[tUserSetting] (
        [No],
        --,[BackgroundColor]		-- 値はデフォルト値
        --,[ExternalSearchEngine]	-- 値はデフォルト値
		Gender,
		BirthDate,
		Career,
		IPv4
	) VALUES (
		@No,
        --,@BackgroundColor			-- 値はデフォルト値
        --,@ExternalSearchEngine	-- 値はデフォルト値
		@Gender,
		@BirthDate,
		@Career,
		@IPv4
	);

END
GO
/*
*/