import type { IRole, RoleCtor } from "./role-parent.ts"

export class RoleBuilder {
  private readonly dependentRoles: string[] = []

  build(): readonly string[] {
    return [...new Set(this.dependentRoles)]
  }

  dependsOf(roleType: RoleCtor<IRole>): this {
    const role = new roleType()

    this.dependentRoles.push(role.name, ...role.dependentRoles)
    return this
  }
}
