USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spCollectTargetMail_GetNo]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spCollectTargetMail_GetNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spCollectTargetMail_GetNo]
	@ñºèÃ				nvarchar(50),
	@From_MailAddress	nvarchar(200),
	@No					int				output
AS
BEGIN

	declare @Cnt int = 0;
	SELECT @Cnt = count(*)

	FROM [mst].[tCollectTargetMail]
	where [From_MailAddress] = @From_MailAddress;
	
	if @Cnt < 1
	begin

		INSERT INTO [mst].[tCollectTargetMail] (
            [ñºèÃ]
           ,[From_MailAddress]
	    ) VALUES (
            @ñºèÃ
           ,@From_MailAddress
		);

	end;

	SELECT TOP 1 @No = [No]
	FROM [mst].[tCollectTargetMail]
	where [From_MailAddress] = @From_MailAddress;

END
GO
/*
*/

