import { BaseModel } from '../../_base/crud';
import { OccurrenceRecordsAnalysis } from './occurrenceRecordsAnalysis.model';
import { OccurrenceRecordsApprovals } from './occurrenceRecordsApprovals.modal';
import { OccurrenceRecordsStatus } from './occurrenceRecordsStatus.model';
import { State } from './state.model';
import { SystemNode } from './systemNode.model';
import { TypeEventCategory } from './typeEventCategory.model';
import { TypeOccurenceRecord } from './typeOccurrenceRecord.model';
import { TypeSeverity } from './typeSeverity.model';

export class OccurrenceArea extends BaseModel {
    number: number;
    areaName: string;
    process: string;
    status: number;
    clear(): void {
		this.number = 0;
		this.status = 0;
		this.areaName = "";
		this.process = "";
    }
}
