// RxJS
import { of, Observable, BehaviorSubject } from 'rxjs';
import { catchError, finalize, tap, debounceTime, delay, distinctUntilChanged } from 'rxjs/operators';
// NGRX
import { Store, select } from '@ngrx/store';
// CRUD
import { BaseDataSource, QueryResultsModel, QueryParamsModel } from '../../_base/crud';
// State
import { AppState } from '../../reducers';
// Selectirs
import { AppService } from '../_services/app.service';
import { AuditTrailFilter } from '../_models/auditTrailFilter.model';


export class AuditTrailDataSource extends BaseDataSource {
	loadingSubject = new BehaviorSubject<boolean>(true);
	filter: AuditTrailFilter = new AuditTrailFilter();

	constructor(private appService: AppService,private store: Store<AppState>) {
		super();
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(this.defaultPageNumber, this.defaultPageSize, null);
	}

	load(pageNumber: number, pageSize: number, filtro: AuditTrailFilter): void
	{
		let filtro2 = null;
		if (filtro != null)
		{
			filtro2 = filtro;
		}

		this.appService.searchAuditTrail(pageNumber + 1, pageSize, filtro2).subscribe((response: QueryResultsModel) => {
			this.paginatorTotalSubject.next(response.rowsCount);
			this.entitySubject.next(response.entities);
			this.loadingSubject.next(false);
		}, error => {
			this.paginatorTotalSubject.next(0);
			this.entitySubject.next(null);
			this.loadingSubject.next(false);
		} );
	}
}
