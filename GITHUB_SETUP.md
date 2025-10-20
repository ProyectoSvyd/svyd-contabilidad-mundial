# Configuración para GitHub Repository

## 🚀 Setup de CI/CD

### 1. Crear repositorio en GitHub
- Ve a https://github.com/new
- Nombre: `svyd-contabilidad-mundial`
- Descripción: `Sistema de Contabilidad Mundial - Blazor + Azure SQL`
- Público o Privado (tu elección)

### 2. Configurar Secrets en GitHub
Ve a tu repositorio → Settings → Secrets and variables → Actions

**Agregar estos secrets:**

**AZURE_WEBAPP_PUBLISH_PROFILE:**
```xml
<publishData><publishProfile profileName="svyd-contabilidad-app - Web Deploy" publishMethod="MSDeploy" publishUrl="svyd-contabilidad-app.scm.azurewebsites.net:443" msdeploySite="svyd-contabilidad-app" userName="$svyd-contabilidad-app" userPWD="7ivoSuwHw7Z2dZyMuwH6PmW2c4zjKcp7wTHLn8ZrQD7aFbqrw7EjrnAuaoe0" destinationAppUrl="https://svyd-contabilidad-app.azurewebsites.net" SQLServerDBConnectionString="Server=tcp:svydconta-sqlserver.database.windows.net,1433;Initial Catalog=SvydConta;Authentication=Active Directory Default;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" mySQLDBConnectionString="" hostingProviderForumLink="" controlPanelLink="https://portal.azure.com" webSystem="WebSites" targetDatabaseEngineType="sqlazuredatabase" targetServerVersion="Version100"><databases><add name="DefaultConnection" connectionString="Server=tcp:svydconta-sqlserver.database.windows.net,1433;Initial Catalog=SvydConta;Authentication=Active Directory Default;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" providerName="System.Data.SqlClient" type="Sql" targetDatabaseEngineType="sqlazuredatabase" targetServerVersion="Version100" /></databases></publishProfile></publishData>
```

**APPINSIGHTS_INSTRUMENTATION_KEY:**
```
ef920821-e8ed-4b07-ba53-c562dee9ff9f
```

### 3. Comandos para subir código a GitHub
```bash
# Desde la carpeta del proyecto
git init
git add .
git commit -m "🚀 Initial deployment - Aplicación de Contabilidad Mundial"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/svyd-contabilidad-mundial.git
git push -u origin main
```

### 4. Verificar deployment automático
- Cada push a `main` activará el deployment automático
- Ve a Actions tab en GitHub para monitorear