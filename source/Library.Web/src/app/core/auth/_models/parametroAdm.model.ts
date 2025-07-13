
export class ParametroAdm {
	// idTag: number;
	senha: string;
	senhaAnterior: string;
	confirmacaoSenha: string;

	clear(): void {
		this.senha = '';
		this.senhaAnterior = '';
		this.confirmacaoSenha = '';
	}
}
