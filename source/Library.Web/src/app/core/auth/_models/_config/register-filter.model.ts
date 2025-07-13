export class RegisterFilter  {
    tag:string;
    processId:number;
    typeId:number;
    areaId:number;
    onlyWithoutGroup:boolean;
	sendMail: string;
    assesmentNeeded: boolean;
    clear(): void {
        this.tag =null;
        this.processId=null;
        this.areaId = null;
        this.typeId = null;
		this.sendMail = null;
        this.assesmentNeeded = null;
    }
}
