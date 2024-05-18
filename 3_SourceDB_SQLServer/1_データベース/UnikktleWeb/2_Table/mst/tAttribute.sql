USE [UnikktleWeb]
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
CONSTRAINT [PK_mst_tAttribute] PRIMARY KEY CLUSTERED
(
	[Class] ASC,
	[AttributeNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,1,10,'医療');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,2,20,'美容・ファッション');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,3,20,'旅行・ホテル');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,4,1,'飲食');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,5,1,'教育・保育');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,6,1,'自然・動物');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,7,10,'運輸・乗り物');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,8,20,'出版・報道');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,9,20,'インターネットメディア');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,10,20,'テレビ・映画');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,11,20,'音楽・ラジオ');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,12,20,'芸能');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,13,20,'スポーツ');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,14,20,'マンガ・アニメ・ゲーム');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,15,20,'広告・デザイン・アート');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,16,10,'コンピューター');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,17,10,'保安');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,18,10,'軍事');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,19,10,'法律');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,20,10,'国際');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,21,10,'金融');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,22,10,'建築');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,23,10,'販売');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,24,10,'オフィス');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,25,10,'エネルギー');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,26,10,'素材');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,27,10,'電気・機械');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,28,10,'企業');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,29,10,'公務');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,30,20,'葬祭・宗教');
INSERT INTO [UnikktleWeb].[mst].[tAttribute] ([Class],[AttributeNo],[OrderNo],[AttributeName]) VALUES (1,31,100,'その他');
