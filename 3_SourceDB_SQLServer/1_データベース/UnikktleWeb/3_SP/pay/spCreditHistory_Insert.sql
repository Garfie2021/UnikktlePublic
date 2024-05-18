USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[pay].[spCreditHistory_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [pay].[spCreditHistory_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [pay].[spCreditHistory_Insert]
	--@Email			varchar(256),
	@UserNo			bigint,
	@PayPalIpnNo	bigint,
	@RegisteredDate	datetime,
	@txn_id			varchar(50),
	@AddCredit		int
AS
BEGIN

	/*
	declare	@Email			varchar(256) = 'xxx@xxx.com';
	declare	@PayPalIpnNo	bigint  = 5;
	declare	@RegisteredDate	datetime  = '2019-08-25 8:38:44';
	declare	@txn_id			varchar(50)  = 'xxx';
	declare	@AddCredit		int  = 1000;
	*/

	--declare	@log			varchar(max) = @Email + '\n' + @PayPalIpnNo + '\n' + @RegisteredDate + '\n' + @txn_id + '\n' + @AddCredit;
	--EXECUTE [UnikktleCmn].[test].[spDebugLog_Insert] @Email;



	--declare	@PayPalIpnNo	bigint;
	--declare	@Credit			int;

	-- UserNo�擾
	--SELECT
	--	@UserNo = [No]
	--FROM
	--	[UnikktleWeb].[dbo].[AspNetUsers]
	--WHERE
	--	UPPER([Email]) = UPPER(@Email)
	--;

	--declare	@UserNo		bigint;	
	--SELECT
	--	@UserNo = [No]
	--FROM
	--	[dbo].[AspNetUsers]
	--WHERE
	--	UPPER([Email]) = UPPER(@Email)
	--;


	declare	@OwnedCredit_before	int;
	SELECT
		@OwnedCredit_before = [OwnedCredit]
	FROM
		[usr].[tUserSetting]
	WHERE
		[No] = @UserNo
	;

	-- �w���E�ԕi�����N���W�b�g�����[�U�f�[�^�ɔ��f
	--UPDATE
	--	[UnikktleWeb].[usr].[tUserSetting]
	--SET
	--	[OwnedCredit] = [OwnedCredit] + @mc_gross
	--WHERE
	--	@UserNo = [No]
	--;

	UPDATE
		[usr].[tUserSetting]
	SET
		[OwnedCredit] = [OwnedCredit] + @AddCredit
	WHERE
		[No] = @UserNo 
	;


	declare	@OwnedCredit_after	int;
	SELECT
		@OwnedCredit_after = [OwnedCredit]
	FROM
		[usr].[tUserSetting]
	WHERE
		[No] = @UserNo
	;


	-- ���O�o�^
	INSERT INTO [pay].[tCreditHistory] (
        [UserNo],
        [PayPalIpnNo],
		[RegisteredDate],
		[txn_id],
        [AddCredit],
		[OwnedCredit_before],
		[OwnedCredit_after]
    ) VALUES (
		@UserNo,
		@PayPalIpnNo,
		@RegisteredDate,
		@txn_id,
		@AddCredit,
		@OwnedCredit_before,
		@OwnedCredit_after
	);

END
GO
/*
*/