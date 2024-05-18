USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spDomain_GetWithInsert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spDomain_GetWithInsert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spDomain_GetWithInsert]
	@Domain	varchar(max),
	@No		bigint	output
AS
BEGIN

	declare @Cnt int = 0;
	SELECT @Cnt = count(*)
	FROM [mst].[tDomain]
	where [Domain] = @Domain;
	
	if @Cnt < 1
	begin

		INSERT INTO [mst].[tDomain] (
			[Domain]
		) VALUES (
			@Domain
		);

	end;

	SELECT @No = [No]
	FROM [mst].[tDomain]
	where [Domain] = @Domain;

END
GO
/*
*/

