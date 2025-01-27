data "azurerm_resource_group" "resource-group" {
  name = var.resource-group-name
}

# Service plan.
resource "azurerm_service_plan" "blazor-app-service-plan" {
  name                = "sp-${var.application-name}-${var.environment}"
  location            = data.azurerm_resource_group.resource-group.location
  resource_group_name = data.azurerm_resource_group.resource-group.name
  os_type             = var.app-plan-os-type
  sku_name            = var.app-plan-sku-name
  tags = {
    "environment" = var.environment,
    "application" = var.application-name
  }
}

# Web app.
resource "azurerm_windows_web_app" "blazor-app" {
  name                = "web-${var.application-name}-${var.environment}"
  location            = data.azurerm_resource_group.resource-group.location
  resource_group_name = data.azurerm_resource_group.resource-group.name
  service_plan_id     = azurerm_service_plan.blazor-app-service-plan.id
  https_only          = true

  site_config {
    application_stack {
      current_stack = "dotnet"
      #checkov:skip=CKV_AZURE_80:Ignore Net Framework' version check.
      dotnet_version = var.dotnet-version
    }
    http2_enabled = true
    always_on     = var.app-plan-sku-name != "F1" && var.app-plan-sku-name != "D1"
    ftps_state    = "FtpsOnly"
    virtual_application {
      preload       = false
      virtual_path  = "/"
      physical_path = "site\\wwwroot"
    }
  }

  app_settings = {
    APPLICATIONINSIGHTS_CONNECTION_STRING      = var.application-insights-connection-string
    ApplicationInsightsAgent_EXTENSION_VERSION = "~2"
    ASPNETCORE_ENVIRONMENT                     = var.asp-environment
    XDT_MicrosoftApplicationInsights_Mode      = "default"
  }

  identity {
    type = "SystemAssigned"
  }
}

