import { BaseModel } from '../../_base/crud';

export class Unit extends BaseModel {

	unitId: number;
	acronym: string;
	description: string;

    clear(): void {
		this.unitId = 0;
		this.acronym = "";
		this.description = "";
    }
}
