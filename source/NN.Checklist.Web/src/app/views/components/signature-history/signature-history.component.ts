import { Component, OnInit, Inject } from '@angular/core';
import { currentUser } from '../../../core/auth';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { LayoutUtilsService } from '../../../core/_base/crud';
import { select, Store } from '@ngrx/store';
import { AppState } from '../../../core/reducers';

import { TranslateService } from '@ngx-translate/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SignatureService } from '../../../core/auth/_services/signature.service';
import { Signature } from '../../../core/auth/_models/signature.model';
import { User } from '../../../core/auth/_models/user.model';
import { SignApproval } from '../../../core/auth/_models/signAprovval.model';
import { OptionItemVersionChecklist } from 'src/app/core/auth/_models/optionItemVersionChecklist.model';
import { HistorySignutureModel } from 'src/app/core/auth/_models/historySignature.model';

@Component({
    selector: 'td-signature-history',
    templateUrl: './signature-history.component.html',
    styleUrls: ['./signature-history.component.scss'],
    providers: [SignatureService]
})
export class SignatureHistoryComponent implements OnInit {

    loading: boolean = false;
    permissionTag: string = '';
    username: string = '';
    password: string = '';
    errors: string[] = [];
    user: User = null;
    showPassword: boolean = false;
    dataSource: MatTableDataSource<any> = new MatTableDataSource();
hasOptions:boolean =false;

    displayedColumns:string[];



    /**
     * Component constructor
     *
     * @param store: Store<AppState>
     * @param router: Router
     */
    constructor(
        public store: Store<AppState>,
        public dialog: MatDialog,
        public translateService: TranslateService,
        public dialogRef: MatDialogRef<string>,
        public signatureService: SignatureService,
        private layoutUtilsService: LayoutUtilsService,
        @Inject(MAT_DIALOG_DATA)
        public data: { checklistId: number, itemTemplateId: number }
    ) {

    }

    /**
     * On init
     */
    ngOnInit() {
        this.store.pipe(select(currentUser)).subscribe((x: User) => {
            this.user = x;
            this.username = this.user.userAD;
        },
            error => {
                this.layoutUtilsService.showErrorNotification(error);
            });

        this.loadSignatureHistory();
    }

    /**
     * On Destroy
     */
    ngOnDestroy() {
        //this.subscriptions.forEach(el => el.unsubscribe());
    }

    loadSignatureHistory() {
        this.errors = [];
    
        if (this.errors.length == 0) {
            this.signatureService.GetSignatureHistory(this.data.checklistId, this.data.itemTemplateId).subscribe((x) => {
               this.hasOptions = x.some(x=>x.optionsAvalible);
               if(this.hasOptions){
                this.displayedColumns = ['user', "date", "comments","selected"]
               }else{
                this.displayedColumns = ['user', "date", "comments"]
               }
                this.dataSource.data = x.map(item => {
                    if(item.optionsAvalible!=undefined && item.optionsAvalible.length>0 ){

                        item.optionsAvalible = item.optionsAvalible.map(op => {
                            let icon = "";
                            if(item.optionsSelected.length >0){

                            if (item.optionsSelected.some(opS => opS.optionItemVersionChecklistTemplateId == op.optionItemVersionChecklistTemplateId)) {
                                icon = "check_box";
                            } else {
                                icon = "crop_square";
                            }    
                        }
                        return {
                            ...op,
                            icon
                        };
                    });
                }
                    return item;
                });
            }, responseError => {
                this.errors.push((typeof responseError.error === 'string') ? responseError.error : responseError.message);
            });
        }
    }


    closeModal() {
        this.dialogRef.close();
    }

}
