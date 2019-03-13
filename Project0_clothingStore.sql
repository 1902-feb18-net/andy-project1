-- create our DB
CREATE DATABASE Project0;
GO

-- create a schema
CREATE SCHEMA Project0;
GO

-- create tables
CREATE TABLE Project0.Location (
	LocationID INT NOT NULL UNIQUE IDENTITY,
	StoreName NVARCHAR(100) NOT NULL,
);

CREATE TABLE Project0.Customer (
	CustomerID INT NOT NULL UNIQUE IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	DefaultStoreID INT NOT NULL
);

CREATE TABLE Project0.StoreOrder (
	OrderID INT NOT NULL UNIQUE IDENTITY,
	StoreID INT NOT NULL,
	CustomerID INT NOT NULL,
	DatePurchased DATETIME2 NOT NULL,
	Total MONEY
);

CREATE TABLE Project0.OrderList (
	OrderID INT NOT NULL,
	ItemID INT NOT NULL,
	ItemBought INT NOT NULL,
	OrderListID INT NOT NULL UNIQUE IDENTITY
);

CREATE TABLE Project0.Inventory (
	StoreID INT NOT NULL,
	ItemID INT NOT NULL, -- should FK to ItemProducts
	ItemRemaining INT NOT NULL,
	InventoryID INT NOT NULL UNIQUE IDENTITY
);

CREATE TABLE Project0.ItemProducts (
	ItemID INT NOT NULL UNIQUE IDENTITY,
	ItemName NVARCHAR(100) NOT NULL,
	ItemPrice MONEY NOT NULL,
	ItemDescription NVARCHAR(200),
)

-- add primary keys
ALTER TABLE Project0.Location   
	ADD CONSTRAINT PK_Location_ID PRIMARY KEY CLUSTERED (LocationID);
ALTER TABLE Project0.Customer   
	ADD CONSTRAINT PK_Customer_ID PRIMARY KEY CLUSTERED (CustomerID);
ALTER TABLE Project0.StoreOrder   
	ADD CONSTRAINT PK_Order_ID PRIMARY KEY CLUSTERED (OrderID);
ALTER TABLE Project0.ItemProducts   
	ADD CONSTRAINT PK_Item_ID PRIMARY KEY CLUSTERED (ItemID);

-- add primary keys to my junction table
ALTER TABLE Project0.Inventory
	ADD CONSTRAINT PK_Inventory_ID PRIMARY KEY CLUSTERED (InventoryID);
ALTER TABLE Project0.OrderList
	ADD CONSTRAINT PK_OrderList_ID PRIMARY KEY CLUSTERED (OrderListID);


-- add FK
-- store to location
ALTER TABLE Project0.Inventory 
	ADD CONSTRAINT FK_Store_Location_ID FOREIGN KEY (StoreID) REFERENCES Project0.Location (LocationID);

-- inventory to itemProduct
ALTER TABLE Project0.Inventory 
	ADD CONSTRAINT FK_Inventory_Item_ID FOREIGN KEY (ItemID) REFERENCES Project0.ItemProducts (ItemID);

-- Order to Customer
ALTER TABLE Project0.StoreOrder 
	ADD CONSTRAINT FK_Order_Customer_ID FOREIGN KEY (CustomerID) REFERENCES Project0.Customer (CustomerID);
-- Order to Location
ALTER TABLE Project0.StoreOrder 
	ADD CONSTRAINT FK_Order_Location_ID FOREIGN KEY (StoreID) REFERENCES Project0.Location (LocationID);

-- customer to location
ALTER TABLE Project0.Customer 
	ADD CONSTRAINT FK_Customer_Location_ID FOREIGN KEY (DefaultStoreID) REFERENCES Project0.Location (LocationID);

-- orderlist to order
ALTER TABLE Project0.OrderList 
	ADD CONSTRAINT FK_Orderlist_Order_ID FOREIGN KEY (OrderID) REFERENCES Project0.StoreOrder (OrderID);
-- orderlist to Item
ALTER TABLE Project0.OrderList 
	ADD CONSTRAINT FK_Orderlist_Item_ID FOREIGN KEY (ItemID) REFERENCES Project0.ItemProducts (ItemID);

SET IDENTITY_INSERT Project0.Project0 off;
SET IDENTITY_INSERT Project0.Inventory off;
SET IDENTITY_INSERT Project0.Customer off;
SET IDENTITY_INSERT Project0.Project0.Location off;
SET IDENTITY_INSERT Project0.Project0.StoreOrder off;
SET IDENTITY_INSERT Project0.Project0.OrderList on;
SET IDENTITY_INSERT Project0.Project0.ItemProducts off;


