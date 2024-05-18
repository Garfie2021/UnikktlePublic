USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spUrl_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spUrl_Insert]
	@DomainNo	bigint,
	@Url		varchar(max)
AS
BEGIN

	/*
	DECLARE @DomainNo	bigint = 1670;
	DECLARE @Url		varchar(max) = 'https://maps.google.co.jp/maps?q=b&amp;oe=UTF-8&amp;num=20&amp;um=1&amp;ie=UTF-8&amp;sa=X&amp;ved=0ahUKEwjVzqu9yN3mAhXGJTQIHe1mBlkQ_AUICigD';
	*/

	DECLARE @Cnt int = 0;
	SELECT @Cnt = COUNT_BIG(*)
	FROM (
		SELECT [Url]
		FROM [mst].[tUrl]
		WHERE [DomainNo] = @DomainNo
	) AS T
	WHERE [Url] = @Url;
	
	IF @Cnt < 1
	BEGIN
		INSERT INTO [mst].[tUrl] (
			[DomainNo],
			[UrlNo],
			[Url]
		) VALUES (
			@DomainNo,
			(SELECT COUNT_BIG(*) FROM [mst].[tUrl] WHERE [DomainNo] = @DomainNo) + 1,
			@Url
		);
	END;

END
GO
/*
*/

