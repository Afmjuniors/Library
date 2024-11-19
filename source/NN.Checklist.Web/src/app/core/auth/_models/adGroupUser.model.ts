import { BaseModel } from '../../_base/crud';
import { AdGroupUserArea } from './adGroupUserArea.model';

export class AdGroupUser extends BaseModel {

	groupName: string;
    adGroupUserId: number;
    adGroupId: number;
    userId: number;
	adGroupUserAreas: AdGroupUserArea[];


    clear(): void {
		this.groupName = "";
		this.adGroupUserId = 0;
		this.adGroupId = 0;
		this.userId = 0;
		this.adGroupUserAreas = [];
    }
}
