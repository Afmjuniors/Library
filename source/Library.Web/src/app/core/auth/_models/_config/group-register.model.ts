import { BaseModel } from "../../../../core/_base/crud";

export class GroupRegister  extends BaseModel  {
    typeOccurrenceGroupDTO: number;
    description: string;
    
    clear(): void {
        this.typeOccurrenceGroupDTO = 0;
        this.description = null;
	}
}