USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spCollectTargetMail_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spCollectTargetMail_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spCollectTargetMail_Insert]
	@����				nvarchar(50),
	@From_MailAddress	nvarchar(200),
	@No					bigint	output
AS
BEGIN

	INSERT INTO [mst].[tCollectTargetMail](
		 [����]
		,[From_MailAddress]
    ) VALUES (
		 @����
		,@From_MailAddress
	);

	SELECT TOP 1 @No = [No]
	FROM [mst].[tCollectTargetMail]
	where [����] = @���� AND 
		[From_MailAddress] = @From_MailAddress ;

END
GO

