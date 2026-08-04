\c postgres

DROP DATABASE IF EXISTS library;

CREATE DATABASE library;

\c library

CREATE TABLE library_card (
    id UUID PRIMARY KEY,
    ssn VARCHAR(13),
    first_name VARCHAR(30),
    last_name VARCHAR(30),
    email VARCHAR(30),
    home_address VARCHAR(30),
    postal_code VARCHAR(30),
    city VARCHAR(30),
    active BOOLEAN,
    current_loan_amount INT,
    pin_code INT
);

CREATE TABLE book 
(
    id UUID PRIMARY KEY,
    isbn VARCHAR(20),
    title VARCHAR(30),
    author VARCHAR(30),
    publisher VARCHAR(30),
    pages INT,
    lang VARCHAR(30),
    release_date DATE,
    amount_available INT
);

CREATE TABLE loan
(
    id UUID PRIMARY KEY,
    library_card_id UUID references library_card(id),
    book_id UUID references book(id) 
);

CREATE TABLE staff
(
    id UUID PRIMARY KEY,
    username VARCHAR(30) UNIQUE NOT NULL,
    first_name VARCHAR(30),
    last_name VARCHAR(30),
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    token_hash TEXT
);