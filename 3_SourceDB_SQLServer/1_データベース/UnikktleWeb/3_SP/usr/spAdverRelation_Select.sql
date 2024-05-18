USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverRelation_Select]
	@UserNo			bigint,
	@BusinessNo		smallint
AS
BEGIN
	--declare	@UserNo			bigint = 40;
	--declare	@BusinessNo		smallint = 1;

	SELECT
		[No] AS Id,
		[Valid],
		[Category],
		[AdverName],
		[AdvertisingBudget],
		DATEADD(DAY, 30, [�X�V����]) AS ExpirationDate -- �Ō�̍X�V����30���オ�\������
	FROM 
		[usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo
	;

END
GO
/*
*/