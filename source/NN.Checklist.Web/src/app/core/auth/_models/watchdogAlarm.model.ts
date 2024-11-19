import { Status } from './status.model';
import { User } from './user.model';
import { BaseModel } from '../../_base/crud';
import { Area } from './area.model';
import { OccurrenceRecord } from './occurenceRecord.model';
import { MessageStatus } from './messageStatus.model';
import { Process } from './process.model';

export class WatchdogAlarm extends BaseModel {

	twilioAlarm: boolean;
	processes: Process[];
	databaseAlarm: boolean;


    clear(): void {
		this.twilioAlarm = false;
		this.processes = [];
		this.databaseAlarm = false;
    }
}
