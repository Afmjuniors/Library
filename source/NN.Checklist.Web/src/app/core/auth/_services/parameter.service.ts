import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';

import { DomainParameter } from '../_models/_parameter/DomainParameter.models';
import { DBParameter } from '../_models/_parameter/DBParameter.model';
import { PolicyParameter } from '../_models/_parameter/PolicyParameter.models';
import { ResponseBase } from '../_models/ResponseBase.model';
import { MailParameter } from '../_models/_parameter/MailParameter.model';

const URL_BASE = environment.api;

@Injectable()
export class ParameterService {
    constructor(private http: HttpClient) { }

    GetDomainParameter(): Observable<DomainParameter> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.get<DomainParameter>(URL_BASE + '/Parameter/GetDomainParameter', { headers: header });
    }

    GetDBParameter(): Observable<DBParameter> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.get<DBParameter>(URL_BASE + '/Parameter/GetDbParameter', { headers: header });
    }

    GetPolicyParameter(): Observable<PolicyParameter> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.get<PolicyParameter>(URL_BASE + '/Parameter/GetPolicyParameter', { headers: header });
    }
    GetMailParameter(): Observable<MailParameter> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.get<MailParameter>(URL_BASE + '/Parameter/GetMailParameter', { headers: header });
    }

    SetMailParameter(data:MailParameter, comments: string): Observable<any> {
        var par = {
			data: data, comments: comments
		}
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<MailParameter>(URL_BASE + '/Parameter/InsertMailParameter', par, { headers: header });
    }
    SetDomainParameter(data:DomainParameter, comments: string): Observable<any> {
        var par = {
			data: data, comments: comments
		}
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<DomainParameter>(URL_BASE + '/Parameter/InsertDomainParameter', par, { headers: header });
    }

    SetDBParameter(data: DBParameter, comments: string): Observable<any> {
        var par = {
			data: data, comments: comments
		}
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<DBParameter>(URL_BASE + '/Parameter/InsertDbParameter', par, { headers: header });
    }

    SetPolicyParameter(data: PolicyParameter, comments: string): Observable<any> {
        var par = {
			data: data, comments: comments
		}
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<PolicyParameter>(URL_BASE + '/Parameter/InsertPolicyParameter', par, { headers: header });
    }
}
