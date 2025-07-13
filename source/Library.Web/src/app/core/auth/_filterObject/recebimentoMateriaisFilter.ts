import { BaseModel } from '../../_base/crud';

export class RecebimentoMateriaisFilter extends BaseModel {
    idTipoTransporte: number = null;
    idTipoMaterial: number = null;
    inicio_1: Date = null;
    fim_1: Date = null;
    inicio_2: Date = null;
    fim_2: Date = null;

    clear(): void {
        this.inicio_1 = null;
        this.inicio_2 = null;
        this.fim_1 = null;
        this.fim_2 = null;
        this.idTipoTransporte = 0;
        this.idTipoMaterial = 0;

	}
}