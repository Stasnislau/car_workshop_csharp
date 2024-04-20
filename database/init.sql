CREATE DATABASE carWorkshop;

\c carWorkshop;

CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    name VARCHAR NOT NULL,
    hourlyRate DECIMAL(10, 2) NOT NULL
);

CREATE TABLE tickets (
    id SERIAL PRIMARY KEY,
    brand VARCHAR NOT NULL,
    model VARCHAR NOT NULL,
    registrationId VARCHAR NOT NULL,
    problemDescription TEXT NOT NULL,
    state TEXT NOT NULL DEFAULT 'Created',
    employeeId INTEGER NOT NULL,
    createdAt TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    estimateDescription TEXT,
    estimateCost DECIMAL(10, 2),
    estimateAccepted BOOLEAN DEFAULT FALSE,
    pricePaid DECIMAL(10, 2),
    FOREIGN KEY (employeeId) REFERENCES employees(id)
);

CREATE TABLE timeSlots (
    id SERIAL PRIMARY KEY,
    startTime TIMESTAMP WITH TIME ZONE NOT NULL,
    endTime TIMESTAMP WITH TIME ZONE NOT NULL,
    employeeId INTEGER,
    ticketId INTEGER,
    FOREIGN KEY (employeeId) REFERENCES employees(id),
    FOREIGN KEY (ticketId) REFERENCES tickets(id)
);

-- Create the parts table
CREATE TABLE parts (
    id SERIAL PRIMARY KEY,
    name VARCHAR NOT NULL,
    amount DECIMAL(10, 2) NOT NULL,
    unitPrice DECIMAL(10, 2) NOT NULL,
    totalPrice DECIMAL(10, 2) NOT NULL,
    ticketId INTEGER,
    FOREIGN KEY (ticketId) REFERENCES tickets(id)
);
