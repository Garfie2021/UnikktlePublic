USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_Select_�X�V����]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Select_�X�V����] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Select_�X�V����]
	@�X�V����End	DateTime,
	@�X�V����Start	DateTime
AS
BEGIN

	SELECT
		[No],
		[r_w],
		[Word],
		[��͌��f�[�^]
	FROM
		[mst].[tKeyword]
	WHERE
		@�X�V����End >= [�X�V����] AND [�X�V����] >= @�X�V����Start

END
GO

