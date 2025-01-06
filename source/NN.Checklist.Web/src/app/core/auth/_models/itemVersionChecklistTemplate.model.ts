import { BaseModel } from '../../_base/crud';
import { DependencyItemVersionChecklistTemplate } from './dependencyItemVersionChecklistTemplate.model';
import { OptionFieldVersionChecklistTemplate } from './optionFieldVersionChecklistTemplate.model';


export class ItemVersionChecklistTemplate extends BaseModel {

    blockVersionChecklistTemplateId: number;
    itemVersionChecklistTemplateId: number;
    parentBlockVersionChecklistTemplateId: number;
    versionChecklistTemplateId: number;
    itemTypeId: number;
    optionFieldVersionChecklistTemplateId: number;
    optionsTitle: string;
    optionFieldVersionChecklistTemplate: OptionFieldVersionChecklistTemplate;
    DependencyItemVersionChecklistTemplate: DependencyItemVersionChecklistTemplate[];
    position: number;
    title: string;


    clear(): void {

        this.blockVersionChecklistTemplateId = 0;
        this.itemVersionChecklistTemplateId = 0;
        this.parentBlockVersionChecklistTemplateId = 0;
        this.versionChecklistTemplateId = 0;
        this.itemTypeId = 0;
        this.optionFieldVersionChecklistTemplateId = 0;
        this.optionsTitle = "";
        this.optionFieldVersionChecklistTemplate = null;
        this.DependencyItemVersionChecklistTemplate = null;
        this.position = 0;
        this.title = "";


    }
}

