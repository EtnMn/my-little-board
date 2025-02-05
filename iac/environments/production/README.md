<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.10.4 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | =4.16.0 |
| <a name="requirement_mssql"></a> [mssql](#requirement\_mssql) | =0.3.1 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 4.16.0 |
| <a name="provider_null"></a> [null](#provider\_null) | 3.2.3 |

## Modules

| Name | Source | Version |
|------|--------|---------|
| <a name="module_application-insights"></a> [application-insights](#module\_application-insights) | ../../modules/application-insights | n/a |
| <a name="module_blazor-app"></a> [blazor-app](#module\_blazor-app) | ../../modules/blazor-app | n/a |
| <a name="module_sql-database"></a> [sql-database](#module\_sql-database) | ../../modules/sql-database | n/a |

## Resources

| Name | Type |
|------|------|
| [null_resource.update_blazor-app-connection-string](https://registry.terraform.io/providers/hashicorp/null/latest/docs/resources/resource) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/4.16.0/docs/data-sources/resource_group) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_application-client-id"></a> [application-client-id](#input\_application-client-id) | Application client id | `string` | n/a | yes |
| <a name="input_application-client-secret"></a> [application-client-secret](#input\_application-client-secret) | Application client secret | `string` | n/a | yes |
| <a name="input_application-name"></a> [application-name](#input\_application-name) | Application base name | `string` | `"mlb"` | no |
| <a name="input_application-tenant-id"></a> [application-tenant-id](#input\_application-tenant-id) | Application tenant id | `string` | n/a | yes |
| <a name="input_asp-environment"></a> [asp-environment](#input\_asp-environment) | Name of the asp environment | `string` | `"production"` | no |
| <a name="input_azure-region"></a> [azure-region](#input\_azure-region) | Azure location | `string` | `"westeurope"` | no |
| <a name="input_environment"></a> [environment](#input\_environment) | Name of the environment | `string` | `"prod"` | no |
| <a name="input_sql-server-administrator-login"></a> [sql-server-administrator-login](#input\_sql-server-administrator-login) | The login username of the Azure AD Administrator of this SQL Server. | `string` | n/a | yes |
| <a name="input_sql-server-administrator-object-id"></a> [sql-server-administrator-object-id](#input\_sql-server-administrator-object-id) | The object id of the Azure AD Administrator of this SQL Server. | `string` | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_resource-group-name"></a> [resource-group-name](#output\_resource-group-name) | Application resource group |
| <a name="output_sql-db-connection-string"></a> [sql-db-connection-string](#output\_sql-db-connection-string) | Sql database connection string |
| <a name="output_web-app-name"></a> [web-app-name](#output\_web-app-name) | Web application name |
<!-- END_TF_DOCS -->