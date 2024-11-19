import { BaseModel } from '../../_base/crud';

export class OccurrenceRecordFilter extends BaseModel {

	startDate: Date;
	endDate: Date;
	name: Date;
	statusName: string;
	status: number;
	lastCondition: string;
	process: number;
	area: number;
	idType: number;
	sendMail: string;
	alarm: string;
	impacts: boolean;
	endDateFilled: boolean;
	assessmentNeeded: boolean;
	release: boolean;

    clear(): void {
		this.startDate = null;
		this.endDate = null;
		this.name = null;
		this.status = 0;
		this.process = 0;
		this.area = 0;
		this.idType = 0;
		this.sendMail = null;
		this.alarm = null;
		this.impacts = null;
		this.endDateFilled = null;
		this.assessmentNeeded = null;
    }
}
