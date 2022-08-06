s--1 Which shippers do we have? We have a table called Shippers. Return all the fields from all the shippers.
SELECT * 
	FROM Shippers;

--2 In the Categories table, selecting all the fields using this SQL: "Select * from Categories" will return 4 columns. We only want to see two columns, CategoryName and Description.
SELECT CategoryName, Description
	 FROM Categories;

--3 We�d like to see just the FirstName, LastName, and HireDate of all the employees with the Title of Sales Representative. Write a SQL statement that returns only those employees.
SELECT * 
	FROM Employees 
		WHERE Title = 'Sales Representative';

--4 Now we�d like to see the same columns as above, but only for those employees that both have the title of Sales Representative, and also are in the United States.
SELECT * 
	FROM Employees 
		WHERE Title = 'Sales Representative'and country = 'USA';

--5 Show all the orders placed by a specific employee. The EmployeeID for this Employee (Steven Buchanan) is 5.
SELECT * 
	FROM Orders 
		WHERE EmployeeID = '5';

--6 In the Suppliers table, show the SupplierID, ContactName, and ContactTitle for those Suppliers whose ContactTitle is not Marketing Manager.
SELECT SupplierID, ContactName, ContactTitle 
	FROM Suppliers 
		WHERE ContactTitle != 'Marketing Manager';

--7 In the products table, we�d like to see the ProductID and ProductName for those products where the ProductName includes the string �queso�.
SELECT ProductID, ProductName 
	FROM Products 
		WHERE ProductName LIKE '%queso%';

--8 Looking at the Orders table, there�s a field called ShipCountry. Write a query that shows the OrderID, CustomerID, and ShipCountry for the orders where the ShipCountry is either France or Belgium.
SELECT OrderID, CustomerID, ShipCountry 
	FROM ORDERS 
		WHERE ShipCountry = 'Belgium' OR ShipCountry = 'France';

--9 Now, instead of just wanting to return all the orders from France of Belgium, we want to show all the orders from any Latin American country. But we don�t have a list of Latin American countries in a table in the Northwind database. So, we�re going to just use this list of Latin American countries that happen to be in the Orders table:
-- Brazil
-- Mexico
-- Argentina
-- Venezuela It doesn�t make sense to use multiple Or statements anymore, it would get too convoluted. Use the In statement.

-- DUAS INTERPRETA��ES DO TEXTO, N�MERO 1:

SELECT OrderID, CustomerID, ShipCountry 
	FROM ORDERS 
		WHERE ShipCountry IN ('Brazil', 'Mexico', 'Argentina', 'Venezuela');

-- N�MERO 2:

SELECT * 
	FROM ORDERS 
		WHERE ShipCountry IN ('Brazil', 'Mexico', 'Argentina', 'Venezuela');

--10 For all the employees in the Employees table, show the FirstName, LastName, Title, and BirthDate. Order the results by BirthDate, so we have the oldest employees first.
SELECT FirstName, LastName, Title, BirthDate 
	FROM Employees 
		ORDER BY BirthDate ASC;

--11 In the output of the query above, showing the Employees in order of BirthDate, we see the time of the BirthDate field, which we don�t want. Show only the date portion of the BirthDate field.
SELECT FirstName, LastName, Title, CAST(BirthDate AS date) 
	FROM Employees 
		ORDER BY BirthDate ASC;

--12 Show the FirstName and LastName columns from the Employees table, and then create a new column called FullName, showing FirstName and LastName joined together in one column, with a space in-between.
SELECT CONCAT(FirstName, ' ',LastName) AS FullName 
	FROM Employees

--13 In the OrderDetails table, we have the fields UnitPrice and Quantity. Create a new field, TotalPrice, that multiplies these two together. We�ll ignore the Discount field for now. In addition, show the OrderID, ProductID, UnitPrice, and Quantity. Order by OrderID and ProductID.
--DBL
ALTER TABLE OrderDetails
	ADD TotalPrice decimal(10,2)
UPDATE OrderDetails
	SET TotalPrice =UnitPrice * Quantity
SELECT OrderID, ProductID, UnitPrice, Quantity 
	FROM OrderDetails 
		ORDER BY OrderID, ProductID ASC;

--DLM
SELECT UnitPrice * Quantity AS TotalPrice
	FROM OrderDetails
SELECT OrderID, ProductID, UnitPrice, Quantity 
	FROM OrderDetails 
		ORDER BY OrderID, ProductID ASC;


--14 How many customers do we have in the Customers table? Show one value only, and don�t rely on getting the recordcount at the end of a resultset.
SELECT COUNT (CustomerID)
	FROM Customers;

--15 Show the date of the first order ever made in the Orders table.
SELECT TOP 1 OrderDate 
	FROM Orders
		ORDER BY OrderDate ASC;

--16 Show a list of countries where the Northwind company has customers.
SELECT DISTINCT Country 
	FROM Customers;

--17 Show a list of all the different values in the Customers table for ContactTitles. Also include a count for each ContactTitle. This is similar in concept to the previous question �Countries where there are customers�, except we now want a count for each ContactTitle.
SELECT DISTINCT ContactTitle 
	FROM Customers
SELECT COUNT(DISTINCT ContactTitle)AS ContactTitles
	FROM Customers;

--18 We�d like to show, for each product, the associated Supplier. Show the ProductID, ProductName, and the CompanyName of the Supplier. Sort by ProductID.
SELECT ProductID, ProductName, CompanyName
	FROM Products
		INNER JOIN Suppliers ON Products.SupplierID = Suppliers.SupplierID
			ORDER BY ProductID ASC;

