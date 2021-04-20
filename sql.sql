CREATE TABLE Clients (
    id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    name nvarchar(255),
    address nvarchar(255),
    post_code varchar(255 )

);

CREATE TABLE Logs(
    id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    time datetime default CURRENT_TIMESTAMP, 
    action nvarchar(255),
    record nvarchar(255)

);