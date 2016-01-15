-- bootstrap script for LendingLibrary application
-- Please run before attempting to start the application
------ uncomment next few lines if you'd *really* like to recreate the database
-- use master;
-- go
-- ALTER DATABASE  [LendingLibrary]
-- SET SINGLE_USER
-- WITH ROLLBACK IMMEDIATE
-- drop database [LendingLibrary]
-- go
------ normal creation after here
use master;
go
if not exists (select name from master..syslogins where name = 'LendingLibraryWeb')
    begin
        create login LendingLibraryWeb with password = 'P4$$w0rd';
    end;
go


if not exists (select name from master..sysdatabases where name = 'LendingLibrary')
begin
create database LendingLibrary
end;
GO

use LendingLibrary
if not exists (select * from sysusers where name = 'LendingLibraryWeb')
begin
create user LendingLibraryWeb
	for login LendingLibraryWeb
	with default_schema = dbo
end;
GO
grant connect to LendingLibraryWeb
go
exec sp_addrolemember N'db_datareader', N'LendingLibraryWeb';
go
exec sp_addrolemember N'db_datawriter', N'LendingLibraryWeb';
go
exec sp_addrolemember N'db_owner', N'LendingLibraryWeb';
GO
use master;
GO

