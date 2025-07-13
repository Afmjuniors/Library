export class CreatedGroupRegister  {
    success:boolean;
    message:string;
   
    clear(): void {
        this.message =null;
        this.success=null;
    }
}