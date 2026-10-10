# Neox.Extensions.Roles

Bibliothèque pour définir des rôles, les contrôler dans ASP.NET Core et les héberger avec Aspire, avec un package web.

## Installation

Prérequis : [.NET 10 SDK](https://dotnet.microsoft.com/download), Node.js.

```bash
dotnet restore Neox.Extensions.Roles.slnx
dotnet build Neox.Extensions.Roles.slnx
```

## Utilisation

Référencer `Neox.Extensions.Roles.Shared` pour le modèle, `Neox.Extensions.Roles.AspNetCore` pour l'autorisation, `Neox.Extensions.Roles.Aspire.Hosting.Azure.EntraId` pour déclarer les rôles d'une collection sur une inscription Entra, et `Neox.Extensions.Roles.Aspire.Hosting.Keycloak` pour les déclarer sur un client Keycloak. Le package web est `@neox/extensions-roles`.

```csharp
registration.WithRoles<Billing>();
apiClient.WithRoles<Billing>();
```

Une racine `RoleRoot` qualifie les noms (`Billing.Reader`) et peut faire accepter un rôle à tous ses descendants sans `DependsOf` :

```csharp
public sealed class AppRoles : RoleRoot {

    public override void AcceptImplicitly(IImplicitRoleRegistry registry) =>
        registry.Accept<Administrator>();

    public sealed class Administrator : Role<AppRoles> {

        public Administrator() : base("Administrator") {
        }
    }
}
```

## Contribuer

Les questions et les changements passent par les issues et les pull requests de [extensions-roles](https://github.com/neox-technology/extensions-roles). Décrire le changement dans la pull request.

## Liens

- [Spécifications](specs/README.md)
- [Sources](src/)
- [Workflows](.github/workflows/)
- [Solution](Neox.Extensions.Roles.slnx)
