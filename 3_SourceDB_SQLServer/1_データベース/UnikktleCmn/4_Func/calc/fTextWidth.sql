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
	--declare @Word NVARCHAR(100) = 'ああC#い'
	--declare @Word NVARCHAR(100) = 'ああい'

	-- NVARCHARのデータに対して、VARCHARのDATALENGTHを比較することで、半角全角を判定する。
	-- VARCHARと比較しているのはバグではない。
	IF LEN(@Word)*2 <= DATALENGTH(CONVERT(VARCHAR(100), @Word))
	BEGIN
		--全角のみ
		RETURN (LEN(@Word)*20)+40 ;
		--select 1
		--select (LEN(@Word)*20)+40 ;
		--RETURN 
	END
	ELSE IF LEN(@Word) < DATALENGTH(CONVERT(VARCHAR(100), @Word))
	BEGIN
		--全角含む
		RETURN ((LEN(@Word)*20)+200) * (CONVERT(float, LEN(@Word)) / DATALENGTH(CONVERT(NVARCHAR(100), @Word))) ;
		--select 2
		--select ((LEN(@Word)*20)+200) * (CONVERT(float, LEN(@Word)) / DATALENGTH(CONVERT(VARCHAR(100), @Word))) ;
		--RETURN 
	END ;

	--半角のみ
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
				'全角のみ'
			WHEN LEN([Word]) < DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				'全角含む'
			ELSE 
				'半角のみ'
		END AS 判定,

		CASE
			WHEN LEN([Word]) < DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				--全角含む
				(LEN([Word])*20)+40	
			ELSE 
				--半角のみ
				(LEN([Word])*9)+40	
		END AS old,
		(LEN([Word])*20)+40 as 全角含む, 
		(LEN([Word])*9)+40	as 半角のみ, 

		(CONVERT(float, LEN([Word])) / DATALENGTH(CONVERT(VARCHAR(100), [Word]))) as 割る,

		((LEN([Word])*20)+40) * (CONVERT(float, LEN([Word])) / DATALENGTH(CONVERT(VARCHAR(100), [Word]))) as new,

		CASE
			WHEN LEN([Word])*2 <= DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				--全角のみ
				(LEN([Word])*20)+40
			WHEN LEN([Word]) < DATALENGTH(CONVERT(VARCHAR(100), [Word])) THEN 
				--全角含む
				((LEN([Word])*20)+40) * (CONVERT(float, LEN([Word])) / DATALENGTH(CONVERT(VARCHAR(100), [Word])))
			ELSE 
				--半角のみ
				(LEN([Word])*9)+40
		END AS new2

	from [mst].[tKeyword] 
	where [no]=1704677 or [no]=3000 or [no]=1;
	*/

GO