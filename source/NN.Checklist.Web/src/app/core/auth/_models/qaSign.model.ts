import { BaseModel } from '../../_base/crud';

export class QASign extends BaseModel {

	initials: string;
	dthSign: Date;
    result: boolean;
    type: string;
    clear(): void {
		this.initials = "";
		this.dthSign = null;
        this.result = false;
        this.type = '';
    }
}
