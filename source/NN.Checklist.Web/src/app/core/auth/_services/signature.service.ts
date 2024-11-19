import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';

import { Signature } from '../_models/signature.model';

const URL_BASE = environment.api;

@Injectable()
export class SignatureService {
    constructor(private http: HttpClient) { }

    GetSignatureObject(signature: string): Observable<Signature> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.get<Signature>(URL_BASE + '/AccessControl/ReadSignature?signature=' + signature, { headers: header });
    }

    ValidateSignature(data : Signature): Observable<Signature> {
        const userToken = localStorage.getItem(environment.authTokenKey);
        let header = new HttpHeaders({ 'Authorization': 'Bearer ' + userToken });
        header.append('Content-Type', 'application/json');
        return this.http.post<Signature>(URL_BASE + '/AccessControl/SignatureValidation', data, { headers: header });
    }
}
