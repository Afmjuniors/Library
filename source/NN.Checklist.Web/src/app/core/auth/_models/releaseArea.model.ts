import { BaseModel } from '../../_base/crud';
import { Area } from './area.model';

export class ReleaseArea extends BaseModel {

	releaseAreaId: number;
	typeOccurrenceRecordId: number;
	areaId: number;
	area: Area;

    clear(): void {
		this.releaseAreaId = 0;
		this.typeOccurrenceRecordId = 0;
		this.areaId = 0;
    }
}
