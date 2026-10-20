export class RoleBuilder {
    dependentRoles = [];
    build() {
        return [...new Set(this.dependentRoles)];
    }
    dependsOf(roleType) {
        const role = new roleType();
        this.dependentRoles.push(role.name, ...role.dependentRoles);
        return this;
    }
}
