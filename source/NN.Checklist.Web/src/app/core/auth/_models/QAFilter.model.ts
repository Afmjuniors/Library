import { BaseModel } from '../../_base/crud';

export class QAFilter extends BaseModel {
	
	startDate: Date;
	endDate: Date;
	status: number;
	process: number;
	area: number;
	sendMail: string;
	release: boolean;
    clear(): void {
		this.startDate = null;
		this.endDate = null;
		this.status = 0;
		this.release = false;
    }
}
