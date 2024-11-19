import { BaseModel } from '../../_base/crud';

export class MessageFilter extends BaseModel {

	occurrenceRecordId: number;
	typeOccurrenceRecordId: number;

    clear(): void {
		this.occurrenceRecordId = 0;
		this.typeOccurrenceRecordId = 0;
    }
}
