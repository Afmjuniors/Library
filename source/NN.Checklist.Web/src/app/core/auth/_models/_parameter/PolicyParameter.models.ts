export class PolicyParameter {
    inactivityTimeLimit: number;
	timeResendAlarmsNotification: number;
	messageNotificationExpirationTime: number;
	maximumNotificationResendTime: number;

    clear(): void {
        this.inactivityTimeLimit = 0;
		this.timeResendAlarmsNotification = 0;
		this.messageNotificationExpirationTime = 0;
		this.maximumNotificationResendTime = 0;
	}
}
