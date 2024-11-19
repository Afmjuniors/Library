export class PageConfig {
	public defaults: any = {
		dashboard: {
			page: {
				'title': 'Dashboard',
				'desc': 'Latest updates and statistic charts'
			},
		},
		cadastros: {
			'perfis-usuarios': {
				page: {title: 'Perfis de Usuários', desc: ''}
			},
			usuarios: {
				page: {title: 'Colaboradores', desc: ''}
			},
		},
		Consultas: {

		},
		builder: {
			page: {title: 'Layout Builder', desc: ''}
		},
		header: {
			actions: {
				page: {title: 'Actions', desc: 'Actions example page'}
			}
		},
		profile: {
			page: {title: 'User Profile', desc: ''}
		},
		error: {
			404: {
				page: {title: '404 Not Found', desc: '', subheader: false}
			},
			403: {
				page: {title: '403 Access Forbidden', desc: '', subheader: false}
			}
		},
	};

	public get configs(): any {
		return this.defaults;
	}
}
