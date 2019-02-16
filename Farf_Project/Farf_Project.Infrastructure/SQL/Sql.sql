

------------------------
--- 01 Structure
------------------------


----- 01_Functions.sql


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



----- 02_Users.sql

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

-- Create User table
CREATE TABLE "User" (
	Id 					UUID 			NOT NULL,
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



----- 03_Points.sql

-- Create Point State table
CREATE TABLE PointState (
	Id  	SMALLINT PRIMARY KEY NOT NULL,
	Name 	VARCHAR(20) NOT NULL UNIQUE
);

-- Create Point table
CREATE TABLE Point (
	Id 					UUID 			NOT NULL,
	Name				VARCHAR(255)	NOT NULL,
	Address				VARCHAR(255)	NOT NULL,	
	State				SMALLINT		NOT NULL,
    LastAuthentication	TIMESTAMP WITHOUT TIME ZONE,                                    -- UTC FORMAT
	CreatedOn 			TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
	UpdatedOn           TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
    IsDeleted           BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (Id),
	FOREIGN KEY (State) REFERENCES PointState(Id)
);

-- Create triggers to set and/or update the UpdatedOn and CreatedOn columns of the Point table
CREATE TRIGGER UpdateUpdatedOnColumnOnUpdatePoint          BEFORE UPDATE ON Point FOR EACH ROW EXECUTE PROCEDURE  SetUpdatedOnColumn();
CREATE TRIGGER SetCreatedOnAndUpdatedOnColumnOnInsertPoint BEFORE INSERT ON Point FOR EACH ROW EXECUTE PROCEDURE  SetCreatedOnAndUpdatedOnColumn();



----- 04_Routes.sql

-- Create Route State table
CREATE TABLE RouteState (
	Id  	SMALLINT PRIMARY KEY NOT NULL,
	Name 	VARCHAR(20) NOT NULL UNIQUE
);

-- Create Route table
CREATE TABLE "Route" (
	Id 					UUID 			NOT NULL,
	PointStart			UUID			NOT NULL,
	PointEnd			UUID			NOT NULL,
	RoutePrice			BIGSERIAL		NOT NULL,
	RouteTime			BIGSERIAL		NOT NULL,
	State				SMALLINT		NOT NULL,
    LastAuthentication	TIMESTAMP WITHOUT TIME ZONE,                                    -- UTC FORMAT
	CreatedOn 			TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
	UpdatedOn           TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
    IsDeleted           BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (Id),
	FOREIGN KEY (State) REFERENCES RouteState(Id)
);


-- Create triggers to set and/or update the UpdatedOn and CreatedOn columns of the Route table
CREATE TRIGGER UpdateUpdatedOnColumnOnUpdateRoute          BEFORE UPDATE ON "Route" FOR EACH ROW EXECUTE PROCEDURE  SetUpdatedOnColumn();
CREATE TRIGGER SetCreatedOnAndUpdatedOnColumnOnInsertRoute BEFORE INSERT ON "Route" FOR EACH ROW EXECUTE PROCEDURE  SetCreatedOnAndUpdatedOnColumn();



----- 08_SessionToken.sql



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


----- 09_LogException.sql

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
INSERT INTO "User" (id, username, password, passwordsalt, state, role, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27b794', 'admin', 'F1F8795E1954D229C868231C419E0D2D5FC3D71F1405C33F4D0F6949604FC1EB0BBB4CE3A9987C821DA2921CFBB3807765FD3C4FD15FA84FC4C8DA59DA2B066E', 'E734C8335A75AA0E1C433F3F4CD6501F61C05637845FEB958694C165E2787F8E3DDF2880FC3C15B467971ECA079103879963CFB41D96A30C0E7F565254C435B9', 1, 2, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);


----- 02_Points.sql


-- Point State
INSERT INTO PointState(Id, Name) VALUES (1, 'Active');
INSERT INTO PointState(Id, Name) VALUES (2, 'Inactive');

-- Creates Points
INSERT INTO Point(id, name, address, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27a123', 'Point1', 'Rua Divino Salvador, nº 51, Arentim, Braga', 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);
INSERT INTO Point(id, name, address, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27a124', 'Point2', 'Avenida da Boavista, nº 51, Porto', 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);
INSERT INTO Point(id, name, address, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27a125', 'Point3', 'Praça da Republica, Lisboa', 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);
INSERT INTO Point(id, name, address, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27a126', 'Point4', 'Avenida da Liberdade, nº 12, Braga', 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);
INSERT INTO Point(id, name, address, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27a127', 'Point5', 'Aliados, nº 51, Porto', 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);
INSERT INTO Point(id, name, address, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27a128', 'Point6', 'Praça da Republica, Porto', 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);


----- 03_Routes.sql


-- Route State
INSERT INTO RouteState(Id, Name) VALUES (1, 'Active');
INSERT INTO RouteState(Id, Name) VALUES (2, 'Inactive');

-- Creates Routes
INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c001', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a128', 156, 985, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c002', '755ea1f8-b453-4425-ae93-135a0f27a127', '755ea1f8-b453-4425-ae93-135a0f27a124', 2, 32, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c003', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a127', 32, 65, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c004', '755ea1f8-b453-4425-ae93-135a0f27a125', '755ea1f8-b453-4425-ae93-135a0f27a126', 45, 71, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c005', '755ea1f8-b453-4425-ae93-135a0f27a124', '755ea1f8-b453-4425-ae93-135a0f27a125', 26, 98, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c006', '755ea1f8-b453-4425-ae93-135a0f27a124', '755ea1f8-b453-4425-ae93-135a0f27a128', 14, 54, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c007', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a124', 16, 44, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c008', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a127', 87, 11, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c009', '755ea1f8-b453-4425-ae93-135a0f27a125', '755ea1f8-b453-4425-ae93-135a0f27a129', 33, 26, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, pointstart, pointend, routeprice, routetime, state, lastauthentication, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c010', '755ea1f8-b453-4425-ae93-135a0f27a126', '755ea1f8-b453-4425-ae93-135a0f27a127', 34, 25, 1, null, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);




