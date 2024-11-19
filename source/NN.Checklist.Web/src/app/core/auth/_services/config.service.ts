import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { QueryResultsModel } from '../../_base/crud';
import { OccurrenceRecord } from '../_models/occurenceRecord.model';
import { ExportStatus } from '../_models/ExportStatus.model';
import { CreatedGroupRegister } from '../_models/_config/created-group-register.model';


const URL_BASE = environment.api;

@Injectable()
export class ConfigService {
    constructor(private http: HttpClient) { }

    GetAllOccurrencesGroups(page, quantity, filter): Observable<QueryResultsModel> {
        var data = {
            pageNumber: page,
            pageSize: quantity,
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<any>(URL_BASE + '/config/GetAllOccurrencesGroups', data, { headers: header });
	}

    GetAllRegisters(page, quantity, filter): Observable<QueryResultsModel> {
        var data = {
            pageNumber: page,
            pageSize: quantity,
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<any>(URL_BASE + '/config/GetAllRegisters', data, { headers: header });
	}

    InsertRegisterGroup(data):Observable<CreatedGroupRegister> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<CreatedGroupRegister>(URL_BASE + '/config/InsertRegisterGroup', data, { headers: header });
    }
    DeleteRegisterGroup(data):Observable<any> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<any>(URL_BASE + '/config/DeleteRegisterGroup', data, { headers: header });
    }
    GetRegistersByGroupId(page, quantity, filter):Observable<any>
    {
        var data = {
            pageNumber: page,
            pageSize: quantity,
            filter: filter
        }
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<any>(URL_BASE + '/config/GetRegistersByGroupId', data, { headers: header });
    }

	getGroupById(id: number): Observable<any> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        var result = this.http.get<any>(URL_BASE + `/config/GetOccurrenceGroup/${id}`, { headers: header });
        return result;
	}

    DeleteRegisterFromGroup(data):Observable<any> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<any>(URL_BASE + '/config/DeleteRegisterFromGroup', data, { headers: header });
    }

	SendMessagesByTypeOccurrence(id: number) {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + `/config/SendMessagesByTypeOccurrence/${id}`, { headers: header });
	}
}
