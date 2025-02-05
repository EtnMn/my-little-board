<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_mssql"></a> [mssql](#requirement\_mssql) | =0.3.1 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | n/a |
| <a name="provider_mssql"></a> [mssql](#provider\_mssql) | =0.3.1 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_mssql_database.sql-database](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_database) | resource |
| [azurerm_mssql_firewall_rule.allow-azure-accessazure](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_firewall_rule) | resource |
| [azurerm_mssql_server.sql-server](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_server) | resource |
| [mssql_user.sql-web-app-user](https://registry.terraform.io/providers/betr-io/mssql/0.3.1/docs/resources/user) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/resource_group) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_application-name"></a> [application-name](#input\_application-name) | The name of the application. | `string` | n/a | yes |
| <a name="input_environment"></a> [environment](#input\_environment) | The environment to deploy the resources. | `string` | n/a | yes |
| <a name="input_resource-group-name"></a> [resource-group-name](#input\_resource-group-name) | The name of the Resource Group. | `string` | n/a | yes |
| <a name="input_sql-server-administrator-login"></a> [sql-server-administrator-login](#input\_sql-server-administrator-login) | The login username of the Azure AD Administrator of this SQL Server. | `string` | n/a | yes |
| <a name="input_sql-server-administrator-object-id"></a> [sql-server-administrator-object-id](#input\_sql-server-administrator-object-id) | The object id of the Azure AD Administrator of this SQL Server. | `string` | n/a | yes |
| <a name="input_web-app-name"></a> [web-app-name](#input\_web-app-name) | The name of the web app. | `string` | n/a | yes |
| <a name="input_web-app-principal-id"></a> [web-app-principal-id](#input\_web-app-principal-id) | The principal id of the web app. | `string` | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_connection-string"></a> [connection-string](#output\_connection-string) | n/a |
<!-- END_TF_DOCS -->