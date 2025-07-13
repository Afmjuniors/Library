import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'kt-controle-acesso',
	templateUrl: './controle-acesso.component.html',
	styleUrls: ['./controle-acesso.component.scss']
})
export class ControleAcessoComponent implements OnInit {
	tipo_login = 1;
	exibeOpcaoLogin: boolean;
	sub: any;

	constructor(
		public activated_router: ActivatedRoute,
		private ref: ChangeDetectorRef
	) { }

	ngOnInit() {
		this.sub = this.activated_router.params.subscribe(params=> {
			this.exibeOpcaoLogin = params['exibeOpcaoLogin'] == "true" ? true : false;
			this.ref.detectChanges();
		});
	}


}
