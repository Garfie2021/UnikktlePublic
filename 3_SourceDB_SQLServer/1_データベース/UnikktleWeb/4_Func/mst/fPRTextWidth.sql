USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID (N'mst.fPRTextWidth', N'FN') IS NOT NULL  
    DROP FUNCTION mst.fPRTextWidth;
GO  

CREATE FUNCTION mst.fPRTextWidth(@AdverTitle1 NVARCHAR(50), @AdverTitle2 NVARCHAR(50))
RETURNS smallint
AS   
BEGIN  

	declare	@AdverTitle1_r_w	smallint = UnikktleCmn.calc.fTextWidth(@AdverTitle1);
	declare	@AdverTitle2_r_w	smallint = UnikktleCmn.calc.fTextWidth(@AdverTitle2);

	IF @AdverTitle1_r_w > @AdverTitle2_r_w
	BEGIN
		--‘SŠp‚Ì‚Ý
		RETURN @AdverTitle1_r_w ;
	END ;

	RETURN @AdverTitle2_r_w ;

END;
