

------------------------
--- 01 Structure
------------------------


----- 00_Functions.sql


-- Set the UpdatedOn Column to the current date
CREATE OR REPLACE FUNCTION SetUpdatedOnColumn()
RETURNS TRIGGER AS $$
BEGIN
    NEW.UpdatedOn = (NOW() AT TIME ZONE 'utc');
    RETURN NEW;   
END;
$$ language 'plpgsql';

-- Set the UpdatedOn and CreatedOn Columns to the current date
CREATE OR REPLACE FUNCTION SetCreatedOnAndUpdatedOnColumn()
RETURNS TRIGGER AS $$
BEGIN
    NEW.CreatedOn = (NOW() AT TIME ZONE 'utc');
    NEW.UpdatedOn = (NOW() AT TIME ZONE 'utc');
    RETURN NEW;   
END;
$$ language 'plpgsql';



----- 01_Users.sql


-- Drop triggers
DROP TRIGGER IF EXISTS UpdateUpdatedOnColumnOnUpdateUser          ON "User";
DROP TRIGGER IF EXISTS SetCreatedOnAndUpdatedOnColumnOnInsertUser ON "User";

-- Drop all tables
DROP TABLE IF EXISTS "User";
DROP TABLE IF EXISTS UserState;

-- Create User State table
CREATE TABLE UserState (
	Id  	SMALLINT PRIMARY KEY NOT NULL,
	Name 	VARCHAR(20) NOT NULL UNIQUE
);

-- Create User State table
CREATE TABLE UserRole (
	Id  	SMALLINT PRIMARY KEY NOT NULL,
	Name 	VARCHAR(20) NOT NULL UNIQUE
);

-- Create user table
CREATE TABLE "User" (
	Id 					UUID 			NOT NULL,
	Name				VARCHAR(255)	NOT NULL,
	Username			VARCHAR(255)	NOT NULL,
	Password			VARCHAR(255)	NOT NULL,
	PasswordSalt		VARCHAR(128)	NOT NULL,
	State				SMALLINT		NOT NULL,
	Role				SMALLINT			NOT NULL,
    LastAuthentication	TIMESTAMP WITHOUT TIME ZONE,                                    -- UTC FORMAT
	CreatedOn 			TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
	UpdatedOn           TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
    IsDeleted           BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (Id),
	FOREIGN KEY (State) REFERENCES UserState(Id),
	FOREIGN KEY (Role) REFERENCES UserRole(Id)
);


-- Create triggers to set and/or update the UpdatedOn and CreatedOn columns of the User table
CREATE TRIGGER UpdateUpdatedOnColumnOnUpdateUser          BEFORE UPDATE ON "User" FOR EACH ROW EXECUTE PROCEDURE  SetUpdatedOnColumn();
CREATE TRIGGER SetCreatedOnAndUpdatedOnColumnOnInsertUser BEFORE INSERT ON "User" FOR EACH ROW EXECUTE PROCEDURE  SetCreatedOnAndUpdatedOnColumn();



----- 02_SessionToken.sql



-- Session Token Table
CREATE TABLE SessionToken (
    Token TEXT,
    ExpirationDate TIMESTAMP WITHOUT TIME ZONE,
    UserId UUID,
    PRIMARY KEY (Token),
    FOREIGN KEY(userId) REFERENCES "User"(Id)
);

-- Procedure to remove all expired tokens
CREATE OR REPLACE FUNCTION DeleteExpiredSessionTokens()
RETURNS TRIGGER AS $$
BEGIN
    DELETE FROM SessionToken WHERE ExpirationDate > NOW();
    RETURN NEW;   
END;
$$ language 'plpgsql';

-- Trigger to run the expired tokens clean up on every new token insert
CREATE TRIGGER DeleteExpiredSessionTokens BEFORE INSERT ON SessionToken FOR EACH ROW EXECUTE PROCEDURE  DeleteExpiredSessionTokens();


----- 04_LogException.sql

-- Drop all tables
DROP TABLE IF EXISTS LogException;

--Create LogException table
CREATE TABLE LogException (
	Id 			UUID  NOT NULL,
	Exception	TEXT,
	CreatedOn   TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
	PRIMARY KEY (Id)
);


------------------------
--- 02 Initial Data
------------------------


----- 01_Users.sql


-- User State
INSERT INTO UserState(Id, Name) VALUES (1, 'Active');
INSERT INTO UserState(Id, Name) VALUES (2, 'Inactive');

-- User State
INSERT INTO UserRole(Id, Name) VALUES (1, 'Operator');
INSERT INTO UserRole(Id, Name) VALUES (2, 'Administrator');

-- Creates Admin
INSERT INTO "User"(id, name, username, password, passwordsalt, state, role, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27b794', 'Admin', 'admin', 'F1F8795E1954D229C868231C419E0D2D5FC3D71F1405C33F4D0F6949604FC1EB0BBB4CE3A9987C821DA2921CFBB3807765FD3C4FD15FA84FC4C8DA59DA2B066E', 'E734C8335A75AA0E1C433F3F4CD6501F61C05637845FEB958694C165E2787F8E3DDF2880FC3C15B467971ECA079103879963CFB41D96A30C0E7F565254C435B9', 1, 2, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);




