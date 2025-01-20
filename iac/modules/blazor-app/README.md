<!-- BEGIN_TF_DOCS -->
## Requirements

No requirements.

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | n/a |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_service_plan.blazor-app-service-plan](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/service_plan) | resource |
| [azurerm_windows_web_app.blazor-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/windows_web_app) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/resource_group) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_app-plan-os-type"></a> [app-plan-os-type](#input\_app-plan-os-type) | The O/S type for the App Services. | `string` | `"Windows"` | no |
| <a name="input_app-plan-sku-name"></a> [app-plan-sku-name](#input\_app-plan-sku-name) | The SKU for the plan. | `string` | `"F1"` | no |
| <a name="input_application-insights-connection-string"></a> [application-insights-connection-string](#input\_application-insights-connection-string) | The connection string for the Application Insights. | `string` | n/a | yes |
| <a name="input_application-name"></a> [application-name](#input\_application-name) | The name of the application. | `string` | n/a | yes |
| <a name="input_asp-environment"></a> [asp-environment](#input\_asp-environment) | Name of the asp environment | `string` | n/a | yes |
| <a name="input_dotnet-version"></a> [dotnet-version](#input\_dotnet-version) | The version of .NET to use. | `string` | `"v9.0"` | no |
| <a name="input_environment"></a> [environment](#input\_environment) | The environment to deploy the resources. | `string` | n/a | yes |
| <a name="input_resource-group-name"></a> [resource-group-name](#input\_resource-group-name) | The name of the Resource Group. | `string` | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_name"></a> [name](#output\_name) | n/a |
<!-- END_TF_DOCS -->