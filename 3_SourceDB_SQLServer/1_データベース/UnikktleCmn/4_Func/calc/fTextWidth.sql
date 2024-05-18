USE [UnikktleCmn]
GO
/*
*/
IF OBJECT_ID (N'[calc].[fTextWidth]', N'FN') IS NOT NULL  
    DROP FUNCTION [calc].[fTextWidth];
GO

CREATE FUNCTION [calc].[fTextWidth](@Word NVARCHAR(100))
RETURNS smallint
AS   
BEGIN  

	--declare @Word NVARCHAR(100) = 'C#'
	--declare @Word NVARCHAR(100) = '����C#��'
	--declare @Word NVARCHAR(100) = '������'

	-- NVARCHAR�̃f�[�^�ɑ΂��āAVARCHAR��DATALENGTH���r���邱�ƂŁA���p�S�p�𔻒肷��B
	-- VARCHAR�Ɣ�r���Ă���̂̓o�O�ł͂Ȃ��B
	IF LEN(@Word)*2 <= DATALENGTH(CONVERT(VARCHAR(100), @Word))
	BEGIN
		--�S�p�̂�
		RETURN (LEN(@Word)*20)+40 ;
		--select 1
		--select (LEN(@Word)*20)+40 ;
		--RETURN 
	END
	ELSE IF LEN(@Word) < DATALENGTH(CONVERT(VARCHAR(100), @Word))
	BEGIN
		--�S�p�܂�
		RETURN ((LEN(@Word)*20)+200) * (CONVERT(float, LEN(@Word)) / DATALENGTH(CONVERT(NVARCHAR(100), @Word))) ;
		--select 2
		--select ((LEN(@Word)*20)+200) * (CONVERT(float, LEN(@Word)) / DATALENGTH(CONVERT(VARCHAR(100), @Word))) ;
		--RETURN 
	END ;

	--���p�̂�
	RETURN (LEN(@Word)*9)+40 ;
	--select 3
	--select (LEN(@Word)*9)+40 ;
	--RETURN 

END;
GO
/*
*/

	/*
	select word, 
		LEN([Word]) as LEN, 
		DATALENGTH(CONVERT(VARCHAR(100), [Word])) as DATALENGTH, 
		[r_w],  

		CASE
			WHEN LEN([Word])*2 <= DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				'�S�p�̂�'
			WHEN LEN([Word]) < DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				'�S�p�܂�'
			ELSE 
				'���p�̂�'
		END AS ����,

		CASE
			WHEN LEN([Word]) < DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				--�S�p�܂�
				(LEN([Word])*20)+40	
			ELSE 
				--���p�̂�
				(LEN([Word])*9)+40	
		END AS old,
		(LEN([Word])*20)+40 as �S�p�܂�, 
		(LEN([Word])*9)+40	as ���p�̂�, 

		(CONVERT(float, LEN([Word])) / DATALENGTH(CONVERT(VARCHAR(100), [Word]))) as ����,

		((LEN([Word])*20)+40) * (CONVERT(float, LEN([Word])) / DATALENGTH(CONVERT(VARCHAR(100), [Word]))) as new,

		CASE
			WHEN LEN([Word])*2 <= DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				--�S�p�̂�
				(LEN([Word])*20)+40
			WHEN LEN([Word]) < DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				--�S�p�܂�
				((LEN([Word])*20)+40) * (CONVERT(float, LEN([Word])) / DATALENGTH(CONVERT(VARCHAR(100), [Word])))
			ELSE 
				--���p�̂�
				(LEN([Word])*9)+40
		END AS new2

	from [mst].[tKeyword] 
	where [no]=1704677 or [no]=3000 or [no]=1;
	*/

GO