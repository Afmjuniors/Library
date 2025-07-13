import { BaseModel } from '../../_base/crud';

export class Datatype extends BaseModel {

	datatypeId: number;
	name: string;
	typename: string;

    clear(): void {
		this.datatypeId = 0;
		this.name = "";
		this.typename = "";
    }
}
