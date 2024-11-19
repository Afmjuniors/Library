import { BaseModel } from '../../_base/crud';
import { Permission } from './permission.model';

export class AdGroup extends BaseModel {

	adGroupId: number;
	name: string;
	administrator: boolean;
	maintenance: boolean;
	impactAnalyst: boolean;
	qaAnalyst: boolean;
	permissions: Permission[];

    clear(): void {
		this.adGroupId = 0;
		this.name = "";
		this.administrator = false;
		this.maintenance = false;
		this.impactAnalyst = false;
		this.qaAnalyst = false;
		this.permissions = [];
    }
}
