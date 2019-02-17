
-- Route State
INSERT INTO RouteState(Id, Name) VALUES (1, 'Active');
INSERT INTO RouteState(Id, Name) VALUES (2, 'Inactive');

-- Creates Routes
INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c001', 'A', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a128', 156, 985, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c002', 'B', '755ea1f8-b453-4425-ae93-135a0f27a127', '755ea1f8-b453-4425-ae93-135a0f27a124', 2, 32, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c003', 'C', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a127', 32, 65, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c004', 'D', '755ea1f8-b453-4425-ae93-135a0f27a125', '755ea1f8-b453-4425-ae93-135a0f27a126', 45, 71, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c005', 'E', '755ea1f8-b453-4425-ae93-135a0f27a124', '755ea1f8-b453-4425-ae93-135a0f27a125', 26, 98, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c006', 'F', '755ea1f8-b453-4425-ae93-135a0f27a124', '755ea1f8-b453-4425-ae93-135a0f27a128', 14, 54, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c007', 'G', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a124', 16, 44, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c008', 'H', '755ea1f8-b453-4425-ae93-135a0f27a123', '755ea1f8-b453-4425-ae93-135a0f27a127', 87, 11, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c009', 'I', '755ea1f8-b453-4425-ae93-135a0f27a125', '755ea1f8-b453-4425-ae93-135a0f27a129', 33, 26, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);

INSERT INTO "Route" (id, name, pointstart, pointend, routeprice, routetime, state, createdon, updatedon, isdeleted) 
VALUES ('755ea1f8-b453-4425-ae93-135a0f27c010', 'J', '755ea1f8-b453-4425-ae93-135a0f27a126', '755ea1f8-b453-4425-ae93-135a0f27a127', 34, 25, 1, (NOW() AT TIME ZONE 'utc'), (NOW() AT TIME ZONE 'utc'), false);