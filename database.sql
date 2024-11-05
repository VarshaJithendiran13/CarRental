create database CarRental
use CarRental

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(15),
    Role NVARCHAR(10) CHECK (Role IN ('Customer', 'Admin'))
);

-- Sample Data
INSERT INTO Users (FirstName, LastName, Email, Password, PhoneNumber, Role)
VALUES 
    ('Vishal', 'Bhaskaran', 'vishal.b@example.com', 'password123', '9876543210', 'Customer'),
    ('Madhuri', 'Kumar', 'madhuri.k@example.com', 'securePass', '8765432109', 'Customer'),
    ('Sanjay', 'Iyer', 'sanjay.i@example.com', 'password456', '9123456789', 'Customer'),
    ('Ananya', 'Rao', 'ananya.r@example.com', 'pass789', '9988776655', 'Customer'),
    ('Admin', 'User', 'admin@example.com', 'adminPass', '1112223333', 'Admin');

	CREATE TABLE Cars (
    CarID INT IDENTITY(1,1) PRIMARY KEY,
    Make NVARCHAR(50) NOT NULL,
    Model NVARCHAR(50) NOT NULL,
    Year INT NOT NULL,
    Specifications NVARCHAR(MAX),
    PricePerDay DECIMAL(10, 2) NOT NULL,
    AvailabilityStatus BIT NOT NULL DEFAULT 1,
    Location NVARCHAR(100) NOT NULL,
    ImageURL NVARCHAR(255)
);

-- Sample Data
INSERT INTO Cars (Make, Model, Year, Specifications, PricePerDay, AvailabilityStatus, Location, ImageURL)
VALUES 
    ('Maruti', 'Swift', 2021, 'Hatchback, Automatic, Petrol', 1200.00, 1, 'Mumbai', 'swift.jpg'),
    ('Hyundai', 'i20', 2022, 'Hatchback, Manual, Diesel', 1400.00, 1, 'Bangalore', 'i20.jpg'),
    ('Toyota', 'Innova', 2020, 'SUV, Automatic, Diesel', 2500.00, 1, 'Delhi', 'innova.jpg'),
    ('Honda', 'City', 2021, 'Sedan, Manual, Petrol', 1800.00, 1, 'Chennai', 'city.jpg'),
    ('Kia', 'Seltos', 2022, 'SUV, Automatic, Petrol', 2700.00, 1, 'Kolkata', 'seltos.jpg');


CREATE TABLE Reservations (
    ReservationID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    CarID INT NOT NULL,
    PickupDate DATE NOT NULL,
    DropoffDate DATE NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL,
    ReservationStatus NVARCHAR(15) CHECK (ReservationStatus IN ('Pending', 'Confirmed', 'Completed', 'Cancelled')),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CarID) REFERENCES Cars(CarID)
);

-- Sample Data
INSERT INTO Reservations (UserID, CarID, PickupDate, DropoffDate, TotalPrice, ReservationStatus)
VALUES 
    (1, 1, '2024-11-15', '2024-11-18', 3600.00, 'Confirmed'),
    (2, 2, '2024-12-05', '2024-12-10', 7000.00, 'Pending'),
    (3, 3, '2024-12-20', '2024-12-25', 12500.00, 'Confirmed'),
    (4, 4, '2024-12-01', '2024-12-04', 5400.00, 'Completed'),
    (5, 5, '2024-12-10', '2024-12-15', 13500.00, 'Cancelled');

CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    ReservationID INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATE NOT NULL,
    PaymentMethod NVARCHAR(20) CHECK (PaymentMethod IN ('Credit Card', 'Debit Card', 'PayPal', 'Net Banking')),
    Status NVARCHAR(10) CHECK (Status IN ('Pending', 'Completed')),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)
);
-- Step 1: Drop the existing CHECK constraint
ALTER TABLE Payments
DROP CONSTRAINT CK__Payments__Status__440B1D61;

-- Step 2: Add a new CHECK constraint with the updated condition
ALTER TABLE Payments
ADD CONSTRAINT CK_Payments_Status
CHECK (Status IN ('Pending', 'Completed', 'Cancelled'));

-- Sample Data
INSERT INTO Payments (ReservationID, Amount, PaymentDate, PaymentMethod, Status)
VALUES 
    (1, 3600.00, '2024-11-14', 'Credit Card', 'Completed'),
    (2, 7000.00, '2024-12-04', 'Debit Card', 'Pending'),
    (3, 12500.00, '2024-12-19', 'Credit Card', 'Completed'),
    (4, 5400.00, '2024-12-01', 'PayPal', 'Completed'),
    (5, 13500.00, '2024-12-09', 'Net Banking', 'Cancelled');

CREATE TABLE Reviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    CarID INT NOT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(MAX),
    ReviewDate DATE NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CarID) REFERENCES Cars(CarID)
);

-- Sample Data
INSERT INTO Reviews (UserID, CarID, Rating, Comment, ReviewDate)
VALUES 
    (1, 1, 5, 'Excellent car, very clean and smooth ride!', '2024-11-19'),
    (2, 2, 4, 'Good experience, but the car was slightly delayed.', '2024-12-12'),
    (3, 3, 5, 'Amazing service and the car was in great condition!', '2024-12-26'),
    (4, 4, 3, 'Decent, but could be better in terms of comfort.', '2024-12-06'),
    (5, 5, 4, 'Overall good experience.', '2024-12-16');

CREATE TABLE AdminReport (
    ReportID INT IDENTITY(1,1) PRIMARY KEY,
    ReportDate DATE NOT NULL,
    TotalReservations INT,
    TotalRevenue DECIMAL(15, 2),
    TopCars NVARCHAR(255),
    MostActiveUser NVARCHAR(255)
);

-- Sample Data
INSERT INTO AdminReport (ReportDate, TotalReservations, TotalRevenue, TopCars, MostActiveUser)
VALUES 
    ('2024-12-31', 50, 250000.00, 'Toyota Innova, Maruti Swift', 'Vishal Bhaskaran'),
    ('2025-01-31', 45, 230000.00, 'Honda City, Kia Seltos', 'Madhuri Kumar');
