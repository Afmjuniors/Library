import { BaseModel } from '../../_base/crud';
import { Moment } from 'moment';

export class MinaFiltro extends BaseModel {
	idMina: number;
	nome: string;

    clear(): void {
		this.idMina = 0;
		this.nome = "";
	}
}
