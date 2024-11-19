import { BaseModel } from '../../_base/crud';
import { TypeOccurrence }  from './typeOccurrence.model';
import { TypeOccurrenceRecordGroup } from './typeOccurrenceRecordGroup.model';
import { Unit } from './unit.model';
import { Datatype } from './datatype.model';
import { Area } from './area.model';
import { AdGroup } from './adGroup.model';
import { ImpactedArea } from './impactedArea.model';

export class UserAvailabilities extends BaseModel {

	userAvailabilityId: number;
	endTime: string;
	startTime: string;
	userId: number;
	weekDay: number;

    clear(): void {
		this.userAvailabilityId = 0;
		this.endTime = "";
		this.startTime = "";
		this.userId = 0;
		this.weekDay = 0;
    }
}
