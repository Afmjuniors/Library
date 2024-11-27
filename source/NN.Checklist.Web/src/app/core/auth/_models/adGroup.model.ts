import { BaseModel } from '../../_base/crud';
import { Permission } from './permission.model';

export class AdGroup extends BaseModel {

	adGroupId: number;
	name: string;
	administrator: boolean;
	permissions: Permission[];

    clear(): void {
		this.adGroupId = 0;
		this.name = "";
		this.administrator = false;
		this.permissions = [];
    }
}
