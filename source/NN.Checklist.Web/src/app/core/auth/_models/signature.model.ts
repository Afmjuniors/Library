export class Signature {
    username: string;
    password: string;
    validationDate: Date;
    cryptData: string;
    result: boolean;

    public clear(): void {
        this.username = '';
        this.password = '';
        this.validationDate = null;
        this.cryptData = '';
        this.result = false;
    }
}
