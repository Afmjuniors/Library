import { BaseModel } from '../../_base/crud';


export class DependencyBlockVersionChecklistTemplate extends BaseModel {

    blockVersionChecklistTemplateId: number;
    dependencyBlockVersionChecklistTemplateId: number;
    dependentBlockVersionChecklistTemplateId: number;
    dependentItemVersionChecklistTemplateId: number;




    clear(): void {
        this.blockVersionChecklistTemplateId = 0;
        this.dependencyBlockVersionChecklistTemplateId = 0;
        this.dependentBlockVersionChecklistTemplateId = 0;
        this.dependentItemVersionChecklistTemplateId = 0;


    }
}
