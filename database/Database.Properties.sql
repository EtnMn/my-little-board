--- Execute on database (use federated identity name)
CREATE USER [managed-identity] FROM EXTERNAL PROVIDER

-- Classic application account permissions
ALTER ROLE db_datareader ADD MEMBER [managed-identity]
ALTER ROLE db_datawriter ADD MEMBER [managed-identity]

-- and execute permission for stored proc
GRANT EXECUTE TO [managed-identity]

-- Only for deployment account
ALTER ROLE db_ddladmin ADD MEMBER [managed-identity]
