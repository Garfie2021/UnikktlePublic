USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Update_無料広告期限切れ]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Update_無料広告期限切れ] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
-- 有効期限切れの無料広告を無効化	
CREATE PROCEDURE [usr].[spAdverRelation_Update_無料広告期限切れ]
AS
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tAdverRelation]
	SET
		[Valid] = 0,	-- 無効=0
		[更新日時] = GETDATE()
	WHERE
		[Category] = 1 AND							-- 無料=1
		DATEDIFF(DAY, [更新日時], GETDATE()) > 30	-- 無料広告の有効期限は30日
	;

END
GO
/*
*/