import { BaseModel } from '../../_base/crud';


export class Country extends BaseModel {

	countryId: number;
	name: string;
	prefixNumber: number;

	clear(): void {
		this.countryId = 0;
		this.name = "";
		this.prefixNumber = 0;
    }
}
