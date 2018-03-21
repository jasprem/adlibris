CREATE TABLE [dbo].[tblReservation]
(
	[product] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [customer] NVARCHAR(50) NOT NULL, 
    [shelf] NVARCHAR(50) NOT NULL, 
    [totalOrdered] INT NOT NULL, 
    [totalDelivered] INT NOT NULL
)
