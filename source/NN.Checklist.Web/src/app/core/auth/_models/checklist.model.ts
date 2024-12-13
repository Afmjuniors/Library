import { BaseModel } from '../../_base/crud';


export class Checklist extends BaseModel {

    checklistId: number;
    creationTimestamp: Date;
    creationUserID: number;
    updateTimeStamp: Date;
    updatedUserID: number;
    versionChecklistID: number;
    versionChecklist: string;
    clear(): void {
        this.checklistId = 0;
        this.checklistId = 0;
        this.creationTimestamp = null;
        this.creationUserID = 0;
        this.updateTimeStamp = null;
        this.updatedUserID = 0;
        this.versionChecklistID = 0;
        this.versionChecklist = "";
    }
}
