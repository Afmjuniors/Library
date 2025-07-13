import { User } from './user.model';
import { BaseModel } from '../../_base/crud';
import { MessageStatus } from './messageStatus.model';

export class Message extends BaseModel {

	messageId: number;
	dateTimeCreation: Date;
	typeNotificationChannelName: string;
	lastStatus: string;
	userId: number;
	areaId: number;
	number: string;
	text: string;
	subject: string;
	parameters: string;
	twilioId: string;
	userInitials: string;
	status: string;
	messageStatus: MessageStatus[];
	user: User


    clear(): void {
		this.messageId = 0;
		this.dateTimeCreation = null;
		this.typeNotificationChannelName = "";
		this.lastStatus = "";
		this.userId = 0;
		this.areaId = 0;
		this.number = "";
		this.text = "";
		this.subject = "";
		this.parameters = "";
		this.twilioId = "";
		this.userInitials = "";
		this.user = null;
		this.status = "";
		this.messageStatus = [];
    }
}
