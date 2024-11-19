import { BaseModel } from '../../_base/crud';

export class Language extends BaseModel {
    userId: number;
    language: string;

    clear(): void {
        this.userId = 0;
        this.language = '';
    }
}
