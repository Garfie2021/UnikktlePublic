SET ANSI_PADDING ON
GO

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractBing_6_76631416__K7] ON [hst].[t3ExtractBing]
(
	[MeCabState] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractBing_6_76631416__K4_K7] ON [hst].[t3ExtractBing]
(
	[関連キーワード] ASC,
	[MeCabState] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractBing_6_76631416__K8_K7_K4_1_2_3_12] ON [hst].[t3ExtractBing]
(
	[言語判定] ASC,
	[MeCabState] ASC,
	[関連キーワード] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[SearchResultNo],
	[英語連結名詞除外後]
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_76631416_7_8_4] ON [hst].[t3ExtractBing]([MeCabState], [言語判定], [関連キーワード])
GO



CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractBing_6_76631416__K7_K8_K4_1_2_3_12] ON [hst].[t3ExtractBing]
(
	[MeCabState] ASC,
	[言語判定] ASC,
	[関連キーワード] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[SearchResultNo],
	[英語連結名詞除外後]
) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_76631416_4_8] ON [hst].[t3ExtractBing]([関連キーワード], [言語判定])
GO



CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractBing_6_76631416__K4_K8_K7_1_2_3_12] ON [hst].[t3ExtractBing]
(
	[関連キーワード] ASC,
	[言語判定] ASC,
	[MeCabState] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[SearchResultNo],
	[英語連結名詞除外後]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractBing_6_76631416__K8_K7_K4] ON [hst].[t3ExtractBing]
(
	[言語判定] ASC,
	[MeCabState] ASC,
	[関連キーワード] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO



CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractGoogle_6_2128114722__K7_K8_K4_1_2_3_12] ON [hst].[t3ExtractGoogle]
(
	[MeCabState] ASC,
	[言語判定] ASC,
	[関連キーワード] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[SearchResultNo],
	[英語連結名詞除外後]
) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_2128114722_4_7_8] ON [hst].[t3ExtractGoogle]([関連キーワード], [MeCabState], [言語判定])
GO


CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractGoogle_6_2128114722__K7] ON [hst].[t3ExtractGoogle]
(
	[MeCabState] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractGoogle_6_2128114722__K4_K7_K8] ON [hst].[t3ExtractGoogle]
(
	[関連キーワード] ASC,
	[MeCabState] ASC,
	[言語判定] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO


CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractMail_6_1148635235__K5_K6_1_2_3_9] ON [hst].[t3ExtractMail]
(
	[MeCabState] ASC,
	[言語判定] ASC
)
INCLUDE (
 	[CollectTargetNo],
	[SendDate],
	[登録日時],
	[英語連結名詞除外後]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO


CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractYahoo_6_1052634893__K7] ON [hst].[t3ExtractYahoo]
(
	[MeCabState] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractYahoo_6_1052634893__K4_K7] ON [hst].[t3ExtractYahoo]
(
	[関連キーワード] ASC,
	[MeCabState] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractYahoo_6_1052634893__K8_K7_K4_1_2_3_12] ON [hst].[t3ExtractYahoo]
(
	[言語判定] ASC,
	[MeCabState] ASC,
	[関連キーワード] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[SearchResultNo],
	[英語連結名詞除外後]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_1052634893_4_7_8] ON [hst].[t3ExtractYahoo]([関連キーワード], [MeCabState], [言語判定])
GO



CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractYahoo_6_1052634893__K7_K8_K4_1_2_3_12] ON [hst].[t3ExtractYahoo]
(
	[MeCabState] ASC,
	[言語判定] ASC,
	[関連キーワード] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[SearchResultNo],
	[英語連結名詞除外後]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_1052634893_4_8] ON [hst].[t3ExtractYahoo]([関連キーワード], [言語判定])
GO
 

