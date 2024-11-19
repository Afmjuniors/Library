import { BaseModel } from '../../_base/crud';

export class OccurrenceAnalysisDetails extends BaseModel {
	idOccurrenceRecord:number
	alarm: string;
	description: string;
	process: string;
	area: string;
	impactedAreaId: number;
	dateRecord: Date;
	dateStart: Date;
	dateEnd: Date;
	status: string;
	responsible: string;
	dateAnalisys: Date;
	dateAck: Date;
	ackUser: string;
	node: string;
	extremeValue: string;
	unit: string;
	impact: boolean;
	impactDetails: string;
	stamp: string;
	lastCausesImpacts: boolean;
	lastDateTimeImpacts: Date;
	lastImpactDetails: string;
	lastUserIdImpacts: number;
	lastUserNameImpacts: string;

    clear(): void {
		this.idOccurrenceRecord = 0;
		this.alarm = "";
		this.description = "";
		this.process = "";
		this.area = "";
		this.dateRecord = null;
		this.dateStart = null;
		this.dateEnd = null;
		this.status = "";
		this.responsible = "";
		this.dateAnalisys = null;
		this.dateAck = null;
		this.ackUser = "";
		this.node = "";
		this.extremeValue = "";
		this.unit = "";
		this.impact = false;
		this.impactDetails = "";
		this.stamp = "";
		this.lastCausesImpacts = false;
		this.lastDateTimeImpacts = null;
		this.lastImpactDetails = "";
		this.lastUserIdImpacts = 0;
		this.lastUserNameImpacts = "";
    }
}
