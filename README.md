# dotnet-client-template

Configuring database for development:

```
dotnet user-secrets set "ConnectionStrings:Database" "Server=localhost;Database=dotnet_client_template;User ID=sa;Password=yourStrong(!)Password;MultipleActiveResultSets=true"
```

Configuring OIDC for development:

```
dotnet user-secrets set "oidc:clientId" "<clientId>"
dotnet user-secrets set "oidc:clientSecret" "<clientSecret>"
dotnet user-secrets set "oidc:authorityUri" "<authorityUri>"
```
