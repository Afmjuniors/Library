import { BaseModel } from '../../_base/crud';
import { OccurrenceRecordFilter } from './occurrenceRecordFilter.model';

export class QAReport extends BaseModel {

	occurrenceRecordFilter: OccurrenceRecordFilter;
	occurrenceIds: number[];

    clear(): void {
		this.occurrenceRecordFilter = null;
        this.occurrenceIds = null;
    }
}
