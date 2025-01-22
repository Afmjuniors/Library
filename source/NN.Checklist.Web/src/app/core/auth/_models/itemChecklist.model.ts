
import { BaseModel } from '../../_base/crud';
import { ItemVersionChecklistTemplate } from './itemVersionChecklistTemplate.model';
import { SignApproval } from './signAprovval.model';
import { Signature } from './signature.model';


export class ItemChecklist extends BaseModel {

    checklistId: number | null;
    versionChecklistTemplateId: number;
    creationTimestamp: Date;
    creationUserId: number;
    itemVersionChecklistTemplate:ItemVersionChecklistTemplate;
    stamp:string;

    itemChecklistId: number | null;
    blockVersionTemplateId: number | null;

    itemVersionChecklistTemplateId: number;
    comments:string;
    signature:SignApproval;


    clear(): void {
        this.itemChecklistId = 0;
        this.itemVersionChecklistTemplateId = 0;
        this.checklistId = 0;
        this.versionChecklistTemplateId = 0;
        this.creationTimestamp = null;
        this.creationUserId = 0;
        this.stamp = "";
        this.comments = "";
        this.blockVersionTemplateId = null;
        this.signature=null;

    }
    /**
     *
     */
    constructor(_checklistId:number|null,_versionChecklistTemplateId:number,_blockVersionTemplateId:number,_stamp:string,_itemVersionChecklistTemplateId:number,_comments:string) {
        super();
        this.checklistId = _checklistId;
        this.versionChecklistTemplateId = _versionChecklistTemplateId;
        this.stamp = _stamp;
        this.itemVersionChecklistTemplateId =_itemVersionChecklistTemplateId;
        this.blockVersionTemplateId = _blockVersionTemplateId;
        this.comments = _comments;
        
    }
    public insertStamp(_signature:SignApproval){
        this.signature = _signature;
    }
}



