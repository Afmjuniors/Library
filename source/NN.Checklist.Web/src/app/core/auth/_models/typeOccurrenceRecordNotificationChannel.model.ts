import { BaseModel } from '../../_base/crud';

export class TypeOccurrenceRecordNotificationChannel extends BaseModel {

	typeOccurrenceRecordNotificationChannelId:number;
	typeNotificationChannel:number;
	typeOccurrenceRecordId:number;


    clear(): void {
		this.typeOccurrenceRecordNotificationChannelId = 0;
		this.typeNotificationChannel = 0;
		this.typeOccurrenceRecordId = 0;
	}
}
