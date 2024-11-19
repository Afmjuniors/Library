import { UserProfile } from './../_models/user-profile.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, forkJoin } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { AppState } from '../../../core/reducers';
import { User } from '../_models/user.model';
import { Permission } from '../_models/permission.model';
import { catchError, map, mergeMap, tap, takeUntil, withLatestFrom, filter, startWith, switchMap } from 'rxjs/operators';
import { QueryParamsModel, QueryResultsModel } from '../../_base/crud';
import { environment } from '../../../../environments/environment';
import { Router } from '@angular/router';
import { DominioAD } from '../_models/dominioAD.model';
import { currentUser, isUserLoaded, isLoggedIn } from '../_selectors/auth.selectors';
import { Menu } from '../_models/menu.model';
import { Language } from '../_models/language.model';

const URL_API = environment.api;

@Injectable()
export class AuthService {
    constructor(private http: HttpClient, private store: Store<AppState>,
        private router: Router) { }

    loginAD(email: string, password: string, domain: number): Observable<User> {
        let header = new HttpHeaders();
        header.append('Content-Type', 'application/json');
        let user = new User();
        user.username = email;
        user.password = password;
        user.domainAdId = domain;
        const obj = this.http.post<User>(URL_API + "/AccessControl/Authenticate", user);
        return obj;
    }

	getUserByToken(): Observable<User> {
		const userToken = localStorage.getItem(environment.authTokenKey);
		let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
		const obj = this.http.get<User>(URL_API + '/AccessControl/GetUserByToken', { headers: header });
		return obj;
	}

	getMenu(): Observable<Menu> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        return this.http.get<Menu>(URL_API + '/AccessControl/GetMenu', { headers: header });
    }

	updateLanguage(language: Language): Observable<any> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.put(URL_API + '/AccessControl/UpdateLanguage', language, { headers: header });
    }

    getAllPermissionsPaged(queryParams: QueryParamsModel): Observable<QueryResultsModel> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        return this.http.get<QueryResultsModel>(URL_API + `/ControleAcesso/BuscarPermissoes/${queryParams.pageNumber}/${queryParams.pageSize}`, { headers: header });
    }

    getAllPermissions(): Observable<Permission[]> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        return this.http.get<Permission[]>(URL_API + '/AccessControl/ListPermissions', { headers: header });
    }

    verificarPermissao(tag: string): Observable<boolean> {

        const user$ = this.store.pipe(
            select(isUserLoaded),
            filter(([action, _isUserLoaded]) => !_isUserLoaded),
            mergeMap(([action, _isUserLoaded]) => this.getUserByToken()),
            tap(_user => {
                if (_user) {
                    return _user;
                } else {
                    return null;
                }
            },  error =>
            {
                return null;
            })

        );

        const verificacao$ =
            this.store.pipe(select(currentUser),
                map(user => {
                    if (user != null) {
                        if (user.userAD.toString().toLowerCase() == 'admin')
                        {
                            return true;
                        }
                        else if (user.userId > 0)
                        {
                            if (user.permissions != null && user.permissions.length > 0) {
                                let ret = user.permissions.findIndex(x => x.tag == tag);
                                if (ret == -1)
                                {
                                    return false;
                                }
                                return true;
                            }
                        }
                    }
                    else {
                        this.router.navigateByUrl('/inicio');
                    }
            },  error =>
            {
                this.router.navigateByUrl('/inicio');
            })
       );
       return verificacao$;

    }

    /*
 	 * Handle Http operation that failed.
 	 * Let the app continue.
     *
	 * @param operation - name of the operation that failed
 	 * @param result - optional value to return as the observable result
 	 */
    private handleError<T>(operation = 'operation', result?: any) {
        return (error: any): Observable<any> => {
            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // Let the app keep running by returning an empty result.
            return of(result);
        };
    }


}
