import { BaseModel } from '../../_base/crud';

export class RotasFilter extends BaseModel {
    idCorreiaOrigem: number;
    idCorreiaDestino: number;

    clear(): void {
        this.idCorreiaOrigem = null;
        this.idCorreiaDestino = null;
	}
}