-- add some things into the DB now

-- inserts for store
INSERT Project0.Location(StoreName)
	VALUES('MallWart')
INSERT Project0.Location(StoreName)
	VALUES('CJNickel')
INSERT Project0.Location(StoreName)
	VALUES('Apple Republic')

SELECT * FROM Project0.Location;

-- inserts for customer
INSERT Project0.Customer(CustomerID, FirstName, LastName, DefaultStoreID)
	VALUES(1, 'John', 'Lenin', 1);
INSERT Project0.Customer(CustomerID, FirstName, LastName, DefaultStoreID)
	VALUES(2, 'Steve', 'Gates', 2);
INSERT Project0.Customer(CustomerID, FirstName, LastName, DefaultStoreID)
	VALUES(3, 'Chew', 'Baka', 3);
INSERT Project0.Customer(CustomerID, FirstName, LastName, DefaultStoreID)
	VALUES(4, 'Joe', 'Smith', 1);

SELECT * FROM Project0.Customer;

-- insert ItemProducts
Insert Project0.ItemProducts(ItemName, ItemDescription, ItemPrice)
	VALUES('shirt', 'short sleeves t-shirt', 15.00)
Insert Project0.ItemProducts(ItemName, ItemDescription, ItemPrice)
	VALUES('pants', 'shorts up to the knees', 35.00)
Insert Project0.ItemProducts(ItemName, ItemDescription, ItemPrice)
	VALUES('shoes', 'standard shoes, not Wide', 55.00)

SELECT * FROM Project0.ItemProducts;

-- insert into StoreOrder
INSERT Project0.StoreOrder(StoreID, CustomerID, DatePurchased, Total)
	VALUES (1, 1, '2019-03-01 12:30:30', 185.00)
INSERT Project0.StoreOrder(StoreID, CustomerID, DatePurchased, Total)
	VALUES (2, 2, '2019-03-02 14:30:30', 115.00)
INSERT Project0.StoreOrder(StoreID, CustomerID, DatePurchased, Total)
	VALUES (3, 3, '2019-03-02 08:30:30', 105.00)
INSERT Project0.StoreOrder(StoreID, CustomerID, DatePurchased, Total)
	VALUES (1, 4, '2019-03-03 11:40:30', 85.00)

-- dummy test
INSERT Project0.StoreOrder(StoreID, CustomerID, DatePurchased, Total)
	VALUES (2, 3, '2019-03-08 01:40:30', 00.00)

---- haven't ran yet
INSERT Project0.StoreOrder(StoreID, CustomerID, DatePurchased, Total)
	VALUES (3, 3, '2019-03-11 02:25:30', 00.00)

SELECT * FROM Project0.StoreOrder;

-- insert into OrderList
-- add OrderListID
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(1, 1, 4)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(1, 2, 2)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(1, 3, 1)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(2, 1, 3)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(2, 2, 2)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(3, 3, 1)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(4, 1, 1)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(3, 2, 1)

Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(5, 1, 1)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(5, 2, 1)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(5, 3, 1)

-- more test data
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(6, 2, 1)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(6, 3, 3)

-- haven't ran yet
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(6, 1, 5)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(6, 2, 3)
Insert Project0.OrderList(OrderID, ItemID, ItemBought)
	VALUES(6, 3, 3)

SELECT * FROM Project0.OrderList;

-- display sum of orders
Select SO.OrderID, SUM(ItemPrice * ItemBought) as sumOfOrders
FROM Project0.StoreOrder as SO
	JOIN Project0.OrderList as OL on SO.OrderID = OL.OrderID
	JOIN Project0.ItemProducts as IP on OL.ItemID = IP.ItemID
	--WHERE OL.CustomerID = SO.OrderID
	GROUP BY SO.OrderID

-- edit total to match
UPDATE Project0.StoreOrder
SET Total = 345.00
WHERE OrderID = 6

select * FROM Project0.StoreOrder

-- insert into inventory
-- add InventoryID
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(1, 1, 50)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(1, 2, 20)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(1, 2, 10)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(2, 1, 50)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(2, 2, 20)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(2, 2, 10)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(3, 1, 50)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(3, 2, 20)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining)
	VALUES(3, 2, 10)

Select * FROM Project0.Inventory;


Select * From Project0.Customer;
Select * From Project0.Inventory;
Select * From Project0.ItemProducts;
Select * From Project0.Location;
Select * From Project0.OrderList;
Select * From Project0.StoreOrder;
