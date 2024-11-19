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
import { QAFilter } from '../_models/QAFilter.model';
import { QAService } from '../_services/qa.service';
export class QAWaitingApprovationDataSource extends BaseDataSource {
    loadingSubject = new BehaviorSubject<boolean>(true);

	constructor(private qaService: QAService, private store: Store<AppState>, public filter:QAFilter) {
		super();
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(this.defaultPageNumber, this.defaultPageSize, this.filter);
	}

	load(pageNumber: number, pageSize: number, filter: QAFilter): void
	{
		this.qaService.SearchQAData(pageNumber + 1, pageSize, filter).subscribe((response: QueryResultsModel) => {
			this.paginatorTotalSubject.next(response.rowsCount);
			this.entitySubject.next(response.entities);
			this.loadingSubject.next(false);
		});
	}
}
