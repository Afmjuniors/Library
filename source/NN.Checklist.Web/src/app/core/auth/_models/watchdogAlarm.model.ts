import { BaseModel } from '../../_base/crud';

export class WatchdogAlarm extends BaseModel {

	twilioAlarm: boolean;
	databaseAlarm: boolean;


    clear(): void {
		this.twilioAlarm = false;
		this.databaseAlarm = false;
    }
}
