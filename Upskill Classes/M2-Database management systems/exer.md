# SQL Exercises

1. Which shippers do we have? We have a table called Shippers. Return all the fields from all the shippers.
2. In the Categories table, selecting all the fields using this SQL: "Select * from Categories" will return 4 columns. We only want to see two columns, CategoryName and Description.
3. We’d like to see just the FirstName, LastName, and HireDate of all the employees with the Title of Sales Representative. Write a SQL statement that returns only those employees.
4. Now we’d like to see the same columns as above, but only for those employees that both have the title of Sales Representative, and also are in the United States.
5. Show all the orders placed by a specific employee. The EmployeeID for this Employee (Steven Buchanan) is 5.
6. In the Suppliers table, show the SupplierID, ContactName, and ContactTitle for those Suppliers whose ContactTitle is not Marketing Manager.
7. In the products table, we’d like to see the ProductID and ProductName for those products where the ProductName includes the string “queso”.
8. Looking at the Orders table, there’s a field called ShipCountry. Write a query that shows the OrderID, CustomerID, and ShipCountry for the orders where the ShipCountry is either France or Belgium.
9. Now, instead of just wanting to return all the orders from France of Belgium, we want to show all the orders from any Latin American country. But we don’t have a list of Latin American countries in a table in the Northwind database. So, we’re going to just use this list of Latin American countries that happen to be in the Orders table:
- Brazil
- Mexico
- Argentina
- Venezuela
It doesn’t make sense to use multiple Or statements anymore, it would get too convoluted. Use the In statement.
10. For all the employees in the Employees table, show the FirstName, LastName, Title, and BirthDate. Order the results by BirthDate, so we have the oldest employees first.
11. In the output of the query above, showing the Employees in order of BirthDate, we see the time of the BirthDate field, which we don’t want. Show only the date portion of the BirthDate field.
12. Show the FirstName and LastName columns from the Employees table, and then create a new column called FullName, showing FirstName and LastName joined together in one column, with a space in-between.
13. In the OrderDetails table, we have the fields UnitPrice and Quantity. Create a new field, TotalPrice, that multiplies these two together. We’ll ignore the Discount field for now.
In addition, show the OrderID, ProductID, UnitPrice, and Quantity. Order by OrderID and ProductID.
14. How many customers do we have in the Customers table? Show one value only, and don’t rely on getting the recordcount at the end of a resultset.
15. Show the date of the first order ever made in the Orders table.
16. Show a list of countries where the Northwind company has customers.
17. Show a list of all the different values in the Customers table for ContactTitles. Also include a count for each ContactTitle. This is similar in concept to the previous question “Countries where there are customers”, except we now want a count for each ContactTitle.
18. We’d like to show, for each product, the associated Supplier. Show the ProductID, ProductName, and the CompanyName of the Supplier. Sort by ProductID. 
19. We’d like to show a list of the Orders that were made, including the Shipper that was used. Show the OrderID, OrderDate (date only), and CompanyName of the Shipper, and sort by OrderID. In order to not show all the orders (there’s more than 800), show only those rows with an OrderID of less than 10300.
20. For this problem, we’d like to see the total number of products in each category. Sort the results by the total number of products, in descending order.
21. In the Customers table, show the total number of customers per Country and City.
22. What products do we have in our inventory that should be reordered? For now, just use the fields UnitsInStock and ReorderLevel, where UnitsInStock is less than the ReorderLevel, ignoring the fields UnitsOnOrder and Discontinued. Order the results by ProductID.
23. Now we need to incorporate these fields—UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued—into our calculation. We’ll define “products that need reordering” with the following:
- UnitsInStock plus UnitsOnOrder are less than or equal to ReorderLevel
- The Discontinued flag is false (0).
24. A salesperson for Northwind is going on a business trip to visit customers, and would like to see a list of all customers, sorted by region, alphabetically. However, he wants the customers with no region (null in the Region field) to be at the end, instead of at the top, where you’d normally find the null values. Within the same region, companies should be sorted by CustomerID.
25. Some of the countries we ship to have very high freight charges. We'd like to investigate some more shipping options for our customers, to be able to offer them lower freight charges. Return the three ship countries with the highest average freight overall, in descending order by average freight.
26. We're continuing on the question above on high freight charges. Now, instead of using all the orders we have, we only want to see orders from the year 2015.
27. Another (incorrect) answer to the problem above is this:
```
Select Top 3
    ShipCountry
    ,AverageFreight = avg(freight)
From Orders
Where
    OrderDate between '1/1/2015' and '12/31/2015'
Group By ShipCountry
Order By AverageFreight desc;
```
Notice when you run this, it gives Sweden as the ShipCountry with the third highest freight charges. However, this is wrong - it should be France. What is the OrderID of the order that the (incorrect) answer above is missing?

28. We're continuing to work on high freight charges. We now want to get the three ship countries with the highest average freight charges. But instead of filtering for a particular year, we want to use the last 12 months of order data, using as the end date the last OrderDate in Orders.
29. We're doing inventory, and need to show information like the below, for all orders. Sort by OrderID and Product ID.

EmployeeID |  LastName | OrderID | ProductName | Quantity |
-----------|-----------|---------|-------------|----------|
5 | Buchanan | 10248 | Queso Cabrales | 12
5 |          Buchanan     |        10248  |     Singaporean Hokkien Fried Mee         |   10
5 |         Buchanan      |      10248  |    Mozzarella di Giovanni             |      5
6 |         Suyama        |      10249  |    Tofu                               |      9
6 |         Suyama        |      10249  |    Manjimup Dried Apples              |      40
4 |         Peacock       |      10250  |    Jack's New England Clam Chowder    |      10
4 |         Peacock       |      10250  |    Manjimup Dried Apples              |      35
4 |         Peacock       |      10250  |    Louisiana Fiery Hot Pepper Sauce   |      15
3 |         Leverling     |      10251  |    Gustaf's Knäckebröd                |      6
3 |         Leverling     |      10251  |    Ravioli Angelo                     |      15
3 |         Leverling     |      10251  |    Louisiana Fiery Hot Pepper Sauce   |      20
4 |         Peacock       |      10252  |    Sir Rodney's Marmalade             |      40
4 |         Peacock       |      10252  |    Geitost                            |      25
4 |         Peacock       |      10252  |    Camembert Pierrot                  |      40
3 |         Leverling     |      10253  |    Gorgonzola Telino                  |      20
3 |         Leverling     |      10253  |    Chartreuse verte                   |      42
3 |         Leverling     |      10253  |    Maxilaku                           |      40
…
(total 2155 rows)”

30.  There are some customers who have never actually placed an order. Show these customers.
31.  One employee (Margaret Peacock, EmployeeID 4) has placed the most orders. However, there are some customers who've never placed an order with her. Show only those customers who have never placed an order with her.