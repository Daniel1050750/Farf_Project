

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