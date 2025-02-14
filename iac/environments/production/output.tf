output "resource-group-name" {
  value       = data.azurerm_resource_group.resource-group.name
  description = "Application resource group"
}

output "web-app-name" {
  value       = module.blazor-app.name
  description = "Web application name"
}

output "sql-db-connection-string" {
  value       = module.sql-database.connection-string
  description = "Sql database connection string"
}
