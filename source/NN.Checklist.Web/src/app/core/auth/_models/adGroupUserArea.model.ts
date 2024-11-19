import { BaseModel } from '../../_base/crud';
import { Area } from './area.model';

export class AdGroupUserArea extends BaseModel {

	adGroupUserAreaId: number;
	adGroupUserId: number;
	areaId: number;
	area: Area;

    clear(): void {
		this.adGroupUserAreaId = 0;
		this.adGroupUserId = 0;
		this.areaId = 0;
		this.area = null;
    }
}
