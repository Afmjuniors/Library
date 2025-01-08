
import { BaseModel } from '../../_base/crud';


export class ItemChecklist extends BaseModel {

    checklistId: number | null;
    cersionChecklistTemplateId: number;
    creationTimestamp: Date;
    creationUserId: number;

    stamp:string;

    itemChecklistId: number | null;
    itemVersionChecklistTemplateId: number;
    comments:string;

    clear(): void {
        this.itemChecklistId = 0;
        this.itemVersionChecklistTemplateId = 0;
        this.checklistId = 0;
        this.cersionChecklistTemplateId = 0;
        this.creationTimestamp = null;
        this.creationUserId = 0;
        this.stamp = "";
        this.comments = "";

    }
}



