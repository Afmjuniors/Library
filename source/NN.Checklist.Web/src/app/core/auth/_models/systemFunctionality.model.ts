import { BaseModel } from '../../_base/crud';

export class SystemFunctionality extends BaseModel {
    systemFunctionalityId: number;
	description: string;
    
    clear(): void {
		this.systemFunctionalityId = 0;
		this.description = "";
    }
}
