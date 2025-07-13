
export class ParametroSQLServer {
	// idTag: number;
	alarmBlackConnectionString: string;
	alarmCleanConnectionString: string;
	alarmFMSConnectionString: string;
	auditBlackConnectionString: string;
	auditCleanConnectionString: string;
	auditFMSConnectionString: string;

	clear(): void {
		this.alarmBlackConnectionString = '';
		this.alarmCleanConnectionString = '';
		this.alarmFMSConnectionString = '';
		this.auditBlackConnectionString = '';
		this.auditCleanConnectionString = '';
		this.auditFMSConnectionString = '';
	}
}
