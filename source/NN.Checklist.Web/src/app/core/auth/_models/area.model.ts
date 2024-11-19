import { BaseModel } from '../../_base/crud';
import { AreaPhone } from './areaPhone.model';
import { Process } from './process.model';

export class Area extends BaseModel {

	areaId: number;
	description: string;
	name: string;
	processId: number;
	process: Process;
	processName: string;
	selected: boolean;
	phones: AreaPhone[];

    clear(): void {
		this.areaId = 0;
		this.name = "";
		this.description = "";
		this.name = "";
		this.processId = 0;
		this.process = null;
		this.selected = false;
		this.phones = [];
    }
}
