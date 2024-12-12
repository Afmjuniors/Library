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
import { MessageFilter } from '../_models/messageFilter.model';


export class MessagesDataSource extends BaseDataSource {
	loadingSubject = new BehaviorSubject<boolean>(true);
	filter: MessageFilter = new MessageFilter();

	constructor(private appService: AppService,private store: Store<AppState>, private filter2: MessageFilter) {
		super();

		this.loading$ = this.loadingSubject.asObservable();
		this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
		this.load(this.defaultPageNumber, this.defaultPageSize, this.filter2);
	}

	load(pageNumber: number, pageSize: number, filter: MessageFilter): void
	{
		let filter2 = null;
		if (filter != null)
		{
			filter2 = filter;
		}

		
	}
}
