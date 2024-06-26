USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mst].[tKeyword](
	[No]							[bigint] IDENTITY(1,1)	NOT NULL,
	[CollectTargetCategory]			[tinyint]				NULL,		-- C#側 列挙型.CollectTargetCategoryの値　0：未選択　1：人手入力　2：メールマガジン　3：Google検索　4：Bing検索　5：Yahoo検索　6：WebサーバのUIから検索されたキーワード

	-- 検索エンジン
	[CollectNo]						[bigint]				NULL,		-- tCollectBing、tCollectGoogle、tCollectYahoo、各テーブルの SearchKeywordNo カラム、tCollectMail テーブルの CollectTargetNo カラムとリレーション。
	[SearchResultNo]				[tinyint]				NULL,		-- tCollectBing tCollectGoogle/tCollectYahoo テーブルの SearchResultNo カラム。
	[SendDate]						[datetime]				NULL,		-- tCollectMail テーブルの SendDate カラムとリレーション。
	[Google検索日時]				[datetime]				NULL,		-- Googleで再検索した日時
	[Bing検索日時]					[datetime]				NULL,		-- Bingで再検索した日時
	[Yahoo検索日時]					[datetime]				NULL,		-- Yahooで再検索した日時

	-- WebPage
	[DomainNo]						[bigint]				NULL,		-- [mst].[tDomain]テーブルの[No]にリレーション。
	[UrlNo]							[bigint]				NULL,		-- [mst].[tUrl]テーブルの[No]にリレーション。

	-- 共通
	[採用]							[tinyint]				NOT NULL default 0,	-- 0：不採用	1：採用
	[採用判定済み]					[tinyint]				NOT NULL default 0,	-- 0：未判定	1：判定済み
	[名詞区分]						[tinyint]				NOT NULL ,			-- 0：人手で目検　1：英語連結名詞　2：形態素解析名詞　3：形態素解析連結名詞
	[登録日時]						[datetime]				NOT NULL default getdate(),
	[更新日時]						[datetime]				NOT NULL default getdate(),
	--[Collaborate更新日時_Mail]	[datetime]				NULL,	-- Mailの協調フィルタリング計算日時
	--[Collaborate更新日時_Google]	[datetime]				NULL,	-- Googleの協調フィルタリング計算日時
	--[Collaborate更新日時_Yahoo]	[datetime]				NULL,	-- Yahooの協調フィルタリング計算日時
	--[Collaborate更新日時_Bing]	[datetime]				NULL,	-- Bingの協調フィルタリング計算日時
	[r_w]							[smallint]				NOT NULL default 0,		-- Rectの幅
	[Word]							[nvarchar](100)			COLLATE Japanese_BIN2 NOT NULL ,
	[解析元データ]					[nvarchar](max)			NULL,
CONSTRAINT [PK_tKeyword2] PRIMARY KEY CLUSTERED
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Keyword_1] ON [mst].[tKeyword]
(
	[Word] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

--INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'C#');
/* 安定稼働したら復活
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'MeCab');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'Google');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'Google API Client Library for .NET');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'SQLServer');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'Oracle');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'Linux');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'Visual Studio 2017');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'JavaScript');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'GitHub');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'オープンソース');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'OSS');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'NuGet');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'HTML');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'TypeScript');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, '生産性');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, '自動化');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'メリット');
INSERT INTO [mst].[tKeyword] ([名詞区分], [Word]) VALUES (0, 'デメリット');
*/

GO


