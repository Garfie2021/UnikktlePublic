USE UnikktleWebCollectWork
GO

IF OBJECT_ID(N'[mnt].[spIndexes_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [mnt].[spIndexes_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mnt].[spIndexes_Select]
AS
BEGIN

	-- このSQLは、接続しているデータベースに完全依存するので、共通するのは不可能だった。
	
	SELECT
		DB_NAME(A.database_id) AS DatabaseName, 
		OBJECT_SCHEMA_NAME(A.object_id) AS SchemaName, 
		object_name(A.object_id) AS TableName, 
		B.name AS IndedxName, 
		avg_fragmentation_in_percent AS Fragmentation
	FROM
		sys.dm_db_index_physical_stats(DB_ID('UnikktleWebCollectWork'), NULL, NULL, NULL, NULL) AS A
		INNER JOIN
		sys.indexes AS B
			ON A.object_id = B.object_id
			AND A.index_id = B.index_id
	WHERE
		avg_fragmentation_in_percent > 5 AND
		B.name is not null

	
END
GO

