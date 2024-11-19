import { TypeOccurrenceRecordNotificationChannel } from './../_models/typeOccurrenceRecordNotificationChannel.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { OccurrenceRecord } from '../_models/occurenceRecord.model';
import { QueryResultsModel } from '../../_base/crud';
import { OccurrenceAnalysisDetails } from '../_models/occurrenceAnalysisDetails.model';
import { Area } from '../_models/area.model';
import { List } from 'lodash';
import { OccurrenceRecordFlux } from '../_models/occurrenceRecordFlux.model';
import { OccurrenceAnalysisDetailsItem } from '../_models/occurrenceAnalysisDetailsItem.model';
import { AnalysisDetails } from '../_models/analysisDetails.model';
import { AdGroup } from '../_models/adGroup.model';
import { TypeOccurenceRecord } from '../_models/typeOccurrenceRecord.model';
import { ImpactedArea } from '../_models/impactedArea.model';
import { UserAvailabilities } from '../_models/userAvailabilities.model';
import { UserUnavailability } from '../_models/userUnavailability.model';
import { UserPhone } from '../_models/userPhone.model';
import { AdGroupUser } from '../_models/adGroupUser.model';
import { AdGroupUserArea } from '../_models/adGroupUserArea.model';
import { AreaPhone } from '../_models/areaPhone.model';
import { OccurrenceRecordFilter } from '../_models/occurrenceRecordFilter.model';
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

	getAllOccurrencesAnalysis(page, quantidade, filtros: OccurrenceRecordFilter): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: quantidade,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/SearchImpactAnalysis', data, { headers: header });
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

	searchMessagesByOccurrence(page, quantidade, filtros): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: quantidade,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/SearchMessagesByOccurrence', data, { headers: header });
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

	listProcesses() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListProcesses', { headers: header });
	}

	listUnits() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListUnits', { headers: header });
	}

	listAreas() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Application/ListAreas', { headers: header });
	}

	listAreasByProcess(processId: number): Observable<Area[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<Area[]>(URL_BASE + `/Application/ListAreasByProcess/${processId}`, { headers: header });
	}

	listSystemsFunctionalities() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/AuditTrail/ListSystemsFunctionalities', { headers: header });
	}

	exportTypesOccurrencesRecordsPdf(filtros): Observable<string> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post(URL_BASE + '/Config/ExportTypesOccurrencesPdf', filtros, { headers: header, responseType: 'text' });
	}

	exportTypesOccurrencesRecordsCSV(filtros): Observable<string> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post(URL_BASE + '/Config/ExportTypesOccurrencesCSV', filtros, { headers: header, responseType: 'text' });
	}

	exportOccurrencesPdf(filtros): Observable<string> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post(URL_BASE + '/Occurrence/ExportOccurrencePDF', filtros, { headers: header, responseType: 'text' });
	}

	ExportOccurrenceCSV(filtros): Observable<string> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post(URL_BASE + '/Occurrence/ExportOccurrenceCSV', filtros, { headers: header, responseType: 'text' });
	}

	exportOccurrenceAnalysisPdf(filtros: OccurrenceRecordFilter): Observable<string> {
		var data = {
			pageNumber: 0,
			pageSize: 0,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/ExportOccurrenceAnalysisPDF', data, { headers: header });
	}

	exportOccurrenceAnalysisCSV(filtros: OccurrenceRecordFilter): Observable<string> {
		var data = {
			pageNumber: 0,
			pageSize: 0,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/ExportOccurrenceAnalysisCSV', data, { headers: header });
	}

	listOccurrenceAnalysisDetails(data: AnalysisDetails): Observable<OccurrenceAnalysisDetails[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		var result = this.http.post<OccurrenceAnalysisDetails[]>(URL_BASE + '/Occurrence/ListOccurrenceAnalysisDetails', data, { headers: header });
		return result;
	}

	getOccurrenceAnalysisDetails(data: OccurrenceAnalysisDetailsItem): Observable<OccurrenceAnalysisDetails> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		var result = this.http.post<OccurrenceAnalysisDetails>(URL_BASE + '/Occurrence/GetOccurrenceAnalysisDetails', data, { headers: header });
		return result;
	}

	listOccurrenceRecordsFlux(occurrenceRecordId: number, impactedAreaId: number): Observable<OccurrenceRecordFlux[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		var result = this.http.get<OccurrenceRecordFlux[]>(URL_BASE + `/Occurrence/ListOccurrenceRecordFlux/${occurrenceRecordId}/${impactedAreaId}`, { headers: header });
		return result;
	}

	performAnalysis(occurrenceAnalysis): Observable<QueryResultsModel> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/PerformAnalysis', occurrenceAnalysis, { headers: header });
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

	configureRegister(type: TypeOccurenceRecord[], comments: string): Observable<any> {
		var data = {
			types: type, comments: comments
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/ConfigureTypeOccurrenceRecord', data, { headers: header });
	}

	createRegister(type: TypeOccurenceRecord): Observable<any> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/InsertTypeOccurrenceRecord', type, { headers: header });
	}

	getListTypeOccurrenceRecord(listIds: number[]): Observable<any> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Occurrence/GetListTypeOccurrenceRecord', listIds, { headers: header });
	}

	ListImpactedAreasByTypeOccurrenceRecord(typeOccurrenceRecordId: number): Observable<ImpactedArea[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<ImpactedArea[]>(URL_BASE + `/Occurrence/ListImpactedAreasByTypeOccurrenceRecord/${typeOccurrenceRecordId}`, { headers: header });
	}

	listTypeNotificationByTypeOccurrenceRecord(typeOccurrenceRecordId: number): Observable<TypeOccurrenceRecordNotificationChannel[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<TypeOccurrenceRecordNotificationChannel[]>(URL_BASE + `/Occurrence/ListTypeNotificationByTypeOccurrenceRecord/${typeOccurrenceRecordId}`, { headers: header });
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

	listAvailabilitiesByUser(userId: number): Observable<UserAvailabilities[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<UserAvailabilities[]>(URL_BASE + `/AccessControl/ListAvailabilitiesByUser/${userId}`, { headers: header });
	}

	insertUserAvalability(data): Observable<UserAvailabilities> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/InsertUserAvailability', data, { headers: header });
	}

	removeUserAvalability(data): Observable<boolean> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/RemoveUserAvailability', data, { headers: header });
	}

	listUnavailabilitiesByUser(userId: number): Observable<UserUnavailability[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<UserUnavailability[]>(URL_BASE + `/AccessControl/ListUnavailabilitiesByUser/${userId}`, { headers: header });
	}

	insertUserUnavalability(data): Observable<UserUnavailability> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/InsertUserUnavailability', data, { headers: header });
	}

	removeUserUnavailability(data): Observable<boolean> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/RemoveUserUnavailability', data, { headers: header });
	}

	listPhonesNumbersByUser(userId: number): Observable<UserPhone[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<UserPhone[]>(URL_BASE + `/AccessControl/ListPhonesNumbersByUser/${userId}`, { headers: header });
	}

	insertUserPhone(data): Observable<UserPhone> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/InsertUserPhone', data, { headers: header });
	}

	removeUserPhone(data): Observable<boolean> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/RemoveUserPhone', data, { headers: header });
	}

	listAdGroupsByUser(userId: number): Observable<AdGroupUser[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<AdGroupUser[]>(URL_BASE + `/AccessControl/ListAdGroupsByUser/${userId}`, { headers: header });
	}

	insertAdGroupUserArea(data): Observable<AdGroupUserArea> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/InsertAdGroupUserArea', data, { headers: header });
	}

	removeAdGroupUserArea(data): Observable<AdGroupUserArea> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/RemoveAdGroupUserArea', data, { headers: header });
	}

	listAreasByAdGroupUser(userId: number): Observable<Area[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<Area[]>(URL_BASE + `/AccessControl/listAreasByAdGroupUser/${userId}`, { headers: header });
	}

	activateUser(data): Observable<any> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/AccessControl/ActivateUser', data, { headers: header });
	}

	getAllAreas(page, quantidade, filtros): Observable<QueryResultsModel> {
		var data = {
			pageNumber: page,
			pageSize: quantidade,
			filter: filtros
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Application/SearchAreas', data, { headers: header });
	}

	listPhonesNumbersByArea(areaId: number): Observable<AreaPhone[]> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<AreaPhone[]>(URL_BASE + `/Application/ListPhonesByArea/${areaId}`, { headers: header });
	}

	insertAreaPhone(data): Observable<AreaPhone> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Application/InsertAreaPhone', data, { headers: header });
	}

	removeAreaPhone(data): Observable<boolean> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Application/RemoveAreaPhone', data, { headers: header });
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

	listTypes() {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + '/Occurrence/ListTypesOccurrencesRecords', { headers: header });
	}

	listTypesOccurrencesRecordsByTypeArea(type, areaId) {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.get<any>(URL_BASE + `/Occurrence/ListTypesOccurrencesRecordsByArea/${type}/${areaId}`, { headers: header });
	}

	insertOccurrenceRecord(ocr: OccurrenceRecord, comments: string): Observable<OccurrenceRecord> {
		var data = {
			occurrenceRecord: ocr, comments: comments
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<OccurrenceRecord>(URL_BASE + '/Occurrence/InsertOccurrenceRecord', data, { headers: header });
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

	insertArea(obj: Area, comments: string): Observable<Area> {
		let data = {
			data: obj,
			comments: comments
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Application/InsertArea', data, { headers: header });
	}

	updateArea(obj: Area, comments: string): Observable<Area> {
		let data = {
			data: obj,
			comments: comments
		}
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		return this.http.post<any>(URL_BASE + '/Application/UpdateArea', data, { headers: header });
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
