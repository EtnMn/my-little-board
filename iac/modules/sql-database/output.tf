output "connection-string" {
  value = "Server=tcp:${azurerm_mssql_server.sql-server.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.sql-database.name};User Id=${var.web-app-name};Authentication=Active Directory Managed Identity"
}
