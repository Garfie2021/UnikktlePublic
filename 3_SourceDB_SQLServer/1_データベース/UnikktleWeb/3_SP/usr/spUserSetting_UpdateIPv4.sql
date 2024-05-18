USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_UpdateIPv4]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_UpdateIPv4] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_UpdateIPv4]
	@No			bigint,
	@IPv4		varchar(20)
AS
BEGIN

	--declare	@No		bigint = 1;
	declare	@IPv4_old	[nvarchar](20);

	Select 
		@IPv4_old = [IPv4]
	From
		[usr].[tUserSetting]
	WHERE
		[No] = @No 
	;

	IF @IPv4 <> @IPv4_old
	BEGIN
		UPDATE
			[usr].[tUserSetting]
		SET
			[IPv4] = @IPv4
		WHERE
			[No] = @No 
		;
	END;

END
GO
/*
*/