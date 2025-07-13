import { BaseModel } from '../../_base/crud';

export class SystemNode extends BaseModel {

	systemNodeId: number;
	name: string;

    clear(): void {
		this.systemNodeId = 0;
		this.name = "";
    }
}
