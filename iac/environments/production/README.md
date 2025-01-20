<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.10.4 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | =4.16.0 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 4.16.0 |

## Modules

| Name | Source | Version |
|------|--------|---------|
| <a name="module_application-insights"></a> [application-insights](#module\_application-insights) | ../../modules/application-insights | n/a |
| <a name="module_blazor-app"></a> [blazor-app](#module\_blazor-app) | ../../modules/blazor-app | n/a |

## Resources

| Name | Type |
|------|------|
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/4.16.0/docs/data-sources/resource_group) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_application-name"></a> [application-name](#input\_application-name) | Application base name | `string` | `"mlb"` | no |
| <a name="input_asp-environment"></a> [asp-environment](#input\_asp-environment) | Name of the asp environment | `string` | `"production"` | no |
| <a name="input_azure-region"></a> [azure-region](#input\_azure-region) | Azure location | `string` | `"westeurope"` | no |
| <a name="input_environment"></a> [environment](#input\_environment) | Name of the environment | `string` | `"prod"` | no |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_resource-group-name"></a> [resource-group-name](#output\_resource-group-name) | Application resource group |
| <a name="output_web-app-name"></a> [web-app-name](#output\_web-app-name) | Web application name |
<!-- END_TF_DOCS -->