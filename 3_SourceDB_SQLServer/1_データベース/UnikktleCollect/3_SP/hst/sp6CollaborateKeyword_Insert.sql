USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp6CollaborateKeyword_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp6CollaborateKeyword_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �Čv�Z�X�g�A�h
CREATE PROCEDURE [hst].[sp6CollaborateKeyword_Insert]
AS
BEGIN

	SET NOCOUNT ON;

		-- �Â��f�[�^�͍폜
		DELETE FROM [hst].[t6CollaborateKeyword];


		-- �L�[���[�h�̐V�����֘A��Insert
		INSERT INTO [hst].[t6CollaborateKeyword]
		(
			[KeywordNo_��],
			[KeywordNo_��],
			[�����o���h�L�������g��]
		)
		SELECT
			[KeywordNo_��],
			[KeywordNo_��],
			SUM(�����o���h�L�������g��) as �����o���h�L�������g��
		FROM (
			-- [�����o���h�L�������g��]�͌��Ɛ���}�[�W����ׂɁA���ꂼ��̃e�[�u����2��Select���Ă���B
			-- SELECT * FROM [UnikktleCollect].[hst].[t6CollaborateKeyword] where [KeywordNo_��] = 1 and [KeywordNo_��] = 1454; => �����o���h�L�������g���F1�@=> �}�[�W��F3
			-- SELECT * FROM [UnikktleCollect].[hst].[t6CollaborateKeyword] where [KeywordNo_��] = 1454 and [KeywordNo_��] = 1; => �����o���h�L�������g���F2�@=> �}�[�W��F3

			SELECT
				[KeywordNo_��],
				[KeywordNo_��],
				1 as [�����o���h�L�������g��]	-- ���[���}�K�W���͍L���������̂ŁA[�����o���h�L�������g��]��1�Œ�B
			FROM
				[hst].[t5CollaborateKeyword_Mail]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]
			UNION ALL
			SELECT
				[KeywordNo_��] AS KeywordNo_��,
				[KeywordNo_��] AS KeywordNo_��,
				1 as [�����o���h�L�������g��]	-- ���[���}�K�W���͍L���������̂ŁA[�����o���h�L�������g��]��1�Œ�B
			FROM
				[hst].[t5CollaborateKeyword_Mail]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]

			UNION ALL

			SELECT
				[KeywordNo_��],
				[KeywordNo_��],
				[�����o���h�L�������g��]
			FROM
				[hst].[t5CollaborateKeyword_Google]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]
			UNION ALL
			SELECT
				[KeywordNo_��] AS KeywordNo_��,
				[KeywordNo_��] AS KeywordNo_��,
				[�����o���h�L�������g��]
			FROM
				[hst].[t5CollaborateKeyword_Google]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]

			UNION ALL

			SELECT
				[KeywordNo_��],
				[KeywordNo_��],
				[�����o���h�L�������g��]
			FROM
				[hst].[t5CollaborateKeyword_Bing]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]
			UNION ALL
			SELECT
				[KeywordNo_��] AS KeywordNo_��,
				[KeywordNo_��] AS KeywordNo_��,
				[�����o���h�L�������g��]
			FROM
				[hst].[t5CollaborateKeyword_Bing]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]

			UNION ALL

			SELECT
				[KeywordNo_��],
				[KeywordNo_��],
				[�����o���h�L�������g��]
			FROM
				[hst].[t5CollaborateKeyword_Yahoo]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]
			UNION ALL
			SELECT
				[KeywordNo_��] AS KeywordNo_��,
				[KeywordNo_��] AS KeywordNo_��,
				[�����o���h�L�������g��]
			FROM
				[hst].[t5CollaborateKeyword_Yahoo]
			WHERE
				[KeywordNo_��] <> [KeywordNo_��]
		) AS t
		GROUP BY
			[KeywordNo_��],
			[KeywordNo_��]
		;



END
GO
/*
*/

