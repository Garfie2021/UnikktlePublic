USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- メールは1つに複数件含まれることは無いので、WEB検索のようにtExtract〇〇テーブルは設けない
CREATE TABLE [hst].[t1CollectMail](
	-- カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
	[CollectTargetNo]			[bigint]		NOT NULL,	-- [mst].[tCollectTarget].[No]とリレーション。　※旧「FromMailAddressNo」
	[SendDate]					[datetime]		NOT NULL,
	[登録日時]					[datetime]		NOT NULL default getdate(),
	[更新日時]					[datetime]		NOT NULL default getdate(),
	[State]						[tinyint]		NOT NULL default 0,	-- 0：未解析　1：解析済み
	[FromDisplayName]			[nvarchar](200)	NOT NULL,
	[CurrentMessageID]			[nvarchar](200)	NOT NULL,
	[CurrentSubject]			[nvarchar](max)	NULL,
	[CurrentBody]				[nvarchar](max)	NULL,
CONSTRAINT [PK_hst_t1CollectMail] PRIMARY KEY CLUSTERED
(
	[CollectTargetNo] ASC,
	[SendDate] ASC,
	[登録日時] ASC	-- 登録日時もプライマリキーに含め、BulkCopyで同じメールが複数あった場合、BulkCopyが落ちる問題に対処。
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO

