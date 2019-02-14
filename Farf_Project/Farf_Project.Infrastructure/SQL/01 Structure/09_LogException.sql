-- Drop all tables
DROP TABLE IF EXISTS LogException;

--Create LogException table
CREATE TABLE LogException (
	Id 			UUID  NOT NULL,
	Exception	TEXT,
	CreatedOn   TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'), -- UTC FORMAT
	PRIMARY KEY (Id)
);
