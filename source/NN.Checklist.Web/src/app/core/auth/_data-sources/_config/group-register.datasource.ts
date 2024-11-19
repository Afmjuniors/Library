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
import { GroupRegisterFilter } from '../../_models/_config/group-register-filter.model';
import { ConfigService } from '../../_services/config.service';
export class GroupRegisterDataSource extends BaseDataSource {
    loadingSubject = new BehaviorSubject<boolean>(true);

	constructor(private configService: ConfigService, private store: Store<AppState>, public filter: GroupRegisterFilter) {
		super();
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(this.defaultPageNumber, this.defaultPageSize, this.filter);
	}

	load(pageNumber: number, pageSize: number, filter: GroupRegisterFilter): void
	{
		this.configService.GetAllOccurrencesGroups(pageNumber + 1, pageSize, filter).subscribe((response: QueryResultsModel) => {
			this.paginatorTotalSubject.next(response.rowsCount);
			this.entitySubject.next(response.entities);
			this.loadingSubject.next(false);
		});
	}
}
