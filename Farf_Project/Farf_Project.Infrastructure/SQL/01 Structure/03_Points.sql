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
	State				SMALLINT		NOT NULL,                               -- UTC FORMAT
	CreatedOn 			TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
	UpdatedOn           TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
    IsDeleted           BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (Id),
	FOREIGN KEY (State) REFERENCES PointState(Id)
);

-- Create triggers to set and/or update the UpdatedOn and CreatedOn columns of the Point table
CREATE TRIGGER UpdateUpdatedOnColumnOnUpdatePoint          BEFORE UPDATE ON Point FOR EACH ROW EXECUTE PROCEDURE  SetUpdatedOnColumn();
CREATE TRIGGER SetCreatedOnAndUpdatedOnColumnOnInsertPoint BEFORE INSERT ON Point FOR EACH ROW EXECUTE PROCEDURE  SetCreatedOnAndUpdatedOnColumn();
