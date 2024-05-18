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

-- 関連ワードに連動する広告を検索
-- 競合する単価(CompetingUnitPrices)
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

			-- 最初に全行を結合するより、絞り込んだ結果を結合する方が速いはず。
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
					-- 有効=1
					-- Creditが切れたユーザーの広告は別処理で無効にされる。
					-- 有効期限切れ（30日 Over）の広告は別処理で無効にされる。
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
			LEFT JOIN		-- LEFT OUTER JOIN と LEFT JOIN は同じもの。
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
					-- 1日より前のクリック履歴は対象外。
					-- 1日以内にクリックされたPRは表示しないことで、同じ広告が常に表示される問題に対処。
					ClickDate > DATEADD(DAY, -1, GETDATE()) AND
					-- ログインユーザーはUserNoだけで判定した方が良いとか、細かいことは気にしない。パフォーマンス優先。
					[ClickUserNo] = @ClickUserNo AND
					[ClickUserIP] = @ClickUserIP
					-- AND [ClickUserSessionId] = @ClickUserSessionId  セッションIDの変更は頻繁に起きるので条件に加えない。
			) AS C
			ON
				B.UserNo = C.UserNo AND
				B.BusinessNo = C.BusinessNo AND
				B.AdverNo = C.AdverNo
				--B.RelationWordNo = C.WordNo
		WHERE
			C.ClickDate is null		-- クリック履歴が無い
		--ORDER BY
		--	A.Category DESC, 
		--	B.ClickCost DESC ;
	) AS T
	WHERE
		RowNumber = @RowNum	-- ページング
	;

END
GO
/*
*/