import { BaseModel } from '../../_base/crud';
import { ItemMenu } from './ItemMenu.model';

export class Menu extends BaseModel {
    self: string;
    items: ItemMenu[];

    clear(): void {
        this.self = "";
        this.items = [];
    }
}
