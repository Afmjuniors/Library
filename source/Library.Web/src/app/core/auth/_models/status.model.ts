import { BaseModel } from '../../_base/crud';

export class Status extends BaseModel {

	statusId: number;
	description: string;

    clear(): void {
		this.statusId = 0;
		this.description = "";
    }
}
