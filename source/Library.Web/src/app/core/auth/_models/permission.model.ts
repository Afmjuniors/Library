import { BaseModel } from '../../_base/crud';

export class Permission extends BaseModel {
    permissionId: number;
    tag: string;
    description: string;

    clear(): void {
        this.permissionId = 0;
        this.description = "";
        this.tag = "";
	}
}
