import { BaseModel } from '../../_base/crud';
import { Area } from './area.model';

export class Process extends BaseModel {

	processId: number;
	acronym: string;
	description: string;
	areas: Area[];
	watchdogAlarm: boolean;
	watchdogEvent: boolean;

    clear(): void {
		this.processId = 0;
		this.acronym = "";
		this.description = "";
		this.watchdogAlarm = false;
		this.watchdogEvent = false;
    }
}
