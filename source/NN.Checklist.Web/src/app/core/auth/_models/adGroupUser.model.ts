import { BaseModel } from '../../_base/crud';

export class AdGroupUser extends BaseModel {

	groupName: string;
    adGroupUserId: number;
    adGroupId: number;
    userId: number;


    clear(): void {
		this.groupName = "";
		this.adGroupUserId = 0;
		this.adGroupId = 0;
		this.userId = 0;
    }
}
