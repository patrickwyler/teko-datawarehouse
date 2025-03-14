/* Create database */

Create database dwh;
Go

Use dwh;
GO

/* Create tables */

CREATE TABLE dbo.Products (
    PK int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name varchar(100) NOT NULL
);

CREATE TABLE dbo.Sellers (
    PK int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name varchar(100) NOT NULL
);

CREATE TABLE dbo.Cities (
    PK int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name varchar(100) NOT NULL
);

CREATE TABLE dbo.PointOfSales (
    PK int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name varchar(100) NOT NULL
);

CREATE TABLE dbo.Sales (
    PK int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	seller_FK int FOREIGN KEY REFERENCES Sellers(PK),
	product_FK int FOREIGN KEY REFERENCES Products(PK),
	city_FK int FOREIGN KEY REFERENCES Cities(PK),
	pos_FK int FOREIGN KEY REFERENCES PointOfSales(PK),
	[date] datetime NOT NULL,
	amount int NOT NULL,
	price decimal(9,0) NOT NULL
);

GO

/* Procedure: Get sales for city and point of sales */

CREATE PROCEDURE dbo.getSalesForCityAndPointOfSale
    @City varchar(100),
    @PointOfSale varchar(100)
AS
    SELECT SUM(s.amount*s.price)
    FROM Sales s
	INNER JOIN Cities c
 		ON s.city_FK = c.PK
	INNER JOIN PointOfSales pos
 		ON s.pos_FK = pos.PK
	WHERE c.name = @City
		AND pos.name = @PointOfSale
GO

/* Procedure: Get sales for product and seller */

CREATE PROCEDURE dbo.getSalesForProductAndSeller
    @Product varchar(100),
    @Seller varchar(100)
AS
    SELECT SUM(s.amount*s.price)
    FROM Sales s
	INNER JOIN Products p
 		ON s.product_FK = p.PK
	INNER JOIN Sellers se
 		ON s.seller_FK = se.PK
	WHERE p.name = @Product
		AND se.name = @Seller
GO

/* Procedure: Get sales of all cities and point of sales ordered by Sales */

CREATE PROCEDURE dbo.getAllSalesOfAllCitiesAndPointOfSales
AS
    SELECT c.name AS City, pos.name AS POS, sum(s.amount*s.price) 
  OVER (ORDER BY s.PK ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) Sales
    FROM Sales s
	INNER JOIN Cities c
 		ON s.city_FK = c.PK
	INNER JOIN PointOfSales pos
 		ON s.pos_FK = pos.PK
	ORDER BY Sales
GO

/* Procedure: Get all sales of products and sellers */

CREATE PROCEDURE dbo.getAllSalesOfProductsAndSellers
AS
    SELECT p.name AS Product, se.name AS Seller, sum(s.amount*s.price) 
  OVER (ORDER BY s.PK ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) Sales
    FROM Sales s
    INNER JOIN Products p
        ON s.product_FK = p.PK
    INNER JOIN Sellers se
        ON s.seller_FK = se.PK
    ORDER BY Sales 
GO
