import { BaseModel } from '../../_base/crud';
import { DependencyBlockVersionChecklistTemplate } from './dependencyBlockVersionChecklistTemplate.model';
import { ItemVersionChecklistTemplate } from './itemVersionChecklistTemplate.model';


export class BlockVersionChecklistTemplate extends BaseModel {

    blockVersionChecklistTemplateId: number;
    parentBlockVersionChecklistTemplateId: number;
    versionChecklistTemplateId: number;
    position: number;
    title: string;
    dependentBlockVersionChecklistTemplate: DependencyBlockVersionChecklistTemplate[];
    itemsChecklistsTemplate:ItemVersionChecklistTemplate[]


    clear(): void {
        this.blockVersionChecklistTemplateId = 0;
        this.parentBlockVersionChecklistTemplateId = 0;
        this.versionChecklistTemplateId = 0;
        this.position = 0;
        this.title = "";
        this.dependentBlockVersionChecklistTemplate = null;
        this.itemsChecklistsTemplate = null;

    }
}





