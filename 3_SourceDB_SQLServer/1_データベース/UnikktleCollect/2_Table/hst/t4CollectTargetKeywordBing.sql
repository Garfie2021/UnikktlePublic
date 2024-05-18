USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 協調フィルタリングの計算元テーブル
-- ※カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
CREATE TABLE [hst].[t4CollectTargetKeyword_Bing](
	[SearchKeywordNo]		[bigint]			NOT NULL,	-- [hst].[tCollectBing].[SearchKeywordNo]とリレーション。
	[SearchDate]			[datetime]			NOT NULL,	-- [hst].[tCollectBing].[SearchDate]とリレーション。
	[SearchResultNo]		[tinyint]			NOT NULL,	-- [hst].[tCollectBing].[SearchResultNo]とリレーション。
	[KeywordNo]				[bigint]			NOT NULL,	-- [mst].[tKeyword].[No]とリレーション。
	[登録日時]				[datetime]			NOT NULL default getdate(),
	[更新日時]				[datetime]			NOT NULL default getdate(),
CONSTRAINT [PK_hst_t4CollectTargetKeyword_Bing] PRIMARY KEY CLUSTERED
(
	[SearchKeywordNo] ASC,
	[SearchDate] ASC,
	[SearchResultNo] ASC,
	[KeywordNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO
