// NGRX
import { Action } from '@ngrx/store';
import { Update } from '@ngrx/entity';
// CRUD
import { QueryParamsModel } from '../../_base/crud';
// Models
import { DominioAD } from '../_models/dominioAD.model';

export enum DomainActionTypes {
    AllDomainsLoaded = '[Domains API] All Domains Loaded',
}

export class AllDomainsLoaded implements Action {
    readonly type = DomainActionTypes.AllDomainsLoaded;
    constructor(public payload: { domains: DominioAD[] }) { }
}

export type DomainActions = AllDomainsLoaded;
