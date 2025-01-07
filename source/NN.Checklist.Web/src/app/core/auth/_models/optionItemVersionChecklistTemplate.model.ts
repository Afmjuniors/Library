import { BaseModel } from '../../_base/crud';


export class OptionItemVersionChecklistTemplate extends BaseModel {

    itemVersionChecklistTemplateId: number;
    optionItemVersionChecklistTemplateId: number;
    value: string;
    title: string;
    checked:boolean;

    clear(): void {

        this.itemVersionChecklistTemplateId = 0;
        this.optionItemVersionChecklistTemplateId = 0;
        this.title = "";
        this.value = "";
        this.checked= false;

    }
}


