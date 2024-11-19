import { BaseModel } from '../../_base/crud';

export class AlteracaoSenha extends BaseModel {
    usuario: string;
    senhaSistema: string;
    novaSenha: string;
    confirmacaoSenha: string;

    clear(): void {
        this.usuario = '';
        this.senhaSistema = '';
        this.novaSenha = '';
        this.confirmacaoSenha = '';
    }
}
