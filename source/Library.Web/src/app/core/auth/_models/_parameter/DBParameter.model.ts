
export class DBParameter {
    connectionStringSqlServer: string;
    sqlServerSchema: string;
    
    clear(): void {
        this.sqlServerSchema = '';
        this.connectionStringSqlServer = '';
	}
}