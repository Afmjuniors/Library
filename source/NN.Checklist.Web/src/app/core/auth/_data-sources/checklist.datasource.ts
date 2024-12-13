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
import { Checklist } from '../_models/checklist.model';


export class ChecklistDataSource extends BaseDataSource {
    loadingSubject = new BehaviorSubject<boolean>(true);
    filter: Checklist = new Checklist();

    constructor(private appService: AppService,private store: Store<AppState>) {
        super();
        this.loading$ = this.loadingSubject.asObservable();
        this.isPreloadTextViewed$ = this.loadingSubject.asObservable();
        this.load(this.defaultPageNumber, this.defaultPageSize, this.filter);
    }
    

    load(pageNumber: number, pageSize: number, filtro: Checklist): void
    {
        let filtro2 = null;
        if (filtro != null)
        {
            filtro2 = filtro;
        }

        this.appService.getAllUsers(pageNumber + 1, pageSize, filtro2).subscribe((response: QueryResultsModel) => {
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
