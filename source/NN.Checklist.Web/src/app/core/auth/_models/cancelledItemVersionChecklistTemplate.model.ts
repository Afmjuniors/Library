import { BaseModel } from '../../_base/crud';
import { ItemVersionChecklistTemplate } from './itemVersionChecklistTemplate.model';


export class CancelledItemVersionChecklistTemplate extends BaseModel {

    cancelledItemVersionChecklistTemplateId: number;
    optionItemVersionChecklistTemplateId: number;
    targetItemVersionChecklistTemplateId: number;
    targetItemVersionChecklistTemplate: ItemVersionChecklistTemplate;




    clear(): void {

        this.cancelledItemVersionChecklistTemplateId = 0;
        this.optionItemVersionChecklistTemplateId = 0;
        this.targetItemVersionChecklistTemplateId = 0;
        this.targetItemVersionChecklistTemplate = null;

    }
}

