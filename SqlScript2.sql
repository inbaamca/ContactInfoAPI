

USE ContactInfo;

CREATE TABLE Contact(Id BIGINT PRIMARY KEY IDENTITY(1,1), FirstName NVARCHAR(500), LastName NVARCHAR(500), Email VARCHAR(200), 
PhoneNumber VARCHAR(100), Address VARCHAR(1000), City VARCHAR(100), State VARCHAR(100), Country VARCHAR(100), PostalCode VARCHAR(50))

INSERT INTO Contact(FirstName, LastName, Email, PhoneNumber, Address, City, State, Country, PostalCode)
SELECT 'Raju', 'M', 'raju@gmail.com', '12323233', 'Velachery', 'Chennai', 'TN', 'India', '600091' UNION
SELECT 'Bala', 'D', 'bala@gmail.com', '12321333', 'OMR', 'Chennai', 'TN', 'India', '600095' UNION
SELECT 'Prabhu', 'R', 'prabhu@gmail.com', '2323321333', 'T-Nagar', 'Chennai', 'TN', 'India', '600054' UNION
SELECT 'Mohan', 'R', 'mohan@gmail.com', '3233636212', 'Puthur', 'Chennai', 'TN', 'India', '625018'


