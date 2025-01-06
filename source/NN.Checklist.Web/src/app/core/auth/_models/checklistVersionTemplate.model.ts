import { BaseModel } from '../../_base/crud';
import { BlockVersionChecklistTemplate } from './blockVersionChecklistTemplate.model';
import { FieldVersionChecklistTemplate } from './FieldVersionChecklistTemplate.model';


export class ChecklistVersionTemplate extends BaseModel {

    versionChecklistTemplateId: number;
    checklistTemplateId: number;
    checklistTemplateDescription: string;
    version: string;
    timestampCreation: Date;
    creationUserID: number;
    timestampUpdate: Date | null;
    updateUserId: number;
    fieldsVersionChecklistsTemplate:FieldVersionChecklistTemplate[];
    blocksChecklistTemplate:BlockVersionChecklistTemplate[];



    clear(): void {
        this.versionChecklistTemplateId = 0;
        this.checklistTemplateId = 0;
        this.version = "";
        this.checklistTemplateDescription = "";

        this.timestampCreation = null;
        this.creationUserID = 0;
        this.timestampUpdate = null;
        this.updateUserId = 0;
        
        this.fieldsVersionChecklistsTemplate = null;
        this.blocksChecklistTemplate = null;
    }
}





