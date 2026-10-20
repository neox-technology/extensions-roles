import type { IRole, RoleCtor } from "./role-parent.ts"

export class ImplicitRoleRegistry {
  private readonly names: string[] = []
  private readonly self: Function

  constructor(self: Function) {
    this.self = self
  }

  get roleNames(): readonly string[] {
    return this.names
  }

  accept(roleType: RoleCtor<IRole>): void {
    if (roleType === this.self) {
      return
    }

    const role = new roleType()

    this.add(role.name)

    for (const dependent of role.dependentRoles) {
      this.add(dependent)
    }
  }

  private add(name: string): void {
    if (!this.names.includes(name)) {
      this.names.push(name)
    }
  }
}
