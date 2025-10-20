// Infraestructura como Código para Aplicación de Contabilidad Mundial
// Este archivo Bicep crea todos los recursos Azure necesarios para producción

@description('Nombre base para todos los recursos')
param baseName string = 'contabilidad-mundial'

@description('Ubicación para los recursos Azure')
param location string = resourceGroup().location

@description('Environment tag')
param environment string = 'production'

@description('SKU para App Service Plan')
@allowed(['B1', 'B2', 'S1', 'S2', 'P1V2', 'P2V2'])
param appServiceSku string = 'B1'

@description('SKU para Azure SQL Database')
@allowed(['Basic', 'S0', 'S1', 'S2', 'P1', 'P2'])
param sqlDatabaseSku string = 'S0'

// Variables
var appServicePlanName = '${baseName}-plan'
var webAppName = '${baseName}-app'
var sqlServerName = 'svydconta-sqlserver' // Usar el servidor existente
var databaseName = 'SvydConta' // Usar la base de datos existente
var applicationInsightsName = '${baseName}-insights'
var logAnalyticsName = '${baseName}-logs'

// Recurso existente: SQL Server (referencia)
resource sqlServer 'Microsoft.Sql/servers@2023-05-01-preview' existing = {
  name: sqlServerName
}

// Recurso existente: SQL Database (referencia)
resource database 'Microsoft.Sql/servers/databases@2023-05-01-preview' existing = {
  parent: sqlServer
  name: databaseName
}

// Log Analytics Workspace para Application Insights
resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
    workspaceCapping: {
      dailyQuotaGb: 1
    }
  }
  tags: {
    Environment: environment
    Project: 'ContabilidadMundial'
  }
}

// Application Insights para monitoreo
resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: applicationInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
    IngestionMode: 'LogAnalytics'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
  tags: {
    Environment: environment
    Project: 'ContabilidadMundial'
  }
}

// App Service Plan
resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: appServiceSku
    tier: appServiceSku == 'B1' ? 'Basic' : (appServiceSku == 'S1' ? 'Standard' : 'PremiumV2')
    capacity: 1
  }
  properties: {
    reserved: false // Windows hosting
  }
  tags: {
    Environment: environment
    Project: 'ContabilidadMundial'
  }
}

// Web App (Azure App Service)
resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: webAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    clientAffinityEnabled: false
    siteConfig: {
      netFrameworkVersion: 'v8.0'
      metadata: [
        {
          name: 'CURRENT_STACK'
          value: 'dotnet'
        }
      ]
      appSettings: [
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: applicationInsights.properties.ConnectionString
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Production'
        }
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          connectionString: 'Server=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${databaseName};Authentication=Active Directory Default;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
          type: 'SQLAzure'
        }
      ]
      minTlsVersion: '1.2'
      scmMinTlsVersion: '1.2'
      ftpsState: 'FtpsOnly'
      alwaysOn: true
      webSocketsEnabled: true // Necesario para Blazor Server
      requestTracingEnabled: true
      httpLoggingEnabled: true
      logsDirectorySizeLimit: 40
      detailedErrorLoggingEnabled: true
    }
  }
  tags: {
    Environment: environment
    Project: 'ContabilidadMundial'
  }
}

// Asignar permisos de SQL Database Contributor al Managed Identity de la Web App
resource sqlRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(webApp.id, database.id, 'SQL DB Contributor')
  scope: database
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '9b7fa17d-e63e-47b0-bb0a-15c516ac86ec') // SQL DB Contributor
    principalId: webApp.identity.principalId
    principalType: 'ServicePrincipal'
  }
}

// Outputs importantes
output webAppUrl string = 'https://${webApp.properties.defaultHostName}'
output webAppName string = webApp.name
output applicationInsightsName string = applicationInsights.name
output applicationInsightsConnectionString string = applicationInsights.properties.ConnectionString
output resourceGroupName string = resourceGroup().name
output managedIdentityPrincipalId string = webApp.identity.principalId