-- NOTE: Permission must be granted to the federated managed identity (use federated identity name) to allow to deploy the database.

-- Execute on database
CREATE USER [managed-identity] FROM EXTERNAL PROVIDER

-- Classic application account permissions
ALTER ROLE db_datareader ADD MEMBER [managed-identity]
ALTER ROLE db_datawriter ADD MEMBER [managed-identity]

-- and execute permission for stored proc
GRANT EXECUTE TO [managed-identity]

-- Only for deployment account
ALTER ROLE db_ddladmin ADD MEMBER [managed-identity]
ALTER ROLE db_accessadmin ADD MEMBER [managed-identity] -- Allow managed identity to add access
