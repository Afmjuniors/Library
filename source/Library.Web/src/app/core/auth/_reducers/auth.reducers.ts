// Actions
import { AuthActions, AuthActionTypes } from '../_actions/auth.actions';
import { Menu } from '../_models/menu.model';
// Models
import { User } from '../_models/user.model';

export interface AuthState {
    loggedIn: boolean;
    authToken: string;
    user: User;
    menu: Menu;
    isUserLoaded: boolean;
    isMenuLoaded: boolean;
}

export const initialAuthState: AuthState = {
    loggedIn: false,
    authToken: undefined,
    user: undefined,
    menu: undefined,
    isUserLoaded: false,
    isMenuLoaded: false,
};

export function authReducer(state = initialAuthState, action: AuthActions): AuthState {
    switch (action.type) {
        case AuthActionTypes.Login: {
            const _token: string = action.payload.authToken;
            return {
                ...state,
                loggedIn: true,
                authToken: _token,
                user: undefined,
                menu: undefined,
                isUserLoaded: false,
                isMenuLoaded: false
            };
        }

        case AuthActionTypes.Register: {
            const _token: string = action.payload.authToken;
            return {
                ...state,
                loggedIn: true,
                authToken: _token,
                user: undefined,
                isUserLoaded: false
            };
        }

        case AuthActionTypes.Logout:
            return initialAuthState;

        case AuthActionTypes.UserLoaded: {
            const _user: User = action.payload.user;
            return {
                ...state,
                user: _user,
                menu: _user.menu, 
                isUserLoaded: true,
                isMenuLoaded: _user.menu != null ? true : false
            };
        }

        case AuthActionTypes.UserReload: {            
            return {
                ...state,
                user: undefined,
                isUserLoaded: false
            };
        }

        case AuthActionTypes.MenuLoaded: {
            const _menu: Menu = action.payload.menu;
            return {
                ...state,
                menu: _menu,
                isMenuLoaded: _menu != null && _menu.items != null && _menu.items.length > 0 ? true : false
            };
        }

        default:
            return state;
    }
}
