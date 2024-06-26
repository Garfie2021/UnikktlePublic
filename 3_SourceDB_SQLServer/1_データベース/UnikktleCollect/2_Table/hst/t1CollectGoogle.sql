USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
CREATE TABLE [hst].[t1CollectGoogle](
	[SearchKeywordNo]		[bigint]			NOT NULL,	-- [mst].[tKeyword].[No]とリレーション
	[SearchDate]			[datetime]			NOT NULL,
	[State]					[tinyint]			NOT NULL default 0,	-- HTML切り出し済みフラグ。	0：未解析　1：解析済み
	[検索結果Html]			[nvarchar](max)		NULL,	-- ディスク容量を圧迫してきたら、この列のデータを削除する
	[HtmlParseResult]		[tinyint]			NULL,	-- null：未解析　0：解析失敗　1：解析成功
	[UrlState]				[tinyint]			NOT NULL default 0,	-- 0RL切り出し済みフラグ。	0：未解析　1：解析済み
CONSTRAINT [PK_hst_t1CollectGoogle] PRIMARY KEY CLUSTERED
(
	[SearchKeywordNo] ASC,
	[SearchDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO

