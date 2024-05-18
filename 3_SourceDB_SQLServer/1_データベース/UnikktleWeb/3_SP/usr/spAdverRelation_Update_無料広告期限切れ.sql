USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Update_�����L�������؂�]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Update_�����L�������؂�] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
-- �L�������؂�̖����L���𖳌���	
CREATE PROCEDURE [usr].[spAdverRelation_Update_�����L�������؂�]
AS
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tAdverRelation]
	SET
		[Valid] = 0,	-- ����=0
		[�X�V����] = GETDATE()
	WHERE
		[Category] = 1 AND							-- ����=1
		DATEDIFF(DAY, [�X�V����], GETDATE()) > 30	-- �����L���̗L��������30��
	;

END
GO
/*
*/