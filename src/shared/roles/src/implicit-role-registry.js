export class ImplicitRoleRegistry {
    names = [];
    self;
    constructor(self) {
        this.self = self;
    }
    get roleNames() {
        return this.names;
    }
    accept(roleType) {
        if (roleType === this.self) {
            return;
        }
        const role = new roleType();
        this.add(role.name);
        for (const dependent of role.dependentRoles) {
            this.add(dependent);
        }
    }
    add(name) {
        if (!this.names.includes(name)) {
            this.names.push(name);
        }
    }
}