--19 We�d like to show a list of the Orders that were made, including the Shipper that was used. Show the OrderID, OrderDate (date only), and CompanyName of the Shipper, and sort by OrderID. In order to not show all the orders (there�s more than 800), show only those rows with an OrderID of less than 10300.
SELECT OrderID, cast(OrderDate AS date), CompanyName
	FROM Orders
		INNER JOIN Shippers ON Orders.ShipVia = Shippers.ShipperID
			WHERE OrderID < 10300
				ORDER BY OrderID ASC;

--20 For this problem, we�d like to see the total number of products in each category. Sort the results by the total number of products, in descending order.
SELECT COUNT(DISTINCT ProductName) AS QuantityofProduct, CategoryName 
	FROM Products
		INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID
			GROUP BY CategoryName
				ORDER BY QuantityofProduct DESC;

--21 In the Customers table, show the total number of customers per Country and City.
SELECT Country, COUNT (Country) AS customersfromcountry, City, COUNT (City) AS customersfromcity 
	FROM Customers
		GROUP BY Country, City;

--22 What products do we have in our inventory that should be reordered? For now, just use the fields UnitsInStock and ReorderLevel, where UnitsInStock is less than the ReorderLevel, ignoring the fields UnitsOnOrder and Discontinued. Order the results by ProductID.
SELECT ProductName, ProductID, UnitsInStock 
	FROM PRODUCTS WHERE UnitsinStock < ReorderLevel
		ORDER BY ProductID ASC;

--23 Now we need to incorporate these fields—UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued—into our calculation. We’ll define “products that need reordering” with the following:
--UnitsInStock plus UnitsOnOrder are less than or equal to ReorderLevel
--The Discontinued flag is false (0).
SELECT ProductName AS ProductsThatNeedReordering 
	FROM PRODUCTS 
		WHERE UnitsinStock + UnitsOnOrder <= ReorderLevel AND Discontinued = 0;

-- 24 A salesperson for Northwind is going on a business trip to visit customers, and would like to see a list of all customers, sorted by region, alphabetically. However, he wants the customers with no region (null in the Region field) to be at the end, instead of at the top, where you’d normally find the null values. Within the same region, companies should be sorted by CustomerID.
SELECT *
	FROM CUSTOMERS
		ORDER BY (CASE WHEN Region IS NULL THEN 1 ELSE 0 END), Region, CustomerID ASC;

-- 25 Some of the countries we ship to have very high freight charges. We'd like to investigate some more shipping options for our customers, to be able to offer them lower freight charges. Return the three ship countries with the highest average freight overall, in descending order by average freight.
SELECT TOP 3 ShipCountry, AVG(Freight) AS media 
	FROM Orders
		GROUP BY ShipCountry
			ORDER BY AVG(Freight) DESC;

-- 26 We're continuing on the question above on high freight charges. Now, instead of using all the orders we have, we only want to see orders from the year 2015.
SELECT TOP 3 ShipCountry, AVG(Freight) AS media 
	FROM Orders
		WHERE YEAR(ShippedDate) = '2015'
			GROUP BY ShipCountry
				ORDER BY AVG(Freight) DESC;

-- 27
-- faltam as ordens do dia 31, que não estão incluídas no " OrderDate between '1/1/2015' and '12/31/2015'". Devemos, portanto, encontrar quais ordens foram feitas no dia 31.
SELECT *
	FROM Orders
		WHERE CAST (OrderDate as date) = '2015-12-31';

-- A ordem relevante para este caso é a da França, portanto:
SELECT *
	FROM Orders
		WHERE CAST (OrderDate as date) = '2015-12-31' AND ShipCountry = 'France';

-- a OrderID que falta é 10806

--28 We're continuing to work on high freight charges. We now want to get the three ship countries with the highest average freight charges. But instead of filtering for a particular year, we want to use the last 12 months of order data, using as the end date the last OrderDate in Orders.
SELECT TOP 3 ShipCountry, AVG(Freight) AS media 
	FROM Orders
		WHERE ShippedDate BETWEEN  DATEADD(month, -12, (SELECT Max(OrderDate) FROM Orders)) AND (Select Max(OrderDate) FROM Orders)
			GROUP BY ShipCountry
				ORDER BY AVG(Freight) DESC;

--29 We're doing inventory, and need to show information like the below, for all orders. Sort by OrderID and Product ID.
SELECT orders.OrderID, LastName, Quantity, ProductName, Employees.EmployeeID FROM Orders
	INNER JOIN Employees ON Employees.EmployeeID = Orders.EmployeeID 
	INNER JOIN OrderDetails ON Orders.OrderID = OrderDetails.OrderID
	INNER JOIN Products ON Products.ProductID = OrderDetails.ProductID
		ORDER BY OrderID, OrderDetails.ProductID ASC;

--30 There are some customers who have never actually placed an order. Show these customers.
SELECT * FROM CUSTOMERS
	LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
		WHERE OrderID IS NULL;

--31 One employee (Margaret Peacock, EmployeeID 4) has placed the most orders. However, there are some customers who've never placed an order with her. Show only those customers who have never placed an order with her.
SELECT DISTINCT(ContactName)
	FROM Customers
		LEFT JOIN Orders
		ON CUSTOMERS.CustomerID = Orders.CustomerID
			WHERE Customers.CustomerID NOT IN (SELECT CustomerID FROM Orders WHERE EmployeeID = 4);



