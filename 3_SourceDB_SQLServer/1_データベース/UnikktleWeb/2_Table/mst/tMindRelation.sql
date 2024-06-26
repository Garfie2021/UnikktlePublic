USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 記録だけしておいて、後で全体像の把握、分析に使う
CREATE TABLE [mst].[tMindRelation](
	[MindNo]			[bigint]		NOT NULL,	-- tMind.[No] にリレーション
	[KeywordNo_Left]	[bigint]		NOT NULL,	-- [mst].[tMindKeyword].[No] にリレーション
	[KeywordNo_Right]	[bigint]		NOT NULL,	-- [mst].[tMindKeyword].[No] にリレーション
	[RelationType]		[tinyint]		NOT NULL	DEFAULT 0,	-- 0：関係する（ブランク）　0：LeftはRightに依存する（>）　1:LeftはRightに接続する（-）
CONSTRAINT [PK_mst_tMindRelation] PRIMARY KEY CLUSTERED
(
	[MindNo] ASC,
	[KeywordNo_Left] ASC,
	[KeywordNo_Right] ASC,
	[RelationType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

