export class DomainParameter {
    domainAddress: string;
    adminUsername: string;
    adminPassword: string;
    
    clear(): void {
        this.domainAddress = '';
        this.adminUsername = '';
        this.adminPassword = '';
	}
}
