import { BaseModel } from '../../_base/crud';
import { each } from 'lodash';
import { Permission } from './permission.model';

export class UserProfile extends BaseModel {
    profileUserId: number;
    description: string;
    permissions: Permission[];

    public clear(): void {
        this.profileUserId = undefined;
        this.description = '';
        this.permissions = [];
    }
}
