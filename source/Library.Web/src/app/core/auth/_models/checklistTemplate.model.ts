import { BaseModel } from '../../_base/crud';


export class ChecklistTemplate extends BaseModel {

    checklistTemplateId: number;
    description: string;
    clear(): void {
        this.checklistTemplateId = 0;
        this.description = "";

    }
}
