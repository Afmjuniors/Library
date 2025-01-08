
import { BaseModel } from '../../_base/crud';


export class FieldChecklist extends BaseModel {

    checklistId: number | null;
    versionChecklistTemplateId: number;
    creationTimestamp: Date | null;
    creationUserId: number | null;
    updateTimestamp: Date| null;
    updateUserId: number| null;


    fieldChecklistId: number | null;
    fieldVersionChecklistTemplateId: number;
    optionFieldVersionChecklistTemplateId: number | null;
    value: string;

    clear(): void {
        this.fieldChecklistId = 0;
        this.fieldVersionChecklistTemplateId = 0;
        this.optionFieldVersionChecklistTemplateId = 0;
        this.value = "";
        this.checklistId = 0;
        this.versionChecklistTemplateId = 0;
        this.creationTimestamp = null;
        this.creationUserId = null;
        this.updateTimestamp = null;
        this.updateUserId = null;

    }
}


