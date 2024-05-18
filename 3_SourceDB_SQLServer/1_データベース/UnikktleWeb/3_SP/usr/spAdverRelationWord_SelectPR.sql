USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelationWord_SelectPR]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelationWord_SelectPR] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �֘A���[�h�ɘA������L��������
-- ��������P��(CompetingUnitPrices)
CREATE PROCEDURE [usr].[spAdverRelationWord_SelectPR]
	@WordNo				bigint,
	@ClickUserNo		bigint,
	@ClickUserIP		varchar(20),
	@RowNum				int
AS
BEGIN

	--declare	@WordNo				bigint = 115170;
	--declare	@ClickUserNo		bigint = 40;
	--declare	@ClickUserIP		varchar(20) = '0.0.0.1';
	--declare	@ClickUserSessionId	varchar(50) = '05d80aa6-7258-e622-a08a-c8ace2fad22b';
	--declare	@PageNum			bigint = 0;

	SELECT
		1 as Id,
		UserNo, 
		BusinessNo, 
		AdverNo, 
		AdverTitle1,
		AdverTitle2,
		AdverTitle_r_w,
		AdverURL
	FROM (
		SELECT
			ROW_NUMBER() OVER (ORDER BY A.Category DESC, B.ClickCost DESC) AS RowNumber,
			A.UserNo as UserNo,
			A.BusinessNo as BusinessNo,
			A.[No] as AdverNo,
			A.AdverTitle1 as AdverTitle1,
			A.AdverTitle2 as AdverTitle2,
			A.AdverTitle_r_w as AdverTitle_r_w,
			A.AdverURL as AdverURL
		FROM
			--usr.tAdverRelation AS A INNER JOIN usr.tAdverRelationWord AS B ON
			--	A.UserNo = B.UserNo AND 
			--	A.BusinessNo = B.BusinessNo AND 
			--	A.[No] = B.AdverNo 

			-- �ŏ��ɑS�s������������A�i�荞�񂾌��ʂ�����������������͂��B
			(
				SELECT
					UserNo,
					BusinessNo,
					[No],
					Category,
					AdverTitle1,
					AdverTitle2,
					AdverTitle_r_w,
					AdverURL
				FROM
					usr.tAdverRelation
				WHERE
					-- �L��=1
					-- Credit���؂ꂽ���[�U�[�̍L���͕ʏ����Ŗ����ɂ����B
					-- �L�������؂�i30�� Over�j�̍L���͕ʏ����Ŗ����ɂ����B
					[Valid] = 1
			) AS A
			INNER JOIN 
			(
				SELECT
					UserNo,
					BusinessNo,
					AdverNo,
					ClickCost
				FROM
					usr.tAdverRelationWord
				WHERE
					RelationWordNo = @WordNo
			) AS B
			ON
				A.UserNo = B.UserNo AND 
				A.BusinessNo = B.BusinessNo AND 
				A.[No] = B.AdverNo 
			LEFT JOIN		-- LEFT OUTER JOIN �� LEFT JOIN �͓������́B
			(
				SELECT
					UserNo,
					BusinessNo,
					AdverNo,
					ClickDate
				FROM
					usr.tAdverRelationClickHistory 
				WHERE
					WordNo = @WordNo AND
					-- 1�����O�̃N���b�N�����͑ΏۊO�B
					-- 1���ȓ��ɃN���b�N���ꂽPR�͕\�����Ȃ����ƂŁA�����L������ɕ\���������ɑΏ��B
					ClickDate > DATEADD(DAY, -1, GETDATE()) AND
					-- ���O�C�����[�U�[��UserNo�����Ŕ��肵�������ǂ��Ƃ��A�ׂ������Ƃ͋C�ɂ��Ȃ��B�p�t�H�[�}���X�D��B
					[ClickUserNo] = @ClickUserNo AND
					[ClickUserIP] = @ClickUserIP
					-- AND [ClickUserSessionId] = @ClickUserSessionId  �Z�b�V����ID�̕ύX�͕p�ɂɋN����̂ŏ����ɉ����Ȃ��B
			) AS C
			ON
				B.UserNo = C.UserNo AND
				B.BusinessNo = C.BusinessNo AND
				B.AdverNo = C.AdverNo
				--B.RelationWordNo = C.WordNo
		WHERE
			C.ClickDate is null		-- �N���b�N����������
		--ORDER BY
		--	A.Category DESC, 
		--	B.ClickCost DESC ;
	) AS T
	WHERE
		RowNumber = @RowNum	-- �y�[�W���O
	;

END
GO
/*
*/