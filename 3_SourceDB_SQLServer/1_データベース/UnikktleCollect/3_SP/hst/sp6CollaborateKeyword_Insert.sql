USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp6CollaborateKeyword_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp6CollaborateKeyword_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp6CollaborateKeyword_Insert]
AS
BEGIN

	SET NOCOUNT ON;

		-- 古いデータは削除
		DELETE FROM [hst].[t6CollaborateKeyword];


		-- キーワードの新しい関連をInsert
		INSERT INTO [hst].[t6CollaborateKeyword]
		(
			[KeywordNo_元],
			[KeywordNo_先],
			[同時出現ドキュメント数]
		)
		SELECT
			[KeywordNo_元],
			[KeywordNo_先],
			SUM(同時出現ドキュメント数) as 同時出現ドキュメント数
		FROM (
			-- [同時出現ドキュメント数]は元と先をマージする為に、それぞれのテーブルを2回Selectしている。
			-- SELECT * FROM [UnikktleCollect].[hst].[t6CollaborateKeyword] where [KeywordNo_元] = 1 and [KeywordNo_先] = 1454; => 同時出現ドキュメント数：1　=> マージ後：3
			-- SELECT * FROM [UnikktleCollect].[hst].[t6CollaborateKeyword] where [KeywordNo_元] = 1454 and [KeywordNo_先] = 1; => 同時出現ドキュメント数：2　=> マージ後：3

			SELECT
				[KeywordNo_元],
				[KeywordNo_先],
				1 as [同時出現ドキュメント数]	-- メールマガジンは広告が多いので、[同時出現ドキュメント数]は1固定。
			FROM
				[hst].[t5CollaborateKeyword_Mail]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]
			UNION ALL
			SELECT
				[KeywordNo_先] AS KeywordNo_元,
				[KeywordNo_元] AS KeywordNo_先,
				1 as [同時出現ドキュメント数]	-- メールマガジンは広告が多いので、[同時出現ドキュメント数]は1固定。
			FROM
				[hst].[t5CollaborateKeyword_Mail]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]

			UNION ALL

			SELECT
				[KeywordNo_元],
				[KeywordNo_先],
				[同時出現ドキュメント数]
			FROM
				[hst].[t5CollaborateKeyword_Google]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]
			UNION ALL
			SELECT
				[KeywordNo_先] AS KeywordNo_元,
				[KeywordNo_元] AS KeywordNo_先,
				[同時出現ドキュメント数]
			FROM
				[hst].[t5CollaborateKeyword_Google]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]

			UNION ALL

			SELECT
				[KeywordNo_元],
				[KeywordNo_先],
				[同時出現ドキュメント数]
			FROM
				[hst].[t5CollaborateKeyword_Bing]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]
			UNION ALL
			SELECT
				[KeywordNo_先] AS KeywordNo_元,
				[KeywordNo_元] AS KeywordNo_先,
				[同時出現ドキュメント数]
			FROM
				[hst].[t5CollaborateKeyword_Bing]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]

			UNION ALL

			SELECT
				[KeywordNo_元],
				[KeywordNo_先],
				[同時出現ドキュメント数]
			FROM
				[hst].[t5CollaborateKeyword_Yahoo]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]
			UNION ALL
			SELECT
				[KeywordNo_先] AS KeywordNo_元,
				[KeywordNo_元] AS KeywordNo_先,
				[同時出現ドキュメント数]
			FROM
				[hst].[t5CollaborateKeyword_Yahoo]
			WHERE
				[KeywordNo_元] <> [KeywordNo_先]
		) AS t
		GROUP BY
			[KeywordNo_元],
			[KeywordNo_先]
		;



END
GO
/*
*/

