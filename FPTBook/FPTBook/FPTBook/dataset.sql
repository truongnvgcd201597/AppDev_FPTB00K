INSERT INTO Categories (CategoryName)
VALUES ('Fiction'), ('Non-Fiction'), ('Biography'), ('History'), ('Science Fiction');

INSERT INTO Publishers (PublisherName, PublisherAddress, PublisherPhoneNumber)
VALUES ('Penguin Random House', '123 Main St, New York, NY', '555-555-1234'),
('HarperCollins Publishers', '456 Elm St, San Francisco, CA', '555-555-5678'),
('Simon & Schuster', '789 Oak St, Chicago, IL', '555-555-9101');

INSERT INTO Books (CategoryID, BookName, BookPrice, Description, Image, BookCount, PublisherID)
VALUES (1, 'To Kill a Mockingbird', 10.99, 'A Pulitzer Prize-winning novel set in the Deep South.', 'https://example.com/to-kill-a-mockingbird.jpg', 100, 1),
(2, 'A Brief History of Time', 8.99, 'A popular science book by Stephen Hawking.', 'https://example.com/a-brief-history-of-time.jpg', 50, 2),
(3, 'Steve Jobs: The Biography', 14.99, 'A biography of Apple co-founder Steve Jobs.', 'https://example.com/steve-jobs-biography.jpg', 75, 3),
(4, 'The Guns of August', 12.99, 'A Pulitzer Prize-winning book about World War I.', 'https://example.com/the-guns-of-august.jpg', 25, 1),
(5, 'Dune', 11.99, 'A classic science fiction novel by Frank Herbert.', 'https://example.com/dune.jpg', 40, 2);

INSERT INTO Authors (Address, PhoneNumber, FullName)
VALUES ('123 Main St, New York, NY', '555-555-1111', 'Harper Lee'),
('456 Elm St, San Francisco, CA', '555-555-2222', 'Stephen Hawking'),
('789 Oak St, Chicago, IL', '555-555-3333', 'Walter Isaacson');

INSERT INTO Roles (RoleName)
VALUES ('Admin'), ('User'), ('Shop owner');

INSERT INTO Users (RoleID, Username, Password, Email, Address, PhoneNumber, Gender, Birthdate, FullName)
VALUES (1, 'admin', 'password123', 'admin@example.com', '123 Main St, New York, NY', '555-555-4444', 'Male', '1990-01-01', 'John Doe'),
(2, 'user1', 'password456', 'user1@example.com', '456 Elm St, San Francisco, CA', '555-555-5555', 'Female', '1995-05-05', 'Jane Doe');

INSERT INTO Orders (Payment, Status, OrderDate, DeliveryDate, UserID)
VALUES (50.99, 'Complete', '2023-04-01', '2023-04-05', 2),
(25.99, 'Pending', '2023-04-05', '2023-04-05', 2);

INSERT INTO OrderDetails (OrderID, BookID, Quantity, TotalPrice)
VALUES (1, 1, 2, 21.98),
(1, 2, 1, 8.99),
(2, 3, 3, 44.97);

INSERT INTO BookAuthors (BookID, AuthorID)
VALUES (1, 1),
(2, 2),
(3, 3),
(4, 1),
(5, 2)


