export class MenuConfig {
	public defaults: any = {
		header: {
			self: {},
			items: [
				// {
				// 	root: true,
				// 	title: 'Registros',
				// 	alignment: 'left',
				// 	toggle: 'click',
				// 	translate: 'MENU.RECORDS',
				// 	submenu: [
				// 		{
				// 			title: 'Alarmes',
				// 			alignment: 'left',
				// 			// icon: 'flaticon-settings',
				// 			page:'/parametros',
				// 			translate: 'MENU.ALARMS'
				// 		},
				// 		{
				// 			title: 'Eventos',
				// 			alignment: 'left',
				// 			// icon: 'flaticon-settings',
				// 			page:'/parametros',
				// 			translate: 'MENU.EVENTS'
				// 		},
				// 		{
				// 			title: 'Audit Trail',
				// 			alignment: 'left',
				// 			// icon: 'flaticon-settings',
				// 			page:'/parametros',
				// 			translate: 'MENU.AUDIT_TRAIL'
				// 		},

				// 	]
				// },
				// {
				// 	title: 'Análise Impacto',
				// 	root: true,
				// 	alignment: 'left',
				// 	toggle: 'click',
				// 	translate: 'MENU.ANALYSIS_IMPACT',
				// 	submenu: [
				// 		{
				// 			title: 'Overview',
				// 			alignment: 'left',
				// 			// icon: 'fa fa-user-circle',
				// 			page:'/colaboradores',
				// 			translate: 'MENU.OVERVIEW'
				// 		},
				// 		{
				// 			title: 'Realizar Análise',
				// 			alignment: 'left',
				// 			// icon: 'flaticon-settings',
				// 			page:'/dominios-geometalurgicos',
				// 			translate: 'MENU.PERFORM_ANALYSIS'
				// 		},
				// 		{
				// 			title: 'Realizar Análise Parcial',
				// 			alignment: 'left',
				// 			// icon: 'fas fa-briefcase',
				// 			page:'/empresas',
				// 			translate: 'MENU.PERFORM_PARTIAL_ANALYSIS'
				// 		}
				// 	]
				// },
				// {
				// 	title: 'Aprovação QA',
				// 	root: true,
				// 	alignment: 'left',
				// 	toggle: 'click',
				// 	translate: 'MENU.QA_APPROVAL',
				// 	submenu: [
				// 		{
				// 			title: 'Overview',
				// 			alignment: 'left',
				// 			// icon: 'fa fa-user-circle',
				// 			page:'/colaboradores',
				// 			translate: 'MENU.OVERVIEW'
				// 		},
				// 		{
				// 			title: 'Realizar Aprovação',
				// 			alignment: 'left',
				// 			// icon: 'flaticon-settings',
				// 			page:'/dominios-geometalurgicos',
				// 			translate: 'MENU.PERFORM_APROVAL'
				// 		},
				// 		{
				// 			title: 'Realizar Aprovação Parcial',
				// 			alignment: 'left',
				// 			// icon: 'fas fa-briefcase',
				// 			page:'/empresas',
				// 			translate: 'MENU.PERFORM_PARTIAL_APROVAL'
				// 		},
				// 		{
				// 			title: 'Revisar Aprovação',
				// 			alignment: 'left',
				// 			// icon: 'fas fa-briefcase',
				// 			page:'/empresas',
				// 			translate: 'MENU.REVIEW_APROVAL'
				// 		},
				// 		{
				// 			title: 'Relatório de Aprovações',
				// 			alignment: 'left',
				// 			// icon: 'fas fa-briefcase',
				// 			page:'/empresas',
				// 			translate: 'MENU.REPORT_APPROVALS'
				// 		}
				// 	]
				// },
				// {
				// 	title: 'Controle de Aprovação',
				// 	root: true,
				// 	alignment: 'left',
				// 	toggle: 'click',
				// 	translate: 'MENU.QA_APPROVAL',
				// 	submenu: [
				// 		{
				// 			title: 'Liberação Lote FP BMS',
				// 			alignment: 'left',
				// 			// icon: 'fa fa-user-circle',
				// 			page:'/colaboradores',
				// 			translate: 'MENU.OVERVIEW'
				// 		},
				// 		{
				// 			title: 'Liberação Lote FP FMS',
				// 			alignment: 'left',
				// 			// icon: 'flaticon-settings',
				// 			page:'/dominios-geometalurgicos',
				// 			translate: 'MENU.PERFORM_APROVAL'
				// 		},
				// 		{
				// 			title: 'Liberação Lote ALP',
				// 			alignment: 'left',
				// 			// icon: 'fas fa-briefcase',
				// 			page:'/empresas',
				// 			translate: 'MENU.PERFORM_PARTIAL_APROVAL'
				// 		},
				// 		{
				// 			title: 'Liberação Lote AP',
				// 			alignment: 'left',
				// 			// icon: 'fas fa-briefcase',
				// 			page:'/empresas',
				// 			translate: 'MENU.REVIEW_APROVAL'
				// 		}
				// 	]
				// },

			]
		},
		aside: {
			self: {},
			items: [

			]
		},
	};

	public get configs(): any {
		return this.defaults;
	}
}
