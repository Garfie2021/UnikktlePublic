USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearch_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearch_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverSearch_Select]
	@UserNo			bigint,
	@BusinessNo		smallint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] AS Id,
		[Valid],
		[Category],
		[AdverName],
		[AdvertisingBudget],
		DATEADD(DAY, 30, [�X�V����]) AS ExpirationDate -- �Ō�̍X�V����30���オ�\������
	FROM 
		[usr].[tAdverSearch]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo
	;

END
GO
/*
*/