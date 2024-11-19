export class DominioGeometalurgico {
    idDominioGeometalurgico: number;
    nome: string;
    tipoMinerio:  number;
	cotaInicial: number;
    cotaFinal: number;
    recuperacao: number;
    recuperacaoGravimetrica: number;
    recuperacaoFlotacao: number;
    nomeTipoMinerio: string;

    clear(): void {
        this.idDominioGeometalurgico = 0;
        this.nome = '';
        this.tipoMinerio = 0;
		this.cotaInicial = 0;
        this.cotaFinal = 0;
		this.recuperacao = 0;
        this.recuperacaoGravimetrica = 0;
		this.recuperacaoFlotacao = 0;
        this.nomeTipoMinerio = '';

	}
}
