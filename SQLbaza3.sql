SELECT name, collation_name FROM sys.databases; 
GO 
ALTER DATABASE db_aa094b_leonbicak35  SET SINGLE_USER WITH ROLLBACK IMMEDIATE; 
GO 
ALTER DATABASE db_aa094b_leonbicak35 COLLATE Croatian_CI_AS; 
GO 
ALTER DATABASE db_aa094b_leonbicak35  SET MULTI_USER; 
GO 
SELECT name, collation_name FROM sys.databases; 
GO 


SELECT * FROM fn_helpcollations() 