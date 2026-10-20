import type { ImplicitRoleRegistry } from "./implicit-role-registry.ts"
import { combine } from "./role-path.ts"
import type { RoleCtor, RoleParent } from "./role-parent.ts"

export abstract class RoleCollection<TParent extends RoleParent> implements RoleParent {
  readonly name: string
  private readonly parentType: RoleCtor<TParent>

  constructor(name: string, parentType: RoleCtor<TParent>) {
    if (name.trim() === "") {
      throw new Error("name")
    }

    this.name = name
    this.parentType = parentType
  }

  acceptImplicitly(_registry: ImplicitRoleRegistry): void {}

  getParentRoleName(): string {
    return combine(new this.parentType().getParentRoleName(), this.name)
  }

  parentConstructor(): RoleCtor<TParent> {
    return this.parentType
  }
}
