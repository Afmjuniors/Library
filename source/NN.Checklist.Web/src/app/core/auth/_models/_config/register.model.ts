import { BaseModel } from "../../../../core/_base/crud";

export class Register  extends BaseModel   {
    areaId: number;
    typeOccurrenceRecordId: number;
    processId: number;
    typeId: number;
    area: string;
    process: string;
    tag: string;
    type: string;
    typeOccurrenceRecordGroupId: number;
	selected: boolean;

    clear(): void {
        this.tag = null;
        this.process = null;
        this.area = null;
        this.type = null;
        this.typeOccurrenceRecordGroupId = null;
		this.selected = false;
	}
}
