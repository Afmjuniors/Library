import { BaseModel } from '../../_base/crud';

export class TypeEventCategory extends BaseModel {
	description: string;
    typeEventCategoryId: number;

    clear(): void {
		this.description = '';
		this.typeEventCategoryId = 0;
    }
}
