import { BaseModel } from '../../_base/crud';
import { Area } from './area.model';

export class ImpactedArea extends BaseModel {

	impactedAreaId: number;
	typeOccurrenceRecordId: number;
	areaId: number;
	area: Area;

    clear(): void {
		this.impactedAreaId = 0;
		this.typeOccurrenceRecordId = 0;
		this.areaId = 0;
    }
}
