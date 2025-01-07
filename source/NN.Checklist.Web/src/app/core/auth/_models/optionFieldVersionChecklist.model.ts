import { BaseModel } from '../../_base/crud';


export class OptionFieldVersionChecklist extends BaseModel {

    fieldVersionChecklistTemplateId: number;
    optionFieldVersionChecklistTemplateId: number;
    value: string;
    title: string;
    checked:boolean;


    clear(): void {

        this.fieldVersionChecklistTemplateId = 0;
        this.optionFieldVersionChecklistTemplateId = 0;
        this.title = "";
        this.value = "";
        this.checked=false;

    }
}


