USE [UnikktlePayPalListen]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [pay].[tPayPalIPN](
	[No]						[bigint]		NOT NULL IDENTITY(1,1),
	[PayPalIPNlogNo]			[bigint]		NOT NULL,  -- [pay].[tPayPalIPNlog].[No] にリレーション
	[RegisteredDate]			[datetime] 		NOT NULL,  -- [pay].[tPayPalIPNlog].[RegisteredDate] にリレーション
	[mc_gross]					[varchar](50)	NOT NULL,  -- サンプルデータ > 1000
	[protection_eligibility]	[varchar](50)	NOT NULL,  -- サンプルデータ > Eligible
	[address_status]			[varchar](50)	NOT NULL,  -- サンプルデータ > confirmed
	[payer_id]					[varchar](50)	NOT NULL,  -- サンプルデータ > xxx
	[address_street]			[varchar](100)	NOT NULL,  -- サンプルデータ > xxx
	[payment_date]				[varchar](50)	NOT NULL,  -- サンプルデータ > 06:59:45 Aug 15, 2019 PDT
	[payment_status]			[varchar](50)	NOT NULL,  -- サンプルデータ > Completed
	[charset]					[varchar](50)	NOT NULL,  -- サンプルデータ > Shift_JIS
	[address_zip]				[varchar](50)	NOT NULL,  -- サンプルデータ > xxx
	[first_name]				[varchar](50)	NOT NULL,  -- サンプルデータ > test
	[option_selection1]			[varchar](50)	NOT NULL,  -- サンプルデータ > 1000 Credit
	[mc_fee]					[varchar](50)	NOT NULL,  -- サンプルデータ > 76
	[address_country_code]		[varchar](50)	NOT NULL,  -- サンプルデータ > JP
	[address_name]				[varchar](50)	NOT NULL,  -- サンプルデータ > buyer test
	[notify_version]			[varchar](50)	NOT NULL,  -- サンプルデータ > 3.9
	[custom]					[varchar](50)	NOT NULL,  -- サンプルデータ > 
	[payer_status]				[varchar](50)	NOT NULL,  -- サンプルデータ > verified
	[business]					[varchar](256)	NOT NULL,  -- サンプルデータ > xxx@xxx.com
	[address_country]			[varchar](50)	NOT NULL,  -- サンプルデータ > Japan
	[address_city]				[varchar](50)	NOT NULL,  -- サンプルデータ > xxx
	[quantity]					[varchar](50)	NOT NULL,  -- サンプルデータ > 1
	[verify_sign]				[varchar](500)	NOT NULL,  -- サンプルデータ > xxx
	[payer_email]				[varchar](256)	NOT NULL,  -- サンプルデータ > Unikktle-buyer@xxx.com
	[option_name1]				[varchar](50)	NOT NULL,  -- サンプルデータ > Credit
	[txn_id]					[varchar](50)	NOT NULL,  -- サンプルデータ > xxx
	[payment_type]				[varchar](50)	NOT NULL,  -- サンプルデータ > instant
	[last_name]					[varchar](50)	NOT NULL,  -- サンプルデータ > buyer
	[address_state]				[varchar](50)	NOT NULL,  -- サンプルデータ > xxx
	[receiver_email]			[varchar](256)	NOT NULL,  -- サンプルデータ > xxx@xxx.com
	[payment_fee]				[varchar](50)	NOT NULL,  -- サンプルデータ > 
	[shipping_discount]			[varchar](50)	NOT NULL,  -- サンプルデータ > 0
	[insurance_amount]			[varchar](50)	NOT NULL,  -- サンプルデータ > 0
	[receiver_id]				[varchar](50)	NOT NULL,  -- サンプルデータ > xxx
	[txn_type]					[varchar](50)	NOT NULL,  -- サンプルデータ > web_accept
	[item_name]					[varchar](50)	NOT NULL,  -- サンプルデータ > Credit
	[discount]					[varchar](50)	NOT NULL,  -- サンプルデータ > 0
	[mc_currency]				[varchar](50)	NOT NULL,  -- サンプルデータ > JPY
	[item_number]				[varchar](50)	NOT NULL,  -- サンプルデータ > c1
	[residence_country]			[varchar](50)	NOT NULL,  -- サンプルデータ > JP
	[test_ipn]					[varchar](50)	NOT NULL,  -- サンプルデータ > 1
	[shipping_method]			[varchar](50)	NOT NULL,  -- サンプルデータ > Default
	[transaction_subject]		[varchar](50)	NOT NULL,  -- サンプルデータ > 
	[payment_gross]				[varchar](50)	NOT NULL,  -- サンプルデータ > 
	[ipn_track_id]				[varchar](50)	NOT NULL,  -- サンプルデータ > xxx
CONSTRAINT [PK_pay_tPayPalIPN] PRIMARY KEY CLUSTERED
(
	[No] ASC,
	[PayPalIPNlogNo] ASC,
	[Receiver_email] ASC,
	[Txn_id] ASC,
	[Payment_status] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

