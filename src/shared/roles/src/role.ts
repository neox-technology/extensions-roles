import { ImplicitRoleRegistry } from "./implicit-role-registry.ts"
import { RoleBuilder } from "./role-builder.ts"
import type { IRole, RoleCtor, RoleParent } from "./role-parent.ts"
import { qualify } from "./role-path.ts"

const constructing = new Set<Function>()

export abstract class Role<TParent extends RoleParent> implements IRole {
  readonly dependentRoles: readonly string[]
  readonly name: string
  private readonly parentType: RoleCtor<TParent>

  constructor(
    localName: string,
    parentType: RoleCtor<TParent>,
    compose?: (role: RoleBuilder) => void,
  ) {
    if (localName.trim() === "") {
      throw new Error("name")
    }

    const self = new.target

    if (constructing.has(self)) {
      throw new Error(`Role construction cycle detected at '${self.name}'.`)
    }

    constructing.add(self)
    this.parentType = parentType

    try {
      this.name = qualify(new parentType().getParentRoleName(), localName)

      const builder = new RoleBuilder()

      compose?.(builder)
      this.dependentRoles = withImplicitDependents(
        builder.build(),
        parentType,
        this.name,
        self,
      )
    } finally {
      constructing.delete(self)
    }
  }

  acceptImplicitly(_registry: ImplicitRoleRegistry): void {}

  getParentRoleName(): string {
    return this.name
  }

  parentConstructor(): RoleCtor<TParent> {
    return this.parentType
  }
}

function withImplicitDependents(
  dependents: readonly string[],
  parentType: RoleCtor,
  selfName: string,
  self: Function,
): readonly string[] {
  const result = [...dependents]
  let type: RoleCtor | undefined = parentType

  while (type) {
    const parent: RoleParent = new type()
    const registry = new ImplicitRoleRegistry(self)

    parent.acceptImplicitly(registry)

    for (const roleName of registry.roleNames) {
      if (roleName !== selfName && !result.includes(roleName)) {
        result.push(roleName)
      }
    }

    type = parent.parentConstructor()
  }

  return result
}
