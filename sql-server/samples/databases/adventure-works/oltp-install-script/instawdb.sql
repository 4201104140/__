 :setvar SqlSamplesSourceDataPath "C:\Samples\AdventureWorks\"

/*
 * 4. Append the SQL Server version number to database name if you want to
 *    differentiate it from other installs of AdventureWorks.
 */

:setvar DatabaseName "AdventureWorks"

/* Execute the script
 */

IF '$(SqlSamplesSourceDataPath)' IS NULL OR '$(SqlSamplesSourceDataPath)' = ''
BEGIN
	RAISERROR(N'The variable SqlSamplesSourceDataPath must be defined.', 16, 127) WITH NOWAIT
	RETURN
END;


SET NOCOUNT OFF;
GO

PRINT CONVERT(varchar(1000), @@VERSION);
GO

PRINT '';
PRINT 'Started - ' + CONVERT(varchar, GETDATE(), 121);
GO

USE [master];
GO
-- ****************************************
-- Drop Database
-- ****************************************
PRINT '';
PRINT '*** Dropping Database';
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] WHERE [name] = N'$(DatabaseName)')
    DROP DATABASE $(DatabaseName);

-- If the database has any other open connections close the network connection.
IF @@ERROR = 3702 
    RAISERROR('$(DatabaseName) database cannot be dropped because there are still other open connections', 127, 127) WITH NOWAIT, LOG;
GO


-- ****************************************
-- Create Database
-- ****************************************
PRINT '';
PRINT '*** Creating Database';
GO

CREATE DATABASE $(DatabaseName);
GO

PRINT '';
PRINT '*** Checking for $(DatabaseName) Database';
/* CHECK FOR DATABASE IF IT DOESN'T EXISTS, DO NOT RUN THE REST OF THE SCRIPT */
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.databases WHERE name = N'$(DatabaseName)')
BEGIN
PRINT '*******************************************************************************************************************************************************************'
+char(10)+'********$(DatabaseName) Database does not exist.  Make sure that the script is being run in SQLCMD mode and that the variables have been correctly set.*********'
+char(10)+'*******************************************************************************************************************************************************************';
SET NOEXEC ON;
END
GO

ALTER DATABASE $(DatabaseName) 
SET RECOVERY SIMPLE, 
    ANSI_NULLS ON, 
    ANSI_PADDING ON, 
    ANSI_WARNINGS ON, 
    ARITHABORT ON, 
    CONCAT_NULL_YIELDS_NULL ON, 
    QUOTED_IDENTIFIER ON, 
    NUMERIC_ROUNDABORT OFF, 
    PAGE_VERIFY CHECKSUM, 
    ALLOW_SNAPSHOT_ISOLATION OFF;
GO

USE $(DatabaseName);
GO

-- ****************************************
-- Create DDL Trigger for Database
-- ****************************************
PRINT '';
PRINT '*** Creating DDL Trigger for Database';
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE