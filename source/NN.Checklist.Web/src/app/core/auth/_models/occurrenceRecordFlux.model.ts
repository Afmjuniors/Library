import { BaseModel } from '../../_base/crud';

export class OccurrenceRecordFlux extends BaseModel {

	occurrenceRecordId: number;
	dateTime: Date;
	userId: number;
	initials: string;
	type: string;
	result: boolean;
	resultText: boolean;
	comments: string;

    
}
