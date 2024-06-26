USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mst].[tAttribute](
	[Class]				[int] 			NOT NULL,	-- 1:CareerCategory
	[AttributeNo]		[int] 			NOT NULL,	-- [tCareer]テーブル[CareerCategoryNo]列にリレーション。
	[OrderNo]			[int] 			NOT NULL,	-- ソート順。
	[AttributeName]		[nvarchar](50)	NOT NULL,
	[AttributeName_JA]	[nvarchar](50)	NOT NULL,
CONSTRAINT [PK_mst_tAttribute] PRIMARY KEY CLUSTERED
(
	[Class] ASC,
	[AttributeNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,1,10,'Medical','医療');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,2,20,'Beauty/fashion','美容・ファッション');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,3,20,'Travel/Hotel','旅行・ホテル');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,4,1,'Food and drink','飲食');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,5,1,'Education/childcare','教育・保育');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,6,1,'Nature/animal','自然・動物');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,7,10,'Transportation/Vehicles','運輸・乗り物');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,8,20,'Publishing/reporting','出版・報道');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,9,20,'Internet media','インターネットメディア');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,10,20,'TV/Movie','テレビ・映画');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,11,20,'Music/Radio','音楽・ラジオ');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,12,20,'Entertainment','芸能');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,13,20,'Sports','スポーツ');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,14,20,'Manga/Anime/Game','マンガ・アニメ・ゲーム');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,15,20,'Advertising/Design/Art','広告・デザイン・アート');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,16,10,'Computer','コンピューター');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,17,10,'Security','保安');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,18,10,'Military','軍事');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,19,10,'Law','法律');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,20,10,'International','国際');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,21,10,'Finance','金融');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,22,10,'Architecture','建築');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,23,10,'Sale','販売');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,24,10,'Office','オフィス');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,25,10,'Energy','エネルギー');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,26,10,'Material','素材');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,27,10,'Electricity/machine','電気・機械');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,28,10,'Company','企業');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,29,10,'Public affairs','公務');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,30,20,'Funeral and religion','葬祭・宗教');
INSERT INTO [mst].[tAttribute] ([Class], [AttributeNo], [OrderNo], [AttributeName], [AttributeName_JA]) VALUES (1,31,100,'Other','その他');
