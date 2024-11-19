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
import { OccurrenceRecordFilter } from '../_models/occurrenceRecordFilter.model';


export class OccurrencesAnalysisDataSource extends BaseDataSource {
	loadingSubject = new BehaviorSubject<boolean>(true);

	constructor(private appService: AppService,private store: Store<AppState>, filter: OccurrenceRecordFilter) {
		super();
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.loadAnalysis(this.defaultPageNumber, this.defaultPageSize, filter);
	}

	loadAnalysis(pageNumber: number, pageSize: number, filter: OccurrenceRecordFilter): void
	{
		let filter2: OccurrenceRecordFilter = new OccurrenceRecordFilter();
		if (filter != null)
		{
			filter2 = filter;
		}
		this.loading$ = this.loadingSubject.asObservable();

		this.appService.getAllOccurrencesAnalysis(pageNumber + 1, pageSize, filter2).subscribe((response: QueryResultsModel) => {
			this.paginatorTotalSubject.next(response.rowsCount);
			this.entitySubject.next(response.entities);
			this.loadingSubject.next(false);
		}, error =>{
			this.paginatorTotalSubject.next(0);
			this.entitySubject.next(null);
			this.loadingSubject.next(false);
		});
	}
}
