import type { ImplicitRoleRegistry } from "./implicit-role-registry.ts"

export interface RoleParent {
  acceptImplicitly(registry: ImplicitRoleRegistry): void
  getParentRoleName(): string
  parentConstructor(): RoleCtor | undefined
}

export type RoleCtor<T extends RoleParent = RoleParent> = new () => T

export interface IRole extends RoleParent {
  readonly dependentRoles: readonly string[]
  readonly name: string
}
