import { BaseModel } from '../../_base/crud';
import { BlockVersionChecklistTemplate } from './blockVersionChecklistTemplate.model';
import { FieldVersionChecklistTemplate } from './fieldVersionChecklistTemplate.model';


export class VersionChecklistTemplate extends BaseModel {

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
    dependentVersionChecklistTemplate:VersionChecklistTemplate | null;
    blocksTree:BlockVersionChecklistTemplate[];



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
        this.dependentVersionChecklistTemplate = null;
        this.blocksTree=null;

    }
}

