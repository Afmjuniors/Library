export class SignApproval {
    initials: string;
    dthSign: Date;
    result: boolean;
    dthSignFormatted:string;

    public clear(): void {
        this.initials = '';
        this.dthSignFormatted = '';

        this.dthSign = null;
        this.result = false;
    }
}
