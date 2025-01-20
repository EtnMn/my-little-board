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

variable "app-plan-sku-name" {
  description = "The SKU for the plan."
  type        = string
  default     = "F1"
}

variable "app-plan-os-type" {
  description = "The O/S type for the App Services."
  type        = string
  default     = "Windows"
}

variable "asp-environment" {
  description = "Name of the asp environment"
  type        = string
}

variable "dotnet-version" {
  description = "The version of .NET to use."
  type        = string
  default     = "v9.0"
}

variable "application-insights-connection-string" {
  description = "The connection string for the Application Insights."
  type        = string
}
