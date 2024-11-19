import { BaseModel } from '../../_base/crud';
//rotas.model.ts
export class DropDownList {

    id: number;
    valor: string;
    isSelected: boolean;

    clear(): void {
        this.id = 0;
        this.valor = "";
        this.isSelected = false;
    }

}