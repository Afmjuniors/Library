import { BaseModel } from '../../_base/crud';

export class SystemRecord extends BaseModel {
    systemRecordId: number;
	dateTime: Date;
	description: string;
	id: number;
	systemFunctionality: number;
	userId: number;	
    userInitials: string;
    functionalityName: string;
    
    clear(): void {
		this.systemRecordId = 0;
		this.dateTime = null;
		this.description = "";
		this.id = 0;
		this.systemFunctionality = 0;
		this.userId = 0;	
		this.userInitials = "";
		this.functionalityName = "";
    }
}
