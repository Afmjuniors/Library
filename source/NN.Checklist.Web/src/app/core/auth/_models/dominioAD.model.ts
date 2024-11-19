import { BaseModel } from '../../_base/crud';

export class DominioAD extends BaseModel {
    IdDominioAD: number;
    Nome: string;
    Endereco: string;

    clear(): void {
        this.IdDominioAD = 0;
        this.Nome = '';
        this.Endereco = '';
    }
}
