import { User } from "..";
import { QASign } from "./qaSign.model";

export class OccurrenceRecordsApprovals {
	// idTag: number;
    approved: boolean;
    comments: string;
    dateTime: Date;
    occurrenceRecordApprovalId: number;
    occurrenceRecordId: number;
    stamp: string
    user: User;
    userId: number;
    signature: QASign;
	clear(): void {
		this.approved = false;
        this.dateTime = null;
        this.comments = '';
        this.occurrenceRecordId = null;
        this.user = null;
        this.userId = null;
        this.stamp = '';
        this.occurrenceRecordApprovalId=null;
        this.signature = null;
	}
}
