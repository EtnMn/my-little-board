data "azurerm_resource_group" "resource-group" {
  name = var.resource-group-name
}

# Application insights.
resource "azurerm_log_analytics_workspace" "log-analytics" {
  name                = "log-${var.application-name}-${var.environment}"
  location            = data.azurerm_resource_group.resource-group.location
  resource_group_name = data.azurerm_resource_group.resource-group.name
  sku                 = "PerGB2018"
}

resource "azurerm_application_insights" "application-insights" {
  name                = "ai-${var.application-name}-${var.environment}"
  location            = data.azurerm_resource_group.resource-group.location
  resource_group_name = data.azurerm_resource_group.resource-group.name
  workspace_id        = azurerm_log_analytics_workspace.log-analytics.id
  application_type    = "web"
}
