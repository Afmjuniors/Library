import { BaseModel } from '../../_base/crud';

export class TypeComponent extends BaseModel {

    TypeComponentId: number;
    name: string;
    clear(): void {
        this.TypeComponentId = 0;
        this.name = "";
    }
}
