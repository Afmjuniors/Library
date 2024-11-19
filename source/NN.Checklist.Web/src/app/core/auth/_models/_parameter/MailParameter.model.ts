export class MailParameter {
    smtpServer:string;
    smtpPort:number;
    smtpFromAddress:string;
    smtpPassword:string;
    smtpEnabledSSL:boolean;
    
    clear(): void {
        this.smtpServer = "";
        this.smtpPort = 0;
        this.smtpFromAddress = "";
        this.smtpPassword = "";
        this.smtpEnabledSSL = false;
	}
}
