CREATE DATABASE [Sales];
GO

USE [Sales];
GO

CREATE TABLE Managers (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[FirstName] NVARCHAR(30) NOT NULL,
	[LastName] NVARCHAR(30) NOT NULL
);
GO

CREATE TABLE Products (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Title] NVARCHAR(MAX) NOT NULL,
	[MinPrice] MONEY NOT NULL
);
GO

CREATE TABLE Sales (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[ManagerID] INT NOT NULL,
	[ProductID] INT NOT NULL,
	[Sold_In] DATETIME NOT NULL DEFAULT GETDATE(),
	[Price] MONEY NOT NULL,
	CONSTRAINT FK_Managers FOREIGN KEY ([ManagerID]) REFERENCES [Managers] ([ID]),
	CONSTRAINT FK_Products FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ID]),
)
GO 
INSERT INTO [Managers] (FirstName, LastName) VALUES
	('јлександр', 'ёрьев'),
	('Ѕорис', 'Ѕурда'),
	('¬асилий','яковлев'),
	('јбрам','ѕерельман'),
	('яков','¬алек')
GO

INSERT INTO [Products] (MinPrice, Title) VALUES
	(7999.99, 'Xerox'),
	(69.49, 'Kinder Surprise'),
	(120000.0 , 'Lada Granta'),
	(999999.99, 'Land Cruiser')
GO

CREATE PROCEDURE [GetManager] 
	@id INT 
	AS SELECT * FROM [Managers] WHERE ID=@id;
GO

CREATE PROCEDURE [GetProduct] 
	@id INT
	AS SELECT * FROM [Products] WHERE ID=@id;
GO

CREATE PROCEDURE [GetSale] 
	@id INT 
	AS SELECT Sales.*, Managers.FirstName, Managers.LastName, Products.Title, Products.MinPrice  FROM [Sales] 
					JOIN [Managers] ON [Sales].[ManagerID]=[Managers].[ID]
					JOIN [Products] ON [Products].[ID]=[Sales].[ProductID]
					 WHERE Sales.ID=@id;
GO

CREATE PROCEDURE [GetManagers]
AS
    SELECT * FROM Managers;
GO

CREATE PROCEDURE [GetProducts] 
	AS 	SELECT * FROM [Products];
GO

CREATE PROCEDURE [GetSales] 
	AS SELECT Sales.*, Managers.FirstName, Managers.LastName, Products.Title, Products.MinPrice FROM [Sales] 
					JOIN [Managers] ON [Sales].[ManagerID]=[Managers].[ID]
					JOIN [Products] ON [Products].[ID]=[Sales].[ProductID]; 
GO

CREATE PROCEDURE [CreateManager]
	@Firstname NVARCHAR(MAX),
	@Lastname NVARCHAR(MAX)
	AS INSERT INTO Managers VALUES (@Firstname, @Lastname)
	SELECT SCOPE_IDENTITY()

GO
CREATE PROCEDURE [CreateProduct]
	@Title NVARCHAR(MAX),
	@MinPrice MONEY
	AS INSERT INTO Products VALUES (@Title, @MinPrice)
	SELECT SCOPE_IDENTITY()

GO

CREATE PROCEDURE [CreateSale]
	@Price MONEY,
	@SellDate DATETIME,
	@ManID INT,
	@ProdID INT
	AS INSERT INTO Sales(Price, Sold_In, ManagerID, ProductID) VALUES (@Price, @SellDate, @ManID, @ProdID)
	SELECT SCOPE_IDENTITY()
GO

CREATE PROCEDURE [UpdateSale]
	@id INT,
	@Price MONEY,
	@SellDate DATETIME,
	@ManID INT,
	@ProdID INT
	AS UPDATE Sales SET Price=@Price, Sold_In=@SellDate, ManagerID=@ManID, ProductID=@ProdID WHERE ID=@id;

GO	

CREATE PROCEDURE [DeleteManager]
	@id INT
	AS BEGIN
	IF not exists (Select [Sales].[ID] from [Sales] WHERE [ManagerID]=@id)
		BEGIN
			DELETE FROM [Managers] WHERE [ID]=@id;
			SELECT 1;
		END
	ELSE 
		SELECT -1;		
	END
GO

CREATE PROCEDURE [DeleteProduct]
	@id INT
	AS BEGIN
	IF not exists (Select [Sales].[ID] from [Sales] WHERE [ProductID]=@id)
		BEGIN
			DELETE FROM [Products] WHERE [ID]=@id;
			SELECT 1;
		END
	ELSE 
		SELECT -1;		
	END
GO

CREATE PROCEDURE [TotalSales]
	AS Select SUM(Price) FROM [Sales];
GO
