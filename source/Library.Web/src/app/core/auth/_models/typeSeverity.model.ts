import { BaseModel } from '../../_base/crud';

export class TypeSeverity extends BaseModel {

    typeSeverityId: number;
    name: string;
    clear(): void {
		this.typeSeverityId = 0;
		this.name = "";
    }
}
