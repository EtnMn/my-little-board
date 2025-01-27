# Specify the version of the AzureRM Provider to use.
terraform {
  required_version = ">= 1.10.4"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.16.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "rg-mlb-prod"
    storage_account_name = "stmlbstate22472"
    container_name       = "tfstate"
    key                  = "mlb.tfstate"
  }
}

# Configure the AzureRM Provider.
provider "azurerm" {
  features {}
  # In version 4.0 of the Azure Provider, the Azure Subscription ID is required.
  subscription_id = "8595f4ea-053f-4d5c-854b-6db01c741588"
}

locals {
  environment = terraform.workspace == "default" ? var.environment : terraform.workspace
}

# Get existing resource group.
data "azurerm_resource_group" "resource-group" {
  name = "rg-${var.application-name}-${local.environment}"
}

# Set application insights.
module "application-insights" {
  source              = "../../modules/application-insights"
  environment         = local.environment
  application-name    = var.application-name
  resource-group-name = data.azurerm_resource_group.resource-group.name
}

# Set application.
module "blazor-app" {
  source                                 = "../../modules/blazor-app"
  environment                            = local.environment
  application-name                       = var.application-name
  resource-group-name                    = data.azurerm_resource_group.resource-group.name
  asp-environment                        = var.asp-environment
  application-insights-connection-string = module.application-insights.connection-string
}

# Set SQL server database.
module "sql-server" {
  source                             = "../../modules/sql-database"
  environment                        = local.environment
  resource-group-name                = data.azurerm_resource_group.resource-group.name
  application-name                   = var.application-name
  sql-server-administrator-login     = var.sql-server-administrator-login
  sql-server-administrator-object-id = var.sql-server-administrator-object-id
  web-app-name                       = module.blazor-app.name
  web-app-principal-id               = module.blazor-app.principal-id
}
