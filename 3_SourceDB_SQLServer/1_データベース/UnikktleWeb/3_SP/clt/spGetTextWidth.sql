USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spGetTextWidth]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spGetTextWidth] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spGetTextWidth]
	@Word	nvarchar(100),
	@Width	smallint	OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;
	--declare	@Word	[nvarchar](100);

	Set @Width = UnikktleCmn.calc.fTextWidth(@Word);

	--SELECT @Width;

END
GO
/*
*/