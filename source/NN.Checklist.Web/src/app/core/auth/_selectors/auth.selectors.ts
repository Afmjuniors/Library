// NGRX
import { createSelector } from '@ngrx/store';
// Lodash
import { each, find, some } from 'lodash';
// Selectors
import { selectAllRoles } from './role.selectors';
import { selectAllPermissions } from './permission.selectors';
// Models
import { Permission } from '../_models/permission.model';
import { UserProfile } from '../_models/user-profile.model';

export const selectAuthState = state => state.auth;

export const isLoggedIn = createSelector(
    selectAuthState,
    auth => auth.loggedIn
);

export const isLoggedOut = createSelector(
    isLoggedIn,
    loggedIn => !loggedIn
);


export const currentAuthToken = createSelector(
    selectAuthState,
    auth => auth.authToken
);

export const isUserLoaded = createSelector(
    selectAuthState,
    auth => auth.isUserLoaded
);

export const isMenuLoaded = createSelector(
    selectAuthState,
    auth => auth.isMenuLoaded
);

export const currentUser = createSelector(
    selectAuthState,
    auth => auth.user
);

export const currentMenu = createSelector(
    selectAuthState,
    auth => auth.menu
);

export const currentUserRoleIds = createSelector(
    currentUser,
    pf => {
        if (!pf) {
            return [];
        }

        return pf.roles;
    }
);

export const currentUserPermissionsTags = createSelector(
    currentUserRoleIds,
    selectAllRoles,
    (userRoleIds: UserProfile[], allRoles: UserProfile[]) => {
        const result = getPermissionsTagsFrom(userRoleIds, allRoles);
        return result;
    }
);

export const checkHasUserPermission = (tag: string) => createSelector(
    currentUserPermissionsTags,
    (tags: string[]) => {
        return tags.some(t => t === tag);
    }
);

export const currentUserPermissions = createSelector(
    currentUserPermissionsTags,
    selectAllPermissions,
    (permissionTags: string[], allPermissions: Permission[]) => {
        const result: Permission[] = [];
        each(permissionTags, tag => {
            const userPermission = find(allPermissions, elem => elem.tag === tag);
            if (userPermission) {
                result.push(userPermission);
            }
        });
        return result;
    }
);

function getPermissionsTagsFrom(userRolesIds: UserProfile[] = [], allRoles: UserProfile[] = []): string[] {
    const userRoles: UserProfile[] = [];
    each(userRolesIds, (_perfil: UserProfile) => {
       const userRole = find(allRoles, (_role: UserProfile) => _role.profileUserId === _perfil.profileUserId);
       if (userRole) {
           userRoles.push(_perfil);
       }
    });

    const result: string[] = [];
    each(userRoles, (_role: UserProfile) => {
        each(_role.permissions, t => {
            if (!some(result, _t => _t === t.tag)) {
                result.push(t.tag);
            }
        });
    });
    return result;
}