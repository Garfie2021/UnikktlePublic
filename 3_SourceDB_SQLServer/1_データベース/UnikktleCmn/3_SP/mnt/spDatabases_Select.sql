USE UnikktleCmn
GO

IF OBJECT_ID(N'[mnt].[spDatabases_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [mnt].[spDatabases_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mnt].[spDatabases_Select]
AS
BEGIN
	
	SELECT
		name
	FROM
		sys.databases 
	WHERE
		name in ('UnikktleCmn', 'UnikktleCollect', 'UnikktlePayPalListen', 'UnikktleWeb', 'UnikktleWebCollectWork')
	;

END
GO

