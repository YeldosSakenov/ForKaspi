CREATE PROCEDURE spGetListOfApproved
@UserId NVARCHAR(128),
@OfferId INT
AS
BEGIN
SELECT 
                            OfferId,
                            UserId,
                            StatusId,
                            DateApproved                             
                            FROM 
                            Approved WHERE OfferId=@OfferId AND UserId=@UserId
							ORDER BY DateApproved DESC;
END
GO

ALTER PROCEDURE spGetListOfUserRoles
@UserId NVARCHAR(128)
AS
BEGIN
SELECT UserId,
                            RoleId                                                    
                            FROM 
                            AspNetUserRoles WHERE UserId=@UserId;
END
GO



CREATE PROCEDURE dbo.spInsertApproved
       @UserId  NVARCHAR(128),
	   @OfferId INT,
	   @StatusId INT	  
AS 
BEGIN 
     SET NOCOUNT ON    
INSERT INTO dbo.Approved
(UserId,OfferId,StatusId,DateApproved) 
Values( @UserId,@OfferId,@StatusId,GETDATE());
END
GO

CREATE PROCEDURE dbo.spInsertUserRoles
       @UserId  NVARCHAR(128),
	   @RoleId NVARCHAR(128)  
AS 
BEGIN 
     SET NOCOUNT ON    
INSERT INTO dbo.AspNetUserRoles
(UserId,RoleId) 
Values( @UserId,@RoleId);
END
GO


CREATE PROCEDURE dbo.spUpdateApproved
       @UserId  NVARCHAR(128),
	   @OfferId INT,
	   @StatusId INT	
AS 
BEGIN 
SET NOCOUNT ON;    
     UPDATE dbo.Approved
	 SET UserId = @UserId,
	 OfferId=@OfferId,
	 StatusId=@StatusId,
	 DateApproved=GETDATE() 
	 WHERE OfferId=@OfferId 
	 AND UserId=@UserId;
END
GO



CREATE PROCEDURE dbo.spDeleteApproved     
	    @UserId  NVARCHAR(128),
	   @OfferId INT,
	   @StatusId INT	
AS 
BEGIN 
SET NOCOUNT ON;    
     DELETE FROM dbo.Approved 
	 WHERE 
	 OfferId=@OfferId 
	 AND UserId=@UserId
	 AND StatusId=@StatusId;
END
GO

CREATE PROCEDURE dbo.spDeleteUserRoles     
	    @UserId  NVARCHAR(128),
	   @RoleId NVARCHAR(128)  
AS 
BEGIN 
SET NOCOUNT ON;    
     DELETE FROM dbo.AspNetUserRoles 
	 WHERE 
	 UserId=@UserId 
	 AND RoleId=@RoleId;
END
GO

CREATE PROCEDURE dbo.spCheckOfExistApprove
@UserId  NVARCHAR(128),
@OfferId INT,
@StatusId INT,	
@Exists INT = NULL OUTPUT
AS
BEGIN
SET NOCOUNT ON;
IF EXISTS(
SELECT 
                            OfferId,
                            UserId,
                            StatusId,
                            DateApproved                             
                            FROM 
                            Approved WHERE OfferId=@OfferId AND UserId=@UserId AND StatusId=@StatusId
							)
BEGIN
            SET @Exists = 1
      END
      ELSE
      BEGIN
            SET @Exists = 0
      END
 
      RETURN @Exists
END
GO

CREATE PROCEDURE dbo.spCheckOfExistUserRole
 @UserId  NVARCHAR(128),
 @RoleId NVARCHAR(128),
@Exists INT = NULL OUTPUT
AS
BEGIN
SET NOCOUNT ON;
IF EXISTS(
SELECT 
                            RoleId,
                            UserId                            
                            FROM 
                            AspNetUserRoles WHERE UserId=@UserId AND RoleId=@RoleId)
BEGIN
            SET @Exists = 1
      END
      ELSE
      BEGIN
            SET @Exists = 0
      END
 
      RETURN @Exists
END
GO