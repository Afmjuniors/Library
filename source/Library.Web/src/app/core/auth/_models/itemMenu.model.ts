import { BaseModel } from '../../_base/crud';

export class ItemMenu extends BaseModel {
    title: string;
    root: boolean;
    alignment: string;
    toggle: string;
    bullet: string;
    icon: string;
    page: string;
    translate: string;
    submenu: ItemMenu[];

    clear(): void {
        this.title = "";
        this.root = false;
        this.alignment = "";
        this.toggle = "";
        this.bullet = "";
        this.icon = "";
        this.page = "";
        this.translate = "";
        this.submenu = [];
    }
}
