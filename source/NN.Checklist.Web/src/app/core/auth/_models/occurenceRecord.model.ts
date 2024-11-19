import { BaseModel } from '../../_base/crud';
import { OccurrenceRecordsAnalysis } from './occurrenceRecordsAnalysis.model';
import { OccurrenceRecordsApprovals } from './occurrenceRecordsApprovals.modal';
import { OccurrenceRecordsStatus } from './occurrenceRecordsStatus.model';
import { State } from './state.model';
import { SystemNode } from './systemNode.model';
import { TypeEventCategory } from './typeEventCategory.model';
import { TypeOccurenceRecord } from './typeOccurrenceRecord.model';
import { TypeSeverity } from './typeSeverity.model';

export class OccurrenceRecord extends BaseModel {
    occurrenceRecordId: number;
    typeOccurrenceRecordId: number;
    alarmId: number;
    dateTimeRecord: Date;
    dateTimeStart: Date;
    dateTimeEnd: Date;
    dateTimeAck: Date;
    ackUser: string;
	systemNodeId: number;
    typeQualityId: number;
    typeSeverityId: number;
    typeEventCategoryId: number;
    extremeValue: number;
    extremeValueTimeStamp: Date;
	stateId: number;
    message: string;
	beforeValue: string;
	newValue: string;
	comment: string;
	responsible: string;
	responsibleEvent: string;
	state: State;
	selected: boolean;
	lastCondition: string;

	typeOccurrenceRecord: TypeOccurenceRecord = new TypeOccurenceRecord();
	typeEventCategory: TypeEventCategory = new TypeEventCategory();
	typeSeverity: TypeSeverity = new TypeSeverity();
	systemNode: SystemNode = new SystemNode();
	occurrenceRecordsAnalysis: OccurrenceRecordsAnalysis[];
	occurrenceRecordsStatus: OccurrenceRecordsStatus[];
	occurrenceRecordsApprovals: OccurrenceRecordsApprovals[];

    clear(): void {
		this.occurrenceRecordId = 0;
		this.typeOccurrenceRecordId = 0;
		this.alarmId = 0;
		this.dateTimeRecord = null;
		this.dateTimeStart = null;
		this.dateTimeEnd = null;
		this.dateTimeAck = null;
		this.ackUser = "";
		this.systemNodeId = 0;
		this.typeQualityId = 0;
		this.typeSeverityId = 0;
		this.typeEventCategoryId = 0;
		this.extremeValue = 0;
		this.extremeValueTimeStamp = null;
		this.stateId = 0;
		this.message = "";
		this.beforeValue = "";
		this.newValue = "";
		this.comment = "";
		this.responsible = "";
		this.responsibleEvent = "";
		this.selected = false;

		this.typeOccurrenceRecord = null;
		this.systemNode = null;
    }
}
