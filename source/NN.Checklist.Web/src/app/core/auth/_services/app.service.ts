import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { QueryResultsModel } from '../../_base/crud';
import { AdGroup } from '../_models/adGroup.model';
import { AdGroupUser } from '../_models/adGroupUser.model';
import { WatchdogAlarm } from '../_models/watchdogAlarm.model';

const URL_BASE = environment.api;

@Injectable()
export class AppService {
	constructor(private http: HttpClient) { }

	downloadFile(file: string): Observable<any> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		let options = { headers: header, reportProgress: true, responseType: 'blob', observe: 'response' };
		let http = new HttpRequest<any>('GET', URL_BASE + `/Application/Download?nome=${file}`, options);
		let obj = this.http.get(URL_BASE + `/Application/Download?nome=${file}`, {headers: header, responseType: 'blob'})
		return obj;
	}


	getAllAlarms(page, quantidade, filtros): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: quantidade,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/SearchOccurrencesRecords', data, { headers: header });
	}



	getAllEvents(page, quantidade, filtros): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: quantidade,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/SearchOccurrencesRecords', data, { headers: header });
	}

	searchAuditTrail(page, pageSize, filtros): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: pageSize,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AuditTrail/Search', data, { headers: header });
	}

	listStatus() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListStatus', { headers: header });
	}

	listCountries() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListCountries', { headers: header });
	}


	listUnits() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListUnits', { headers: header });
	}




	listSystemsFunctionalities() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/AuditTrail/ListSystemsFunctionalities', { headers: header });
	}



	sendMailNotification(data): Observable<QueryResultsModel> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Application/SendMessageNotifyArea', data, { headers: header });
	}

	listAdGroups(): Observable<AdGroup[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		var result = this.http.get<AdGroup[]>(URL_BASE + `/Application/ListAdGroups`, { headers: header });
		return result;
	}


	getAllUsers(page, quantidade, filtros): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: quantidade,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/SearchUsers', data, { headers: header });
	}

	listAdGroupsByUser(userId: number): Observable<AdGroupUser[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<AdGroupUser[]>(URL_BASE + `/AccessControl/ListAdGroupsByUser/${userId}`, { headers: header });
	}

	activateUser(data): Observable<any> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/ActivateUser', data, { headers: header });
	}

	getAllAdGroups(page, quantidade, filtros): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: quantidade,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/SearchAdGroups', data, { headers: header });
	}

	insertAdGroup(obj: AdGroup, comments: string): Observable<AdGroup> {
		let data = {
			data: obj,
			comments: comments
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/InsertAdGroup', data, { headers: header });
	}

	updateAdGroup(obj: AdGroup, comments: string): Observable<AdGroup> {
		let data = {
			data: obj,
			comments: comments
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/UpdateAdGroup', data, { headers: header });
	}

	removeAdGroup(obj: AdGroup, comments: string): Observable<boolean> {
		let data = {
			data: obj,
			comments: comments
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/RemoveAdGroup', data, { headers: header });
	}


	listSystemNodes() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListSystemNodes', { headers: header });
	}

	listEventsCategories() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListEventsCategories', { headers: header });
	}

	listStates() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListStates', { headers: header });
	}

	listTypesSeverities() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListTypesSeverities', { headers: header });
	}

	getAdGroup(adGroupId) {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + `/AccessControl/GetAdGroup/${adGroupId}`, { headers: header });
	}


	
	listStatusByMessage(messageId) {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + `/Application/ListStatusByMessage/${messageId}`, { headers: header });
	}

	
	checkWatchdogStatus(): Observable<WatchdogAlarm> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<WatchdogAlarm>(URL_BASE + `/Application/CheckStatusConnections`, { headers: header });
	}

}
