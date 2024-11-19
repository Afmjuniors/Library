import { BaseModel } from '../../_base/crud';
import { UserProfile } from './user-profile.model';
import { Permission } from './permission.model';
import { Menu } from './menu.model';
import { AdGroupUser } from './adGroupUser.model';

export class User extends BaseModel {
    userId: number;
    firstName: string;
    lastName: string;
    authenticationType: number;
    email: string;
    password: string;
    userAD: string;
	username: string;
    domainAdId: number;
    active: boolean;
    userProfile: UserProfile;
    userProfileId: number;
    token: string;
	cultureInfo: string;
    permissions: Permission[];
	menu: Menu;
	initials: string;
	phonesNumbers: string[];
	adGroupsUser: AdGroupUser[];

    clear(): void {
		this.userId = 0;
		this.firstName = "";
		this.lastName = "";
		this.authenticationType = 0;
		this.email = "";
		this.password = "";
		this.userAD = "";
		this.domainAdId = 0;
		this.active = false;
		this.userProfile = null;
		this.userProfileId = 0;
		this.token = "";
		this.cultureInfo = "";
		this.permissions = [];
		this.initials = "";
		this.phonesNumbers = [];
		this.adGroupsUser = [];
    }
}