CREATE NONCLUSTERED INDEX [_dta_index_t3ExtractYahoo_6_1052634893__K4_K8_K7_1_2_3_12] ON [hst].[t3ExtractYahoo]
(
	[関連キーワード] ASC,
	[言語判定] ASC,
	[MeCabState] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[SearchResultNo],
	[英語連結名詞除外後]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO





CREATE NONCLUSTERED INDEX [_dta_index_tKeyword_6_2032114380__K14_K1_2_3_4_5_6_7_8_9_10_11_12_13_15_16] ON [mst].[tKeyword]
(
	[Word] ASC,
	[No] ASC
)
INCLUDE (
 	[CollectTargetCategory],
	[CollectNo],
	[SearchResultNo],
	[SendDate],
	[採用],
	[採用判定済み],
	[名詞区分],
	[登録日時],
	[更新日時],
	[Google検索日時],
	[Bing検索日時],
	[Yahoo検索日時],
	[解析元データ],
	[r_w]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_2032114380_1_14] ON [mst].[tKeyword]([No], [Word])
GO

CREATE STATISTICS [_dta_stat_2032114380_8_14] ON [mst].[tKeyword]([名詞区分], [Word])
GO


CREATE NONCLUSTERED INDEX [_dta_index_tKeyword_6_2032114380__K10_1_14_15_16] ON [mst].[tKeyword]
(
	[更新日時] ASC
)
INCLUDE (
 	[No],
	[Word],
	[解析元データ],
	[r_w]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO


CREATE NONCLUSTERED INDEX [_dta_index_tKeyword_6_2032114380__K12_K11_K13_K1_K6_K7_14] ON [mst].[tKeyword]
(
	[Bing検索日時] ASC,
	[Google検索日時] ASC,
	[Yahoo検索日時] ASC,
	[No] ASC,
	[採用] ASC,
	[採用判定済み] ASC
)
INCLUDE (
 	[Word]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_2032114380_12_6_7_11_13_1] ON [mst].[tKeyword]([Bing検索日時], [採用], [採用判定済み], [Google検索日時], [Yahoo検索日時], [No])
GO

CREATE STATISTICS [_dta_stat_2032114380_7_6] ON [mst].[tKeyword]([採用判定済み], [採用])
GO


CREATE NONCLUSTERED INDEX [_dta_index_tKeyword_6_2032114380__K11_K12_K13_K2D_K1D_K6_K7_14] ON [mst].[tKeyword]
(
	[Google検索日時] ASC,
	[Bing検索日時] ASC,
	[Yahoo検索日時] ASC,
	[CollectTargetCategory] DESC,
	[No] DESC,
	[採用] ASC,
	[採用判定済み] ASC
)
INCLUDE (
 	[Word]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_2032114380_11_6_7_12_13_2] ON [mst].[tKeyword]([Google検索日時], [採用], [採用判定済み], [Bing検索日時], [Yahoo検索日時], [CollectTargetCategory])
GO


CREATE NONCLUSTERED INDEX [_dta_index_t4CollectTargetKeyword_Bing_6_988634665__K1_K4] ON [hst].[t4CollectTargetKeyword_Bing]
(
	[SearchKeywordNo] ASC,
	[KeywordNo] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [_dta_index_t4CollectTargetKeyword_Google_6_924634437__K1_K4] ON [hst].[t4CollectTargetKeyword_Google]
(
	[SearchKeywordNo] ASC,
	[KeywordNo] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO


CREATE NONCLUSTERED INDEX [_dta_index_t4CollectTargetKeyword_Yahoo_6_796633981__K1_K4] ON [hst].[t4CollectTargetKeyword_Yahoo]
(
	[SearchKeywordNo] ASC,
	[KeywordNo] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO



CREATE NONCLUSTERED INDEX [_dta_index_t2HtmlParseYahoo_6_172631758__K3_1_2_5] ON [hst].[t2HtmlParseYahoo]
(
	[State] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[HtmlTag除外後2段階目]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO


CREATE NONCLUSTERED INDEX [_dta_index_t2HtmlParseGoogle_6_220631929__K3_1_2_5] ON [hst].[t2HtmlParseGoogle]
(
	[State] ASC
)
INCLUDE (
 	[SearchKeywordNo],
	[SearchDate],
	[HtmlTag除外後2段階目]) 
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO
