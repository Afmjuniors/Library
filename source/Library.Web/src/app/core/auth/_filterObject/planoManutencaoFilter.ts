import { BaseModel } from '../../_base/crud';

export class PlanoManutencaoFilter {
    inicio_1: any;
    fim_1: any;
    inicio_2: any;
    fim_2: any;
    idArea: number;
    idTipoEquipamento: number;
    idPlanoManutencao: number;
    idTipoManutencao: number;

    clear(): void {
        // this.inicio_1 = new Date();
        // this.inicio_2 = new Date();
        // this.fim_1 = new Date();
        // this.fim_2 = new Date();
        this.idPlanoManutencao = 0;
        this.idTipoEquipamento = 0;
        this.idTipoManutencao = 0;
        this.idArea = 0;
    }
}
