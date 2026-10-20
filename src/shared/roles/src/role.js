import { ImplicitRoleRegistry } from "./implicit-role-registry.ts";
import { RoleBuilder } from "./role-builder.ts";
import { qualify } from "./role-path.ts";
const constructing = new Set();
export class Role {
    dependentRoles;
    name;
    parentType;
    constructor(localName, parentType, compose) {
        if (localName.trim() === "") {
            throw new Error("name");
        }
        const self = new.target;
        if (constructing.has(self)) {
            throw new Error(`Role construction cycle detected at '${self.name}'.`);
        }
        constructing.add(self);
        this.parentType = parentType;
        try {
            this.name = qualify(new parentType().getParentRoleName(), localName);
            const builder = new RoleBuilder();
            compose?.(builder);
            this.dependentRoles = withImplicitDependents(builder.build(), parentType, this.name, self);
        }
        finally {
            constructing.delete(self);
        }
    }
    acceptImplicitly(_registry) { }
    getParentRoleName() {
        return this.name;
    }
    parentConstructor() {
        return this.parentType;
    }
}
function withImplicitDependents(dependents, parentType, selfName, self) {
    const result = [...dependents];
    let type = parentType;
    while (type) {
        const parent = new type();
        const registry = new ImplicitRoleRegistry(self);
        parent.acceptImplicitly(registry);
        for (const roleName of registry.roleNames) {
            if (roleName !== selfName && !result.includes(roleName)) {
                result.push(roleName);
            }
        }
        type = parent.parentConstructor();
    }
    return result;
}
