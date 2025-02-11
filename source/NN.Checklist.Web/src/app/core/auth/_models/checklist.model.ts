
import { BaseModel } from '../../_base/crud';
import { DependencyItemVersionChecklistTemplate } from './dependencyItemVersionChecklistTemplate.model';
import { FieldChecklist } from './fieldChecklist.model';
import { ItemChecklist } from './itemChecklist.model';
import { VersionChecklistTemplate } from './versionChecklistTemplate.model';


export class ChecklistModel extends BaseModel {

    checklistId: number | null;
    versionChecklistTemplateId: number;
    creationTimestamp: Date;
    creationUserId: number;
    updateTimestamp: Date;
    updateUserId: number;
    fields: FieldChecklist[];
    items: ItemChecklist[];
    dependentChecklistId:number;
    formattedDate:Date;
    isCompleted:boolean;
    versionChecklistTemplate:VersionChecklistTemplate;


    clear(): void {


        this.checklistId = 0;
        this.versionChecklistTemplateId = 0;
        this.creationTimestamp = null;
        this.creationUserId = 0;
        this.updateTimestamp = null;
        this.updateUserId = 0;
        this.fields = null;
        this.items = null;
        this.dependentChecklistId = 0;
        this.versionChecklistTemplate = null;
        this.formattedDate=null;
        this.isCompleted=null;


    }
}

