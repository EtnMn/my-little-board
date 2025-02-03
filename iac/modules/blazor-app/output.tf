output "name" {
  value = azurerm_windows_web_app.blazor-app.name
}

output "principal-id" {
  value = azurerm_windows_web_app.blazor-app.identity.0.principal_id
}
