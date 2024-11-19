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
import { BatchService } from '../_services/batch.service';
import { BatchFilter } from '../_models/_batch/BatchFilter.model';
import { BatchCalendar } from '../_models/_batch/BatchCalendar.model';
export class BatchReleaseDataSource extends BaseDataSource {
    loadingSubject = new BehaviorSubject<boolean>(true);

	constructor(private batchService: BatchService, private store: Store<AppState>, public filter:BatchFilter) {
		super();
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(this.filter);
	}

	load(filter: BatchFilter): void
	{
		this.batchService.GetCalendarByFilters(filter).subscribe((response: BatchCalendar[]) => {
			this.entitySubject.next(response);
			this.loadingSubject.next(false);
		});
	}
}
