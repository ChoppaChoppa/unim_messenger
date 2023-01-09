CREATE TABLE user (
    id SERIAL PRIMARY KEY,
    login VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    role INTEGER NOT NULL,
    reg_date TIMESTAMPTZ DEFAULT now()
);

CREATE TABLE student (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES user(id),
    f_name VARCHAR(50) NOT NULL,
    l_name VARCHAR(50) NOT NULL,
    age INTEGER NOT NULL,
    course INTEGER NOT NULL,
    group VARCHAR NOT NULL
);

CREATE TABLE teacher (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES user(id),
    f_name VARCHAR(50) NOT NULL,
    l_name VARCHAR(50) NOT NULL,
    age INTEGER NOT NULL,
    position VARCHAR NOT NULL
);