import { BaseModel } from '../../_base/crud';
import { OccurrenceAnalysisDetailsItem } from './occurrenceAnalysisDetailsItem.model';

export class AnalysisDetails extends BaseModel {
	occurrences: OccurrenceAnalysisDetailsItem[] = []	
}
