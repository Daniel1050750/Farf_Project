
-- Drop triggers
DROP TRIGGER IF EXISTS UpdateUpdatedOnColumnOnUpdateRoute          ON "Route";
DROP TRIGGER IF EXISTS SetCreatedOnAndUpdatedOnColumnOnInsertRoute ON "Route";

DROP TRIGGER IF EXISTS UpdateUpdatedOnColumnOnUpdatePoint          ON Point;
DROP TRIGGER IF EXISTS SetCreatedOnAndUpdatedOnColumnOnInsertPoint ON Point;

DROP TRIGGER IF EXISTS UpdateUpdatedOnColumnOnUpdateUser          ON "User";
DROP TRIGGER IF EXISTS SetCreatedOnAndUpdatedOnColumnOnInsertUser ON "User";

-- Drop all tables
DROP TABLE IF EXISTS "Route";
DROP TABLE IF EXISTS RouteState;
DROP TABLE IF EXISTS Point;
DROP TABLE IF EXISTS PointState;
DROP TABLE IF EXISTS "User";
DROP TABLE IF EXISTS UserState;