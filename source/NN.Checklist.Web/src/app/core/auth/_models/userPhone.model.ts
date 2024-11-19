import { BaseModel } from '../../_base/crud';
import { TypeOccurrence }  from './typeOccurrence.model';
import { TypeOccurrenceRecordGroup } from './typeOccurrenceRecordGroup.model';
import { Unit } from './unit.model';
import { Datatype } from './datatype.model';
import { Area } from './area.model';
import { AdGroup } from './adGroup.model';
import { ImpactedArea } from './impactedArea.model';
import { Country } from './country.model';

export class UserPhone extends BaseModel {

	userPhoneId: number;
	countryId: number;
	number: string;
	userId: number;
	country: Country;

    clear(): void {
		this.userPhoneId = 0;
		this.countryId = 0;
		this.number = "";
		this.userId = 0;
		this.country = null;
    }
}
