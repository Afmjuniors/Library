import { BaseModel } from '../../_base/crud';
import { DependencyBlockVersionChecklistTemplate } from './dependencyBlockVersionChecklistTemplate.model';
import { ItemVersionChecklistTemplate } from './itemVersionChecklistTemplate.model';


export class BlockVersionChecklistTemplate extends BaseModel {

    blockVersionChecklistTemplateId: number;
    parentBlockVersionChecklistTemplateId: number;
    versionChecklistTemplateId: number;
    position: number;
    title: string;
    isCompleted:boolean;
    isDisabled:boolean;
    absolutePositionString:string;
    lastPosition:number;
    dependentBlockVersionChecklistTemplate: DependencyBlockVersionChecklistTemplate[];
    itemsChecklistsTemplate:ItemVersionChecklistTemplate[]
    blocks:BlockVersionChecklistTemplate[]


    clear(): void {
        this.blockVersionChecklistTemplateId = 0;
        this.parentBlockVersionChecklistTemplateId = 0;
        this.versionChecklistTemplateId = 0;
        this.position = 0;
        this.title = "";
        this.dependentBlockVersionChecklistTemplate = null;
        this.itemsChecklistsTemplate = null;
        this.isCompleted=null;
        this.isDisabled=null;
        this.absolutePositionString="";
        this.lastPosition=0;
        this.blocks=null;

    }
}





