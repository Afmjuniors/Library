import { BaseModel } from '../../_base/crud';

export class EquipamentosFilter extends BaseModel {
    idArea: number;
    idTipoEquipamento: number;
    idPlanoManutencao: number;
    clear(): void {
        this.idArea = 0;
        this.idTipoEquipamento = 0;
        this.idPlanoManutencao = 0;
	}
}