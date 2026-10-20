import type { ImplicitRoleRegistry } from "./implicit-role-registry.ts"
import type { RoleCtor, RoleParent } from "./role-parent.ts"

export abstract class RoleRoot implements RoleParent {
  acceptImplicitly(_registry: ImplicitRoleRegistry): void {}

  getParentRoleName(): string {
    return ""
  }

  parentConstructor(): RoleCtor | undefined {
    return undefined
  }
}
