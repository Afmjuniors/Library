import { Action } from '@ngrx/store';
import { User } from '../_models/user.model'
import { Menu } from '../_models/menu.model';

export enum AuthActionTypes {
    Login = '[Login] Action',
    Logout = '[Logout] Action',
    Register = '[Register] Action',
    UserRequested = '[Request User] Action',
    UserLoaded = '[Load User] Auth API',
    UserReload = '[Reload User] Auth API',
    MenuLoaded = '[Load Menu] Config API'
 }

export class Login implements Action {
    readonly type = AuthActionTypes.Login;
    constructor(public payload: { authToken: string, menu: Menu }) { }
}

export class Logout implements Action {
    readonly type = AuthActionTypes.Logout;
    constructor(public retornar: boolean) { }
}

export class Register implements Action {
    readonly type = AuthActionTypes.Register;
    constructor(public payload: { authToken: string }) { }
}


export class UserRequested implements Action {
    readonly type = AuthActionTypes.UserRequested;
}

export class UserLoaded implements Action {
    readonly type = AuthActionTypes.UserLoaded;
    constructor(public payload: { user: User }) { }
}

export class UserReload implements Action {
    readonly type = AuthActionTypes.UserReload;
}

export class MenuLoaded implements Action {
    readonly type = AuthActionTypes.MenuLoaded;
    constructor(public payload: { menu: Menu, isMenuLoaded: boolean }) { }
}

export type AuthActions = Login | Logout | Register | UserRequested | UserLoaded | UserReload | MenuLoaded;
