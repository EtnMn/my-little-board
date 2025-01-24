data "azurerm_resource_group" "resource-group" {
  name = var.resource-group-name
}

resource "azurerm_application_insights" "application-insights" {
  name                = "ai-${var.application-name}-${var.environment}"
  location            = data.azurerm_resource_group.resource-group.location
  resource_group_name = data.azurerm_resource_group.resource-group.name
  application_type    = "web"
}
