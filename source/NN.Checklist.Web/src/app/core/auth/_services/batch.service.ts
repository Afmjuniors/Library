import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';

import { Signature } from '../_models/signature.model';
import { BatchFilter } from '../_models/_batch/BatchFilter.model';
import { BatchCalendar } from '../_models/_batch/BatchCalendar.model';
import { OccurrenceArea } from '../_models/occurrenceArea.model';

const URL_BASE = environment.api;

@Injectable()
export class BatchService {
    constructor(private http: HttpClient) { }

    GetCalendarByFilters(data : BatchFilter): Observable<BatchCalendar[]> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<BatchCalendar[]>(URL_BASE + '/Batch/GetCalendarByFilters', data, { headers: header });
    }

    SearchByStatusData(filter : BatchFilter): Observable<OccurrenceArea[]> {
        let data = {
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<OccurrenceArea[]>(URL_BASE + '/Occurrence/SearchByStatusData', data, { headers: header });
    }
}
