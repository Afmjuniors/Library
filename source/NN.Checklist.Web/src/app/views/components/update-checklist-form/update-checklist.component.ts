import { State } from '../../../core/auth/_models/state.model';
import { TypeSeverity } from '../../../core/auth/_models/typeSeverity.model';
import { SystemNode } from '../../../core/auth/_models/systemNode.model';
import { ChangeDetectorRef, Component, Inject, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, ProgressSpinnerMode, ThemePalette } from '@angular/material';
import { AppService } from '../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { TypeEventCategory } from '../../../core/auth/_models/typeEventCategory.model';
import { TranslateService } from '@ngx-translate/core';
import { NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { SignatureComponent } from '../signature/signature.component';
import { VersionChecklistTemplate } from '../../../core/auth/_models/versionChecklistTemplate.model';
import { TypeComponent } from '../../../core/auth/_models/typeComponent.model';
import { ChecklistFilter } from '../../../core/auth/_models/checklistFilter.model';
import { ChecklistTemplate } from '../../../core/auth/_models/checklistTemplate.model';
import { ChecklistModel } from '../../../core/auth/_models/checklist.model';
import { FieldChecklist } from '../../../core/auth/_models/fieldChecklist.model';
import { ItemChecklist } from '../../../core/auth/_models/itemChecklist.model';
import { SignatureService } from '../../../core/auth/_services/signature.service';
import { Signature } from '../../../core/auth/_models/signature.model';
import { SignApproval } from '../../../core/auth/_models/signAprovval.model';
import { SignatureHistoryComponent } from '../signature-history/signature-history.component';
import { BlockVersionChecklistTemplate } from '../../../core/auth/_models/blockVersionChecklistTemplate.model';
import { DependencyItemVersionChecklistTemplate } from '../../../core/auth/_models/dependencyItemVersionChecklistTemplate.model';
import { OptionItemVersionChecklistTemplate } from '../../../core/auth/_models/optionItemVersionChecklistTemplate.model';

const DATE_TIME_FORMAT = {
  parse: {
    dateInput: 'YYYY-MM-DD HH:mm:ss',
  },
  display: {
    dateInput: 'YYYY-MM-DD HH:mm:ss',
    monthYearLabel: 'YYYY MMM',
    dateA11yLabel: 'DD',
    monthYearA11yLabel: 'YYYY MMM',
    enableMeridian: true,
    useUtc: true,
  }
};

@Component({
  selector: 'kt-update-checklist-form',
  templateUrl: './update-checklist.component.html',
  styleUrls: ['./update-checklist.component.scss'],
  providers: [
    { provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
  ],
})
export class UpdateCheklistForm implements OnInit {
  typeComponent: TypeComponent[] = [];
  fieldForm: FormGroup;
  loading = false;
  typeId: number = 0;
  checklist: ChecklistModel = new ChecklistModel();
  checklistVersion: VersionChecklistTemplate = new VersionChecklistTemplate();
  checklists: ChecklistTemplate[] = [];
  filter: ChecklistFilter = new ChecklistFilter();
  loadingTags: boolean = false;
  comments: string = '';
  remainingText: number = 8000;
  displayedColumns = ['title', 'signature'];
  displayedColumnsCell = ['all'];

  selectedOptions: { [key: string]: OptionItemVersionChecklistTemplate[] } = {};


  public title: string;
  public checklistDropDown: string;
  public versions: string;
  myInjector: Injector;
  public signatureService: SignatureService;

  constructor(
    public dialogRef: MatDialogRef<UpdateCheklistForm>,
    @Inject(MAT_DIALOG_DATA) public data: ChecklistModel,
    private app: AppService,
    public fb: FormBuilder,
    private layoutUtilsService: LayoutUtilsService,
    public dialog: MatDialog,
    private ref: ChangeDetectorRef,
    public translate: TranslateService,
  ) {

    this.checklist = data;
    this.checklistVersion = this.data.versionChecklistTemplate;
  }

  ngOnInit() {
    this.title = this.translate.instant('MENU.CHECKLIST');
    this.checklistDropDown = this.translate.instant('FILTERS.CHECKLIST');
    this.versions = this.translate.instant('FILTERS.VERSIONS');



  }




  saveInformation() {
    console.log(this.checklistVersion);
    console.log(this.checklist);

  }



  getValueField(idVersionTemplate): string {
    if (this.checklist.fields) {
      var val = this.checklist.fields.find(x => x.fieldVersionChecklistTemplateId == idVersionTemplate);
      var vF = this.checklistVersion.fieldsVersionChecklistsTemplate.find(x => x.fieldVersionChecklistTemplateId == idVersionTemplate)
      if (val) {
        if (vF.fieldDataTypeId == 4) {
          return vF.optionFieldVersionChecklistTemplate.find(x => x.optionFieldVersionChecklistTemplateId == val.optionFieldVersionChecklistTemplateId).title;
        }
        return val.value
      }
    }
    return ""
  }





  validate(): boolean {
    // Implement the actual validation logic
    return true;
  }

  onCheckboxValueChange(updatedOptions: OptionItemVersionChecklistTemplate[], itemVersionChecklistTemplateId:number): void {

    const key = `${itemVersionChecklistTemplateId}`;
    this.selectedOptions[key] = [...updatedOptions];
    console.log(this.selectedOptions);
  }





  valueChange(value) {
    console.log(value);
    this.remainingText = 8000 - value;
  }

  sign(idItemTemplate: number | null, blockTemplateId: number) {
    if (!idItemTemplate) {
      return;
    }
    let hasSignuture = false;
    if (!this.validate()) {
      return;
    }

    if (this.checklistVersion.checklistTemplateId <= 0) {
      this.layoutUtilsService.showErrorNotification(this.translate.instant('MISSING_TYPE_checklist_RECORD'), MessageType.Create);
      return;
    }
    if (this.checklist.items != null) {

      if (this.checklist.items.find(x => x.itemVersionChecklistTemplate.itemVersionChecklistTemplateId == idItemTemplate)) {
        hasSignuture = true;
      }
    }

    this.dialog.open(SignatureComponent, {
      minHeight: '300px',
      width: '400px',
      data: { result: true, hasComment: hasSignuture },
    }).afterClosed()
      .subscribe(x => {
        if (x != '' && x != undefined) {
          this.saveSignItem(x, idItemTemplate, blockTemplateId);
        }
      });
  }



  viewSignatureHistory(itemTemplateId: number) {
    const dialogRef = this.dialog.open(SignatureHistoryComponent,

      {
        width: '400px',
        data: { checklistId: this.checklist.checklistId, itemTemplateId: itemTemplateId }
      }).afterClosed()



  }

  saveSignItem(x: any, idItemTemplate: number, blockTemplateId: number) {
    let comment = x.comments;
   const optionsDto = this.selectedOptions[idItemTemplate];

    const newItem = new ItemChecklist(this.checklist.checklistId, this.checklistVersion.checklistTemplateId, optionsDto, blockTemplateId, x.stamp, idItemTemplate, comment);

    this.app.signItemChecklist(newItem, comment)
      .subscribe(res => {
        if (!res) {
          return;
        }
        this.checklist = res;
        this.checklistVersion = this.checklist.versionChecklistTemplate;
        this.ref.detectChanges();
      }, error => {
        this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
      });
  }

  saveFieldChecklist() {
    for (let index = 0; index < this.checklistVersion.fieldsVersionChecklistsTemplate.length; index++) {
      const element = this.checklistVersion.fieldsVersionChecklistsTemplate[index];
      const control = this.fieldForm.controls['name' + element.fieldVersionChecklistTemplateId];
      this.checklist.fields = this.checklist.fields || [];
      const field = new FieldChecklist();

      field.checklistId = this.checklist.checklistId;
      this.checklist.versionChecklistTemplateId = element.versionChecklistTemplateId;
      field.fieldChecklistId = null; // TODO: Handle properly
      field.fieldVersionChecklistTemplateId = element.fieldVersionChecklistTemplateId;
      field.optionFieldVersionChecklistTemplateId = element.optionFieldVersionChecklistTemplate != null ? element.optionFieldVersionChecklistTemplate.find(x => x.value == control.value).optionFieldVersionChecklistTemplateId : null;
      field.value = control.value;

      this.checklist.fields.push(field);
    }
  }



  getInitials(idItem: number): string {
    if (this.checklist.items) {
      const arr = this.checklist.items.sort((a, b) => {
        if (a.signature.dthSign < b.signature.dthSign) return 1; // a vem antes de b
        if (a.signature.dthSign > b.signature.dthSign) return -1;  // a vem depois de b
        return 0; // a e b são iguais
      });
      const item = arr.find(x => x.itemVersionChecklistTemplate.itemVersionChecklistTemplateId == idItem);
      if (item) {
        if (item.signature) {

          return item.signature.initials;
        }
      }
    }
    return '';
  }

  getDateSignature(idItem: number): Date | string {
    if (this.checklist.items) {
      const arr = this.checklist.items.sort((a, b) => {
        if (a.signature.dthSign < b.signature.dthSign) return 1; // a vem antes de b
        if (a.signature.dthSign > b.signature.dthSign) return -1;  // a vem depois de b
        return 0; // a e b são iguais
      });
      const item = arr.find(x => x.itemVersionChecklistTemplate.itemVersionChecklistTemplateId == idItem);
      if (item) {
        if (item.signature) {
          return item.signature.dthSignFormatted;
        }
      }
    }
    return '';
  }

  closeModal() {
    this.dialog.closeAll();
  }
}
