import { BaseModel } from '../../_base/crud';

export class ChecklistFilter extends BaseModel {

	startDate: Date;
	endDate: Date;
	checklistTemplateID: number;
	versionChecklistTemplateId: number;

    clear(): void {
		this.startDate = null;
		this.endDate = null;
		this.checklistTemplateID = 0;
		this.versionChecklistTemplateId = 0;

    }
}
