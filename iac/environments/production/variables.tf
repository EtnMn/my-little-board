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
