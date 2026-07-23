\c postgres

DROP DATABASE IF EXISTS library;

CREATE DATABASE library;

\c library

CREATE TABLE library_card (
    id UUID PRIMARY KEY,
    first_name VARCHAR(30),
    last_name VARCHAR(30),
    active BOOLEAN,
    current_loan_amount INT
);