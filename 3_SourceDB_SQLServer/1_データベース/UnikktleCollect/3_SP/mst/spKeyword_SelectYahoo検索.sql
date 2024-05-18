USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_SelectYahoo����]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_SelectYahoo����] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_SelectYahoo����]
AS
BEGIN

	-- CollectYahooSearch2.exe��100������������̂�30�����x������̂ŁA2000���i10���ԁj�ɂ��Ė�Ԃ͂����Ǝ��W��������B
	SELECT  Top 1000
		[No],
		[Word]
	FROM 
		(
			SELECT Top 2000
				 [No]
				,[Word]
			FROM
				[mst].[tKeyword]
			WHERE 
				([�̗p����ς�] = 0 OR ([�̗p] = 1 AND [�̗p����ς�] = 1))
			ORDER BY
				--�L�[���[�h�̌�����100�N�o���Ă��I���Ȃ��̂ŁABing�AGoogle�AYahoo���ꂼ��ŁA�܂��������ĂȂ��L�[���[�h����������B
				 [Yahoo��������]
				,[Google��������]
				,[Bing��������]
				,[No]
		) as t
	ORDER BY
		[No] DESC
END
GO

