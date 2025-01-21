# More details on: https://yourazurecoach.com/2022/12/29/use-github-actions-with-user-assigned-managed-identity/
ENV=Prod
LOCATION=westeurope
RESOURCE_GROUP_NAME=rg-mlb-${ENV,,}
IDENTITY_NAME=id-mlb-federated-${ENV,,}
FEDERATED_IDENTITY_NAME=fid-mlb-github-${ENV,,}
GITHUB_ISSUER=https://token.actions.githubusercontent.com
GITHUB_ORGANIZATION=etnmn
GITHUB_REPOSITORY=my-little-board
GITHUB_ENVIRONMENT=Production
SUBJECT=repo:${GITHUB_ORGANIZATION}/${GITHUB_REPOSITORY}:environment:${GITHUB_ENVIRONMENT}
AUDIENCE=api://AzureADTokenExchange # This value is used to establish a connection between external workload identities (e.g. GitHub) and Microsoft Entra ID.

# Create User Assigned Managed Identity.
IDENTITY_OUTPUT=$(az identity create \
    --name $IDENTITY_NAME\
    --resource-group $RESOURCE_GROUP_NAME \
    --location $LOCATION \
    --output json)

SUBSCRIPTION_ID=$(az account show --query id -o tsv)
IDENTITY_NAME=$(echo $IDENTITY_OUTPUT | jq -r '.name')
IDENTITY_PRINCIPAL_ID=$(echo $IDENTITY_OUTPUT | jq -r '.principalId')
IDENTITY_CLIENT_ID=$(echo $IDENTITY_OUTPUT | jq -r '.clientId')
IDENTITY_TENANT_ID=$(echo $IDENTITY_OUTPUT | jq -r '.tenantId')

# Add environment federated credentials.
az identity federated-credential create \
    --name $FEDERATED_IDENTITY_NAME \
    --identity-name $IDENTITY_NAME \
    --resource-group $RESOURCE_GROUP_NAME \
    --issuer $GITHUB_ISSUER \
    --subject $SUBJECT \
    --audiences $AUDIENCE \
    --output none


# Assign Contributor permissions to create Azure resources.
az role assignment create \
    --role "Contributor" \
    --assignee-object-id $IDENTITY_PRINCIPAL_ID \
    --assignee-principal-type ServicePrincipal \
    --scope /subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP_NAME \
    --output none

# Assign User Access Administrator permissions to perform role assignments on managed identities.
az role assignment create \
    --role "User Access Administrator" \
    --assignee-object-id $IDENTITY_PRINCIPAL_ID \
    --assignee-principal-type ServicePrincipal \
    --scope /subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP_NAME \
    --output none

# Add the following secrets in GitHub environment credentials.
echo "AZURE_CLIENT_ID: ${IDENTITY_CLIENT_ID}"
echo "AZURE_TENANT_ID: ${IDENTITY_TENANT_ID}"
echo "AZURE_SUBSCRIPTION_ID: ${SUBSCRIPTION_ID}"
