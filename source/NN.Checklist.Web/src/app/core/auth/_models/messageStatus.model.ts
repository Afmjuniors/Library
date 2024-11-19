import { User } from './user.model';
import { BaseModel } from '../../_base/crud';
import { Area } from './area.model';
import { OccurrenceRecord } from './occurenceRecord.model';

export class MessageStatus extends BaseModel {

	dateTime:Date;
	messageId:number;
	messageStatusId:number;
	typeMessageStatus:number;
	typeMessageStatusName:String;

    clear(): void {
		this.messageId = 0;
		this.dateTime = null;
		this.messageStatusId = 0;
		this.typeMessageStatus = 0;
		this.typeMessageStatusName = "";


    }
}
