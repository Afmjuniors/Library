import { BaseModel } from '../../_base/crud';

export class TypeOccurrenceRecordGroup extends BaseModel {

	typeOccurrenceRecordGroupId: number;
	description: string;

    clear(): void {
		this.typeOccurrenceRecordGroupId = 0;
		this.description = "";
    }
}
