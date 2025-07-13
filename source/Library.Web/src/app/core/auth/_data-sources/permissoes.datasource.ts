// RxJS
import { of, Observable, BehaviorSubject } from 'rxjs';
import { catchError, finalize, tap, debounceTime, delay, distinctUntilChanged } from 'rxjs/operators';
// CRUD
import { BaseDataSource, QueryResultsModel, QueryParamsModel } from '../../_base/crud';
// State
import { AppState } from '../../reducers';
// Selectirs
import { AuthService } from '../_services/auth.service';

export class PermissoesDataSource extends BaseDataSource {

	loadingSubject = new BehaviorSubject<boolean>(true);

	constructor(private authService: AuthService, pageNumber: number, pageSize: number) {
		super();
	
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(pageNumber, pageSize);
	}

	load(pageNumber: number, pageSize: number): void
	{
		this.authService.getAllPermissionsPaged(new QueryParamsModel(null, null, null, pageNumber+1, pageSize)).subscribe((response: QueryResultsModel) => {
			this.paginatorTotalSubject.next(response.rowsCount);
			this.entitySubject.next(response.entities); 
			this.loadingSubject.next(false);
		});
	}
}
