CREATE TABLE [dbo].[tblReservation]
(
	[productId] NVARCHAR(50) NOT NULL , 
    [customerId] NVARCHAR(50) NOT NULL, 
    [shelf] NVARCHAR(50) NOT NULL, 
    [totalOrdered] INT NOT NULL, 
    [totalDelivered] INT NULL
)
