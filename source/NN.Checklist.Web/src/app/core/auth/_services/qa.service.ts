import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { QueryResultsModel } from '../../_base/crud';
import { OccurrenceRecord } from '../_models/occurenceRecord.model';
import { ExportStatus } from '../_models/ExportStatus.model';
import { QAFilter } from '../_models/QAFilter.model';


const URL_BASE = environment.api;

@Injectable()
export class QAService {
    constructor(private http: HttpClient) { }

    SearchQAData(page, quantity, filter): Observable<QueryResultsModel> {
        var data = {
            pageNumber: page,
            pageSize: quantity,
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<any>(URL_BASE + '/QA/SearchQARecords', data, { headers: header });
	}
    GetQADetailPage(page, quantity, filter): Observable<QueryResultsModel> {
        var data = {
            pageNumber: page,
            pageSize: quantity,
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<any>(URL_BASE + '/QA/SearchQADetailRecords', data, { headers: header });
	}
    GetQADetailData(filter): Observable<OccurrenceRecord> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.get<OccurrenceRecord>(URL_BASE + '/QA/SearchQADetailRecords?id=' + filter, { headers: header });
	}

    GetQAApprovalData(data): Observable<OccurrenceRecord[]> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<OccurrenceRecord[]>(URL_BASE + '/QA/SearchQADetailRecordsByIDs', data, { headers: header });
	}
    
    assessOccurrences(data):Observable<any> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<OccurrenceRecord[]>(URL_BASE + '/QA/AssessOccurrences', data, { headers: header });
    }

    GenerateApprovalPDF(filter: QAFilter): Observable<ExportStatus> {
        var data = {
            pageNumber: 0,
            pageSize: 0,
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<ExportStatus>(URL_BASE + '/QA/ExportQAApprovalPDF', data, { headers: header });
	}
    GenerateApprovalXLS(filter: QAFilter): Observable<ExportStatus> {
        var data = {
            pageNumber: 0,
            pageSize: 0,
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<ExportStatus>(URL_BASE + '/QA/ExportQAApprovalCSV', data, { headers: header });
	}
}
