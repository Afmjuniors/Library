import { BaseModel } from '../../_base/crud';

export class PermissaoAcao extends BaseModel {
    tag: string;
    valor: boolean;
    atualizado: boolean = false;

    clear(): void {
        this.tag = '';
        this.valor = false;
        this.atualizado = false;
	}
}
