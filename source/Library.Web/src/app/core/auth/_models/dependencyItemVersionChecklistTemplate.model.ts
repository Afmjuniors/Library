import { BaseModel } from '../../_base/crud';


export class DependencyItemVersionChecklistTemplate extends BaseModel {

    itemVersionChecklistTemplateId: number;
    dependencyItemVersionChecklistTemplateId: number;
    dependentBlockVersionChecklistTemplateId: number;
    dependentItemVersionChecklistTemplateId: number;




    clear(): void {
        this.itemVersionChecklistTemplateId = 0;
        this.dependencyItemVersionChecklistTemplateId = 0;
        this.dependentBlockVersionChecklistTemplateId = 0;
        this.dependentItemVersionChecklistTemplateId = 0;


    }
}


