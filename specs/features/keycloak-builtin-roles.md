# Built-in Keycloak roles

| Field | Value |
|-------|-------|
| Slug | `keycloak-builtin-roles` |
| Status | implemented |
| Last code review | 2026-10-15 |

## Summary

Shipping package **`Neox.Extensions.Roles.Aspire.Provisioning.Keycloak`** declares the built-in Keycloak 26.7 client roles of `realm-management` as a `RoleRoot` catalog. Published names stay the Keycloak names (`manage-organizations`) because the parent path of a `RoleRoot` is empty. Consumers compose them with `DependsOf<T>()`.

## User scenarios

1. **Catalog author depends on organization management** — `DependsOf<ManageOrganizations>()` records the name `manage-organizations` and does not create a realm role of that name.
2. **Hosting resolves a built-in name** — `KeycloakBuiltInRoles.TryGetClientId` returns client id `realm-management`.

## Business rules

1. **One role type per built-in client role** — `AdminRoles.ALL_REALM_ROLES` plus `impersonation` from Keycloak 26.7. Master-only roles (`admin`, `create-realm`, `realm-admin`) are out of scope.
2. **No extra composites** — the leaves do not `DependsOf` each other. Keycloak already composites `view-*` into `query-*`.
3. **Lookup by reflection** — `TryGetClientId` scans public `IRole` types in this assembly. It does not keep a second handwritten name list.

## Dependencies

- `Neox.Extensions.Roles.Shared` — `RoleRoot`, `Role<T>`, `DependsOf<T>()`
- Keycloak 26.7 `realm-management` client roles

## Out of scope

- Writing realm JSON (`Neox.Extensions.Roles.Aspire.Hosting.Keycloak`)
- Enabling organizations on a realm (`WithOrganizations`)
- Master-realm roles `admin`, `create-realm`, and `realm-admin`

## Acceptance criteria

- [x] Package `Neox.Extensions.Roles.Aspire.Provisioning.Keycloak` under `src/aspire/provisioning/` (`IsPackable=true`, `net10.0`).
- [x] `RealmManagementRoles` is a `RoleRoot`. Each leaf name equals the Keycloak client role name.
- [x] `KeycloakBuiltInRoles.TryGetClientId("manage-organizations", ...)` returns `realm-management`.
- [x] Project listed in `Neox.Extensions.Roles.slnx`.

## Implementation notes

| Item | Path / note |
|------|-------------|
| Shipping package | `src/aspire/provisioning/Neox.Extensions.Roles.Aspire.Provisioning.Keycloak/` |
| Namespace | `Neox.Extensions.Roles.Provisioning.Keycloak` |
| Client id | `realm-management` |
