CREATE TABLE [dbo].[tblCatalog]
(
	[product] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [shelf] NVARCHAR(50) NOT NULL, 
    [totalAvailable] INT NOT NULL
)
