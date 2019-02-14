
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