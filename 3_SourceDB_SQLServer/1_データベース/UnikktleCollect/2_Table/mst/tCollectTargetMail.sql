USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mst].[tCollectTargetMail](
	[No]				[bigint]  IDENTITY(1,1)	NOT NULL,
	[Category]			[int]					NULL,		-- [mst].[tCategory].[No]にリレーション　※当面は手作業でメンテナンスする。
	[名称]				[nvarchar](50)			NULL,
	[From_MailAddress]	[nvarchar](200)			NULL,
	[登録日時]			[datetime]				NULL default getdate(),
	[更新日時]			[datetime]				NULL default getdate(),
CONSTRAINT [PK_tCollectTargetMail2] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE UNIQUE NONCLUSTERED INDEX [IX_CollectTargetMail_1_2] ON [mst].[tCollectTargetMail]
(
	[From_MailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO


INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'', 'infomail@japan.cnet.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'SHOEISHA iD事務局', 'shoeishaid_support@shoeisha.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'TechTargetジャパン/キーマンズネット編集部', 'ttkn-tgmail@noreply.itmedia.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'TechTargetジャパン編集部', 'tt-membership@noreply.itmedia.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'まぐまぐニュース！', 'magnews@mag2official.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'CNET Japan 編集部', 'newsletter@japan.cnet.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'【翔泳社の通販】SEshop', 'contactus@seshop.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'TechTargetジャパン編集部', 'ttkn-wpdl@noreply.itmedia.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'＠IT新着速報', 'noreply_newarrivals@atmarkit.co.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'ZDNet Japan編集部', 'newsletter@japan.zdnet.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'CodeZine編集部', 'support@codezine.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'TRiP EDiTOR編集部', 'tripeditor@mag2official.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'mag2 0000135444', 'mailmag@mag2tegami.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'マネーボイス', 'money@mag2official.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'Qiita[キータ]', 'info@qiita.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'アイティメディア・メールサービス事務局', 'itmedia-mail-service@itmedia.co.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'＠IT通信 [夕刊] Special', 'atmarkit-mail@noreply.itmedia.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'翔泳社 Book News', 'sebn_support@shoeisha.co.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'TechRepublic Japan編集部', 'no-reply@mail.japan.techrepublic.com');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'朝日インタラクティブ株式会社', 'mail-info-inq@aiasahi.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'＠IT自分戦略研究所Weekly', 'noreply_jibun@atmarkit.co.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'＠IT通信', 'noreply_communication@atmarkit.co.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'＠IT通信 [朝刊] Special', 'noreply_morning@atmarkit.co.jp');
INSERT INTO [mst].[tCollectTargetMail] ([Category], [名称], [From_MailAddress]) VALUES (1,'TechTargetジャパン', 'tt-contents@tt.itmedia.co.jp');


