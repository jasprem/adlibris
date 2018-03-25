CREATE TABLE [dbo].[tblCatalog]
(
	[productId] NVARCHAR(50) NOT NULL , 
    [shelf] NVARCHAR(50) NOT NULL, 
    [available] INT NOT NULL, 
    CONSTRAINT [PK_tblCatalog] PRIMARY KEY ([productId], [shelf])
)
