import { BaseModel } from '../../_base/crud';
import { TypeOccurrence }  from './typeOccurrence.model';
import { TypeOccurrenceRecordGroup } from './typeOccurrenceRecordGroup.model';
import { Unit } from './unit.model';
import { Datatype } from './datatype.model';
import { Area } from './area.model';
import { AdGroup } from './adGroup.model';
import { ImpactedArea } from './impactedArea.model';

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
