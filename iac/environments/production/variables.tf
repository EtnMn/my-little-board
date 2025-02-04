variable "environment" {
  description = "Name of the environment"
  type        = string
  default     = "prod"
}

variable "azure-region" {
  description = "Azure location"
  type        = string
  default     = "westeurope"
}

variable "application-name" {
  description = "Application base name"
  type        = string
  default     = "mlb"
}

variable "application-client-id" {
  description = "Application client id"
  type        = string
}

variable "application-client-secret" {
  description = "Application client secret"
  type        = string
  sensitive   = true
}

variable "application-tenant-id" {
  description = "Application tenant id"
  type        = string
}

variable "asp-environment" {
  description = "Name of the asp environment"
  type        = string
  default     = "production"
}

variable "sql-server-administrator-login" {
  description = "The login username of the Azure AD Administrator of this SQL Server."
  type        = string
  sensitive   = true
}

variable "sql-server-administrator-object-id" {
  description = "The object id of the Azure AD Administrator of this SQL Server."
  type        = string
  sensitive   = true
}
