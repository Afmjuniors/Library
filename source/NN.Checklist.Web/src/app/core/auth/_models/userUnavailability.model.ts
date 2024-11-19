import { BaseModel } from '../../_base/crud';
import { TypeOccurrence }  from './typeOccurrence.model';
import { TypeOccurrenceRecordGroup } from './typeOccurrenceRecordGroup.model';
import { Unit } from './unit.model';
import { Datatype } from './datatype.model';
import { Area } from './area.model';
import { AdGroup } from './adGroup.model';
import { ImpactedArea } from './impactedArea.model';

export class UserUnavailability extends BaseModel {

	userUnavailabilityId: number;
	endDate: Date;
	startDate: Date;
	userId: number;

    clear(): void {
		this.userUnavailabilityId = 0;
		this.endDate = null;
		this.startDate = null;
		this.userId = 0;
    }
}
