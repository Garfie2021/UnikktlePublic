USE [UnikktleWeb]
GO

IF OBJECT_ID(N'[job].[spFullText_Rebuild]', N'P') IS NOT NULL
	DROP PROCEDURE [job].[spFullText_Rebuild] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [job].[spFullText_Rebuild]
AS
BEGIN

	-- �t���e�L�X�g �J�^���O �č쐬
	ALTER FULLTEXT CATALOG Word REBUILD;
	ALTER FULLTEXT CATALOG Word REORGANIZE;
	

	-- �t���e�L�X�g �J�^���O �č쐬
	-- �u�x��: �e�[�u���܂��̓C���f�b�N�X�t���r���[ 'clt.tKeyword' �̍쐬�����݃A�N�e�B�u�Ȃ̂ŁA���̃e�[�u���܂��̓C���f�b�N�X�t���r���[�ł̃t���e�L�X�g �C���f�b�N�X�̍쐬�J�n�v���͖�������܂��B�v���o�͂����̂ŃR�����g�A�E�g���Ă�B
	--ALTER FULLTEXT INDEX ON [clt].[tKeyword] START FULL POPULATION;

END
GO

