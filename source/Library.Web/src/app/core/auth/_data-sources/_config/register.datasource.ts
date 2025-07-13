// RxJS
import { of, Observable, BehaviorSubject } from 'rxjs';
import { catchError, finalize, tap, debounceTime, delay, distinctUntilChanged } from 'rxjs/operators';
// NGRX
import { Store, select } from '@ngrx/store';
// CRUD
import { BaseDataSource, QueryResultsModel, QueryParamsModel } from '../../../_base/crud';
// State
import { AppState } from '../../../reducers';
// Selectirs
import { RegisterFilter } from '../../_models/_config/register-filter.model';
import { ConfigService } from '../../_services/config.service';

export class RegisterDataSource extends BaseDataSource {
    loadingSubject = new BehaviorSubject<boolean>(true);

	constructor(private configService: ConfigService, private store: Store<AppState>, public filter: RegisterFilter) {
		super();
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(this.defaultPageNumber, this.defaultPageSize, this.filter);
	}

	load(pageNumber: number, pageSize: number, filter: RegisterFilter): void
	{
		this.configService.GetAllRegisters(pageNumber + 1, pageSize, filter).subscribe((response: QueryResultsModel) => {
			this.paginatorTotalSubject.next(response.rowsCount);
			this.entitySubject.next(response.entities);
			this.loadingSubject.next(false);
		}, error => {
			this.paginatorTotalSubject.next(0);
			this.entitySubject.next(null);
			this.loadingSubject.next(false);
		});
	}
}
