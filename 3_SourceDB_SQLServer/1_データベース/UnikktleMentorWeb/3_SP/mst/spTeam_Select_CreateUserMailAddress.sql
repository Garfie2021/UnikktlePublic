USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_Select_CreateUserMailAddress]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_Select_CreateUserMailAddress] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_Select_CreateUserMailAddress]
	@TeamID		nvarchar(100),
	@Email		varchar(450) OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@Email = usr.tUserSetting.Email
	FROM
		mst.tTeam INNER JOIN
		usr.tUserSetting ON mst.tTeam.CreateUserNo = usr.tUserSetting.No
	WHERE
		mst.tTeam.[TeamID] = @TeamID
	;

END
GO
/*
*/