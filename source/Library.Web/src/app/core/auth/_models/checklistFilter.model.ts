import { BaseModel } from '../../_base/crud';

export class ChecklistFilter extends BaseModel {

	startDate: Date;
	endDate: Date;
	checklistId:number;
	checklistTemplateId: number;
	versionChecklistTemplateId: number;

    clear(): void {
		this.startDate = null;
		this.endDate = null;
		this.checklistId = 0;
		this.checklistTemplateId = 0;
		this.versionChecklistTemplateId = 0;

    }
}
