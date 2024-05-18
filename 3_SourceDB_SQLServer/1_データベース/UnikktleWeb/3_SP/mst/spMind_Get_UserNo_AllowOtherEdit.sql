USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Get_UserNo]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Get_UserNo] ;
GO
IF OBJECT_ID(N'[mst].[spMind_Get_UserNo_AllowOtherEdit]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Get_UserNo_AllowOtherEdit] ;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMind_Get_UserNo_AllowOtherEdit]
	@No				bigint,
	@UserNo			bigint OUTPUT,
	@AllowOtherEdit	bigint OUTPUT
AS
BEGIN


	SELECT
		@UserNo = [UserNo],
		@AllowOtherEdit = [AllowOtherEdit]
	FROM
		[mst].[tMind]
	WHERE
		[No] = @No
	;

END
GO
/*
*/

