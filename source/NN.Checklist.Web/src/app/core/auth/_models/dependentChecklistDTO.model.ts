import { BaseModel } from '../../_base/crud';
import { User } from './user.model';

export class DependentChecklist extends BaseModel {

    checklistId:number;
    versionChecklistTemplateId:number;
    checklistTemplateDescription:string;
    checklistTemplateVersion:string;
    creationTimestamp:number;
    updateTimestamp:number;
    creationUser:User;
    description:string;

    clear(): void {
        this.checklistId = 0;
        this.versionChecklistTemplateId = 0;
        this.checklistTemplateDescription = '';
        this.checklistTemplateVersion = '';
        this.creationTimestamp = null;
        this.updateTimestamp = null;
        this.creationUser = null;
        this.description = '';
    }
}







        



