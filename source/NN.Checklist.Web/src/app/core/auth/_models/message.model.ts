import { Status } from './status.model';
import { User } from './user.model';
import { BaseModel } from '../../_base/crud';
import { Area } from './area.model';
import { OccurrenceRecord } from './occurenceRecord.model';
import { MessageStatus } from './messageStatus.model';

export class Message extends BaseModel {

	messageId: number;
	dateTimeCreation: Date;
	typeNotificationChannelName: string;
	lastStatus: string;
	userId: number;
	areaId: number;
	occurrenceRecordId: number;
	number: string;
	text: string;
	subject: string;
	parameters: string;
	twilioId: string;
	userInitials: string;

	status: string;
	messageStatus: MessageStatus[];
	user: User
	area: Area;
	occurrenceRecord: OccurrenceRecord


    clear(): void {
		this.messageId = 0;
		this.dateTimeCreation = null;
		this.typeNotificationChannelName = "";
		this.lastStatus = "";
		this.userId = 0;
		this.areaId = 0;
		this.occurrenceRecordId = 0;
		this.number = "";
		this.text = "";
		this.subject = "";
		this.parameters = "";
		this.twilioId = "";
		this.userInitials = "";
		this.area = null;
		this.user = null;
		this.occurrenceRecord = null;
		this.status = "";
		this.messageStatus = [];
    }
}
