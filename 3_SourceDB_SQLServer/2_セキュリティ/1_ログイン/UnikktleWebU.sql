USE [master]
GO
CREATE LOGIN [<UnikktleWebU] WITH PASSWORD=N'xxx', DEFAULT_DATABASE=[UnikktleWeb], DEFAULT_LANGUAGE=[���{��], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
USE [UnikktleWeb]
GO
CREATE USER [<UnikktleWebU] FOR LOGIN [<UnikktleWebU]
GO
ALTER USER [<UnikktleWebU] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [<UnikktleWebU]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [<UnikktleWebU]
GO
