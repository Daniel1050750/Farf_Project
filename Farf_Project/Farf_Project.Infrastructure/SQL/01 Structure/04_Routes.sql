-- Create Route State table
CREATE TABLE RouteState (
	Id  	SMALLINT PRIMARY KEY NOT NULL,
	Name 	VARCHAR(20) NOT NULL UNIQUE
);

-- Create Route table
CREATE TABLE "Route" (
	Id 					UUID 			NOT NULL,
	Name				VARCHAR(255)	NOT NULL,
	PointStart			UUID			NOT NULL,
	PointEnd			UUID			NOT NULL,
	RoutePrice			BIGSERIAL		NOT NULL,
	RouteTime			BIGSERIAL		NOT NULL,
	State				SMALLINT		NOT NULL,                                -- UTC FORMAT
	CreatedOn 			TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
	UpdatedOn           TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
    IsDeleted           BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (Id),
	FOREIGN KEY (State) REFERENCES RouteState(Id)
);


-- Create triggers to set and/or update the UpdatedOn and CreatedOn columns of the Route table
CREATE TRIGGER UpdateUpdatedOnColumnOnUpdateRoute          BEFORE UPDATE ON "Route" FOR EACH ROW EXECUTE PROCEDURE  SetUpdatedOnColumn();
CREATE TRIGGER SetCreatedOnAndUpdatedOnColumnOnInsertRoute BEFORE INSERT ON "Route" FOR EACH ROW EXECUTE PROCEDURE  SetCreatedOnAndUpdatedOnColumn();
