import { User } from "..";
import { QASign } from "./qaSign.model";

export class OccurrenceRecordsAnalysis {
	// idTag: number;
	causesImpacts: boolean;
    dateTime: Date;
    impactsDetails: string;
    occurrenceRecordAnalysisId: number;
    occurrenceRecordId: number;
    areaId: number;
    stamp: string;
	selected: boolean;
    user: User;
    signature: QASign;
	clear(): void {
		this.causesImpacts = false;
        this.dateTime = null;
        this.impactsDetails = '';
        this.occurrenceRecordAnalysisId = null;
        this.occurrenceRecordId = null;
        this.areaId = null;
        this.stamp = '';
        this.user = null;
		this.selected = false;
        this.signature = null;
	}
}
