import { BaseModel } from '../../_base/crud';

export class State extends BaseModel {

	stateId: number;
	name: string;

    clear(): void {
		this.stateId = 0;
		this.name = "";
    }
}
