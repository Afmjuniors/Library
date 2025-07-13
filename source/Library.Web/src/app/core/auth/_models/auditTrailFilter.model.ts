import { BaseModel } from '../../_base/crud';

export class AuditTrailFilter extends BaseModel {

	startDate: Date;
	endDate: Date;
	keyword: string;
	functionalityId: number;
	userId: number;

    clear(): void {
		this.startDate = null;
		this.endDate = null;
		this.keyword = null;
		this.functionalityId = 0;
		this.userId = 0;
    }
}
