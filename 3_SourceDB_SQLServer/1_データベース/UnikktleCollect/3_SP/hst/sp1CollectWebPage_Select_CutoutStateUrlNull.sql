USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Select_UrlState0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Select_UrlState0] ;
GO
IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Select_CutoutStateUrl0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Select_CutoutStateUrl0] ;
GO
IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Select_CutoutStateUrlNull]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Select_CutoutStateUrlNull] ;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectWebPage_Select_CutoutStateUrlNull]
AS
BEGIN

	-- ���W����Html����URL�؏o�����I����Ă��Ȃ�Html�B
	SELECT TOP 100	-- SqlDataReader��1���Â��������肾�ƁASQLServer���牞�����Ԃ��ė��Ȃ����ۂɑ��������̂ŁA100���Â�DataTable�ɓ���āA0���ɂȂ�܂ŏ����𑱍s����B
		[DomainNo],
		[UrlNo],
		[Html]
	FROM
		[hst].[t1CollectWebPage]
	WHERE
		[CollectState] = 1 AND
		[CutoutStateUrl] is null
	;

END
GO
