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


export class EventsDataSource extends BaseDataSource {
	loadingSubject = new BehaviorSubject<boolean>(true);
	filter: OccurrenceRecordFilter = new OccurrenceRecordFilter();

	constructor(private appService: AppService,private store: Store<AppState>) {
		super();
		this.filter.idType = 2;
		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(this.defaultPageNumber, this.defaultPageSize, this.filter);
	}

	load(pageNumber: number, pageSize: number, filtro: OccurrenceRecordFilter): void
	{
		let filtro2 = null;
		if (filtro != null)
		{
			filtro2 = filtro;
		}
		this.appService.getAllEvents(pageNumber + 1, pageSize, filtro2).subscribe((response: QueryResultsModel) => {
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
