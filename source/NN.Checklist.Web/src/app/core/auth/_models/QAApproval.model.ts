import { BaseModel } from '../../_base/crud';
import { OccurrenceAnalysisDetailsItem } from './occurrenceAnalysisDetailsItem.model';

export class QAApproval extends BaseModel {
	
	occurrences: OccurrenceAnalysisDetailsItem[];
    approved: boolean;
    review: boolean;
    description: string;
    stamp: string;

    clear(): void {
        this.approved = false;
        this.description = "";
    }
}