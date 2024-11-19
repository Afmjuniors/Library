import { BaseModel } from '../../_base/crud';

export class QADetailFilter extends BaseModel {
	
	occurrenceRecordId: number;
    
    clear(): void {
		this.occurrenceRecordId = null;
    }
}
