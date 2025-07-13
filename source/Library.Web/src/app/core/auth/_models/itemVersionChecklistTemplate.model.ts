import { BaseModel } from '../../_base/crud';
import { DependencyItemVersionChecklistTemplate } from './dependencyItemVersionChecklistTemplate.model';
import { OptionFieldVersionChecklistTemplate } from './optionFieldVersionChecklistTemplate.model';
import { OptionItemVersionChecklistTemplate } from './optionItemVersionChecklistTemplate.model';


export class ItemVersionChecklistTemplate extends BaseModel {

    blockVersionChecklistTemplateId: number;
    itemVersionChecklistTemplateId: number;
    parentBlockVersionChecklistTemplateId: number;
    versionChecklistTemplateId: number;
    itemTypeId: number;
    optionFieldVersionChecklistTemplateId: number;
    optionsTitle: string;
    optionFieldVersionChecklistTemplate: OptionFieldVersionChecklistTemplate[];
    optionItemsVersionChecklistTemplate: OptionItemVersionChecklistTemplate[];
    dependencyItemVersionChecklistTemplate: DependencyItemVersionChecklistTemplate[];
    position: number;
    title: string;
    isDisabled:boolean;


    clear(): void {

        this.blockVersionChecklistTemplateId = 0;
        this.itemVersionChecklistTemplateId = 0;
        this.parentBlockVersionChecklistTemplateId = 0;
        this.versionChecklistTemplateId = 0;
        this.itemTypeId = 0;
        this.optionFieldVersionChecklistTemplateId = 0;
        this.optionsTitle = "";
        this.optionFieldVersionChecklistTemplate = null;
        this.dependencyItemVersionChecklistTemplate = null;
        this.position = 0;
        this.title = "";
        this.isDisabled = false;


    }
}

