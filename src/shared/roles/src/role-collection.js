import { combine } from "./role-path.ts";
export class RoleCollection {
    name;
    parentType;
    constructor(name, parentType) {
        if (name.trim() === "") {
            throw new Error("name");
        }
        this.name = name;
        this.parentType = parentType;
    }
    acceptImplicitly(_registry) { }
    getParentRoleName() {
        return combine(new this.parentType().getParentRoleName(), this.name);
    }
    parentConstructor() {
        return this.parentType;
    }
}
