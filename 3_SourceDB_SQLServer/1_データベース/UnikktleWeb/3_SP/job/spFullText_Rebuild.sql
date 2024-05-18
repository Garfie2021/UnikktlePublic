USE [UnikktleWeb]
GO

IF OBJECT_ID(N'[job].[spFullText_Rebuild]', N'P') IS NOT NULL
	DROP PROCEDURE [job].[spFullText_Rebuild] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [job].[spFullText_Rebuild]
AS
BEGIN

	-- フルテキスト カタログ 再作成
	ALTER FULLTEXT CATALOG Word REBUILD;
	ALTER FULLTEXT CATALOG Word REORGANIZE;
	

	-- フルテキスト カタログ 再作成
	-- 「警告: テーブルまたはインデックス付きビュー 'clt.tKeyword' の作成が現在アクティブなので、このテーブルまたはインデックス付きビューでのフルテキスト インデックスの作成開始要求は無視されます。」が出力されるのでコメントアウトしてる。
	--ALTER FULLTEXT INDEX ON [clt].[tKeyword] START FULL POPULATION;

END
GO

