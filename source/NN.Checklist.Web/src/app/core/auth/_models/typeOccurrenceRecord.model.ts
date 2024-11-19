import { ReleaseArea } from './releaseArea.model';
import { BaseModel } from '../../_base/crud';
import { TypeOccurrence }  from './typeOccurrence.model';
import { TypeOccurrenceRecordGroup } from './typeOccurrenceRecordGroup.model';
import { Unit } from './unit.model';
import { Datatype } from './datatype.model';
import { Area } from './area.model';
import { AdGroup } from './adGroup.model';
import { ImpactedArea } from './impactedArea.model';

export class TypeOccurenceRecord extends BaseModel {

	typeOccurrenceRecordId: number = 0;
	areaId: number = null;
	datatypeId: number = null;
	deactivateDate: Date;
	deactivated: boolean;
	description: string;
	tag: string;
	typeOccurrenceId: number = null;
	typeOccurrenceRecordGroupId: number = null;
	unitId: number = null;
	area: Area;
	datatype: Datatype;
	typeOccurrenceRecordGroup: TypeOccurrenceRecordGroup;
	unit: Unit;
	adGroups: AdGroup[];
	notify: boolean;
	notificationFrequency: number = null;
	compensationValue: string;
	assessmentNeeded: boolean;
	listTypeNotificationChannels: number[];
	selected: boolean;
	areas: Area[];
	areas2: Area[];
	areas3: Area[];
	units: Unit[];
	impactedAreas: ImpactedArea[];
	releaseAreas: ReleaseArea[];
	releaseAreas2: Area[];
	releaseAreas3: Area[];
	listAreasRelease: Area[];
	processRelease: Number;
	getExtremeValue: boolean = false;
	tagDevice: string;
	sending: boolean = false;
	sended: boolean = false;

    clear(): void {
		this.typeOccurrenceRecordId = 0
		this.areaId = null
		this.datatypeId = null
		this.deactivateDate = null;
		this.deactivated = false;
		this.description = "";
		this.tag = "";
		this.typeOccurrenceRecordGroupId = null;
		this.unitId = null;
		this.area = null;
		this.datatype = null;
		this.typeOccurrenceRecordGroup = null;
		this.unit = null;
		this.notify = false;
		this.assessmentNeeded = false;
		this.selected = false;
		this.impactedAreas = [];
		this.releaseAreas = [];
		this.getExtremeValue = false;
		this.tagDevice = "";
		this.sending = false;
		this.sended = null;
    }
}
