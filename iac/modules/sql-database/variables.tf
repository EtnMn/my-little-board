variable "environment" {
  description = "The environment to deploy the resources."
  type        = string
}

variable "resource-group-name" {
  description = "The name of the Resource Group."
  type        = string
}

variable "application-name" {
  description = "The name of the application."
  type        = string
}

variable "sql-server-administrator-login" {
  description = "The login username of the Azure AD Administrator of this SQL Server."
  type        = string
}

variable "sql-server-administrator-object-id" {
  description = "The object id of the Azure AD Administrator of this SQL Server."
  type        = string
}

variable "web-app-name" {
  description = "The name of the web app."
  type        = string
}

variable "web-app-principal-id" {
  description = "The principal id of the web app."
  type        = string
}
