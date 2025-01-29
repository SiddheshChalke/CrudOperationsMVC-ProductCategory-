--Create Table Category
CREATE TABLE [dbo].[Category] (
    [CategoryId]   INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

--Create Table Product
CREATE TABLE [dbo].[Product] (
    [ProductId]   INT           IDENTITY (1, 1) NOT NULL,
    [ProductName] NVARCHAR (50) NOT NULL,
    [CategoryId]  INT           NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK__Product__Categor__4BAC3F29] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE CASCADE
);

--Insert Queries for Product and Category Tables
INSERT INTO Category (CategoryName) VALUES ('Electronics'), ('Clothing'), ('Furniture');
INSERT INTO Product (ProductName, CategoryId) VALUES ('Mobile', 1), ('Shirt', 2), ('Table', 3);

-- Pagination logic in SQL
CREATE PROCEDURE GetProductsPaginated
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.ProductId, p.ProductName, c.CategoryId, c.CategoryName
    FROM Product p
    INNER JOIN Category c ON p.CategoryId = c.CategoryId
    ORDER BY p.ProductId
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
