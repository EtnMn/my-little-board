terraform {
  required_providers {
    mssql = {
      source  = "betr-io/mssql"
      version = "=0.3.1"
    }
  }
}

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
  name                        = "sqldb-${var.application-name}-${var.environment}"
  server_id                   = azurerm_mssql_server.sql-server.id
  sku_name                    = "GP_S_Gen5_2"
  collation                   = "SQL_Latin1_General_CP1_CI_AS"
  min_capacity                = 0.5
  max_size_gb                 = 5
  zone_redundant              = false
  auto_pause_delay_in_minutes = 60
  storage_account_type        = "Local"

  # Prevent the possibility of accidental data loss.
  lifecycle {
    prevent_destroy = true
  }
}

resource "azurerm_mssql_firewall_rule" "allow-azure-accessazure" {
  name             = "AllowAzureAccess"
  server_id        = azurerm_mssql_server.sql-server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
  depends_on = [
    azurerm_mssql_server.sql-server
  ]
}

resource "mssql_user" "sql-web-app-user" {
  server {
    host = azurerm_mssql_server.sql-server.fully_qualified_domain_name
    azuread_default_chain_auth {}
  }
  object_id = var.web-app-principal-id
  database  = azurerm_mssql_database.sql-database.name
  username  = var.web-app-name
  roles     = ["db_datareader", "db_datawriter"]
  depends_on = [
    azurerm_mssql_database.sql-database
  ]
}

