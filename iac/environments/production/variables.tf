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

variable "asp-environment" {
  description = "Name of the asp environment"
  type        = string
  default     = "production"
}

variable "sql-server-administrator-login" {
  description = "The login username of the Azure AD Administrator of this SQL Server."
  type        = string
}

variable "sql-server-administrator-object-id" {
  description = "The object id of the Azure AD Administrator of this SQL Server."
  type        = string
}
