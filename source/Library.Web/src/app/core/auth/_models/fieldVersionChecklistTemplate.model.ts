import { BaseModel } from '../../_base/crud';
import { OptionFieldVersionChecklistTemplate } from './optionFieldVersionChecklistTemplate.model';


export class FieldVersionChecklistTemplate extends BaseModel {

    fieldVersionChecklistTemplateId: number;
    versionChecklistTemplateId: number;    
    position: number;
    title: string;
    fieldDataTypeId: number;
    regexValidation: string;
    format: string;
    mandatory: boolean;
    isDisable: boolean;
    isKey: boolean;
    optionFieldVersionChecklistTemplate: OptionFieldVersionChecklistTemplate[];

    value:any;

    clear(): void {
        this.fieldVersionChecklistTemplateId=0;
        this.versionChecklistTemplateId=0;    
        this.position=0;
        this.title="";
        this.fieldDataTypeId=0;
        this.regexValidation="";
        this.format="";
        this.mandatory=null;
        this.isKey=null;
        this.isDisable = false;
        this.value=null;
        this.optionFieldVersionChecklistTemplate=null;


    
    }
}

