import { BaseModel } from '../../_base/crud';

export class ColaboradorFiltro extends BaseModel {
    nome: string;
    // ativo: boolean;
    
    clear(): void {
        this.nome = null;
        // this.ativo = null;
	}
}