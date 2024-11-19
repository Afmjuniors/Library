import { BaseModel } from '../../_base/crud';

export class TypeOccurrence extends BaseModel {

	typeOccurenceId: number;
	description: string;

    clear(): void {
		this.typeOccurenceId = 0;
		this.description = "";
    }
}
