data "azurerm_resource_group" "resource-group" {
  name = var.resource-group-name
}

# SQL server.
resource "azurerm_mssql_server" "sql-server" {
  name                = "sql-${var.application-name}-${var.environment}"
  resource_group_name = data.azurerm_resource_group.resource-group.name
  location            = data.azurerm_resource_group.resource-group.location
  version             = "12.0"
  minimum_tls_version = "1.2"

  azuread_administrator {
    login_username              = var.sql-server-administrator-login
    object_id                   = var.sql-server-administrator-object-id
    azuread_authentication_only = true
  }
}

# SQL database.
resource "azurerm_mssql_database" "sql-database" {
  name        = "sqldb-${var.application-name}-${var.environment}"
  server_id   = azurerm_mssql_server.sql-server.id
  sku_name    = "GP_S_Gen5_2"
  max_size_gb = 4

  # Prevent the possibility of accidental data loss.
  lifecycle {
    prevent_destroy = true
  }

  provisioner "local-exec" {
    command = <<EOT
      az sql db execute --name ${self.name} --server ${azurerm_mssql_server.sql-server.name} --resource-group ${data.azurerm_resource_group.resource-group.name} --query "
      IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = '${var.web-app-name}')
      BEGIN
        CREATE USER [${var.web-app-name}] FOR LOGIN [${var.web-app-name}]
        ALTER ROLE db_datareader ADD MEMBER [${var.web-app-name}]
        ALTER ROLE db_datawriter ADD MEMBER [${var.web-app-name}]
        GRANT EXECUTE TO [${var.web-app-name}]
      END"
    EOT
  }
}
