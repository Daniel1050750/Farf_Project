
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
