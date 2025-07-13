import { BaseModel } from '../../_base/crud';
import { CancelledItemVersionChecklistTemplate } from './cancelledItemVersionChecklistTemplate.model';


export class OptionItemVersionChecklistTemplate extends BaseModel {

    itemVersionChecklistTemplateId: number;
    optionItemVersionChecklistTemplateId: number;
    value: string;
    title: string;
    checked:boolean;
    cancelledItemsVersionChecklistTemplate:CancelledItemVersionChecklistTemplate[];

    clear(): void {

        this.itemVersionChecklistTemplateId = 0;
        this.optionItemVersionChecklistTemplateId = 0;
        this.title = "";
        this.value = "";
        this.checked= false;
        this.cancelledItemsVersionChecklistTemplate = null;

    }
}

