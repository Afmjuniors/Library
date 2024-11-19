import { User } from "..";
import { Status } from "./status.model";

export class OccurrenceRecordsStatus {
	// idTag: number;
    dateTime: Date;
    occurrenceRecordId: number;
    occurrenceRecordStatusId: number;
    status: Status;
    statusId: number;
    
	clear(): void {
        this.dateTime = null;
        this.occurrenceRecordId = null;
        this.occurrenceRecordStatusId = null;
        this.statusId = null;
        this.status = null;
	}
}
