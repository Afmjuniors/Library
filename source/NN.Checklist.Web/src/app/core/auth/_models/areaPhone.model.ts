import { BaseModel } from '../../_base/crud';
export class AreaPhone extends BaseModel {
	areaId: number;
	areaPhoneId: number;
	countryId: number;
	number: string;
	prefixNumber: number;

    clear(): void {
		this.areaId = 0;
		this.areaPhoneId = 0;
		this.countryId = 0;
		this.number = "";
		this.prefixNumber = 0;
    }
}
