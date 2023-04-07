create database FPTBook;
go
Use FPTBook;
go
CREATE TABLE Categories (
    CategoriesID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CategoriesName VARCHAR(255)
);
go
CREATE TABLE Publishers (
    PublisherID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    PublisherName VARCHAR(255),
    PublisherAddress VARCHAR(255),
    PublisherPhoneNumber VARCHAR(20)
);
go
CREATE TABLE Books (
    BookID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CategoriesID INT,
    BookName VARCHAR(255),
    BookPrice DECIMAL(10, 2),
    Description TEXT,
    Image VARCHAR(255),
    BookCount INT,
    PublisherID INT,
    FOREIGN KEY (CategoriesID) REFERENCES Categories(CategoriesID),
    FOREIGN KEY (PublisherID) REFERENCES Publishers(PublisherID)
);
go
CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY IDENTITY(1,1),
    Address VARCHAR(255),
    PhoneNumber VARCHAR(20),
    FullName VARCHAR(255)
);
go
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    RoleName VARCHAR(50)
);
go
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    RoleID INT,
    Username VARCHAR(50),
    Password VARCHAR(50),
    Email VARCHAR(50),
    Address VARCHAR(255),
    PhoneNumber VARCHAR(20),
    Gender VARCHAR(10),
    Birthdate DATE,
    FullName VARCHAR(255),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
go
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Payment DECIMAL(10, 2),
    Status VARCHAR(20),
    OrderDate DATE,
    DeliveryDate DATE,
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
go


CREATE TABLE OrderDetails (
  --FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    --FOREIGN KEY (BookID) REFERENCES Books(BookID),        
    --CONSTRAINT OrderDetails PRIMARY KEY(OrderID, BookID),
	OrderID INT references Orders(OrderID),
	BookID INT references Books(BookID),
    Quantity INT,
    TotalPrice DECIMAL(10, 2),
	PRIMARY KEY(OrderID,BookID)
);
go
CREATE TABLE BookAuthors (
    BookID INT references Books(BookID),
    AuthorID INT references Authors(AuthorID),
	PRIMARY KEY(BookID,AuthorID)
    --CONSTRAINT BookAuthors PRIMARY KEY(BookID, AuthorID),
    --FOREIGN KEY (BookID) REFERENCES Books(BookID),
    --FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)

);
go
