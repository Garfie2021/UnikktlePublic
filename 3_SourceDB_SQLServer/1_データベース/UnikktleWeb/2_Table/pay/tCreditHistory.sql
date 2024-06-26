USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- クレジット購入履歴
CREATE TABLE [pay].[tCreditHistory](
	[No]					[bigint]		NOT NULL	IDENTITY(1,1),
	[UserNo]				[bigint] 		NOT NULL,	-- [usr].[tUserSetting].[No]列にリレーション
	[PayPalIpnNo]			[bigint] 		NOT NULL,	-- [pay].[tPayPalIPN].[No] にリレーション
	[RegisteredDate]		[datetime] 		NOT NULL,	-- [pay].[tPayPalIPNlog].[RegisteredDate] にリレーション
	[txn_id]				[varchar](50)	NOT NULL,   -- [pay].[tPayPalIPN].[txn_id] にリレーション
	[AddCredit]				[int] 			NOT NULL,	-- 購入クレジット。 [pay].[tPayPalIPN].[mc_gross] 同じ値。
	[OwnedCredit_before]	[int] 			NOT NULL,	-- 変更前の保有クレジット。 [usr].[tUserSetting].[OwnedCredit] 同じ値。
	[OwnedCredit_after]		[int] 			NOT NULL,	-- 変更後の保有クレジット。 [usr].[tUserSetting].[OwnedCredit] 同じ値。
CONSTRAINT [PK_pay_tCreditHistory] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[PayPalIpnNo] ASC,
	[RegisteredDate] ASC,
	[txn_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

