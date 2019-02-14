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
