USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_SelectGoogle����]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_SelectGoogle����] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_SelectGoogle����]
AS
BEGIN

	-- CollectGoogleSearch2.exe��100������������̂�30�����x������̂ŁA2000���i10���ԁj�ɂ��Ė�Ԃ͂����Ǝ��W��������B
	SELECT Top 1000
		 [No]
		,[Word]
	FROM
		[mst].[tKeyword]
	WHERE 
		([�̗p����ς�] = 0 OR ([�̗p] = 1 AND [�̗p����ς�] = 1))
	ORDER BY
		--�L�[���[�h�̌�����100�N�o���Ă��I���Ȃ��̂ŁABing�AGoogle�AYahoo���ꂼ��ŁA�܂��������ĂȂ��L�[���[�h����������B
		 [Google��������]
		,[Bing��������]
		,[Yahoo��������]
		,[CollectTargetCategory] DESC	-- �u6�FWeb�T�[�o��UI���猟�����ꂽ�L�[���[�h�v��D�悵�Č�������B
		,[No] DESC

END
GO

