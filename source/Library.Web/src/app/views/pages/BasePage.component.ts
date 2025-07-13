// Angular

import { OnInit, EventEmitter, Output, OnDestroy, HostListener } from '@angular/core';
import { AuthService, checkHasUserPermission, currentUser, currentUserPermissions, isUserLoaded, Logout, User } from '../../core/auth';
import { map, tap } from 'rxjs/operators';
import { Observable, of, Subscription, timer } from 'rxjs';
import { select, Store } from '@ngrx/store';
import { AppState } from '../../core/reducers';
import { Router } from '@angular/router';
import { PermissaoAcao } from '../../core/auth/_models/permissaoAcao.model';
import { TranslateService } from '@ngx-translate/core';
import { ParameterService } from '../../core/auth/_services';
import { LayoutUtilsService } from '../../core/_base/crud';
import { IdleService } from '../../core/_base/layout/services/idle.service';

export class BasePageComponent {
    loading: boolean = false;

    // Subscriptions
    public subscriptions: Subscription[] = [];
    public permissions: PermissaoAcao[] = [];
    private ok: boolean = false;
    private errorMsg: string = "";
    timer: any;
    private inactivityTime: number = 300;

    constructor(public auth: AuthService,
        public store: Store<AppState>,
        public translate: TranslateService,
        public router: Router,
        public tagPermission: string,
        public tags: string[],
        public title: string,
        public parameterService: ParameterService,
        public idleService: IdleService,
    ) {

        idleService.restartTimer();

        if (tagPermission != null)
        {
            //this.errorMsg = translate.instant("ACCESSDENIED") + ` '${title}'`;
            let self = this;
            const s1 = this.checkAccessPermission(this.tagPermission)
                .subscribe(valido => {
                    if (!valido) {
                        router.navigate(['/inicio'], { queryParams: { error: self.errorMsg } });
                    }
                },
                error =>{
                    
                }
                );

            this.subscriptions.push(s1);
        }

        if (tags && tags.length > 0) {
            tags.forEach(tag => {
                const s = this.checkPermission(tag).subscribe(
                    res => {
                        const acao = new PermissaoAcao();
                        acao.tag = tag;
                        acao.valor = res;
                        acao.atualizado = true
                        this.permissions.push(acao);
                    }
                )
                this.subscriptions.push(s);
            },
            error =>{
                
            });
        }
    }

    @HostListener('click')
    onclick() {
        this.idleService.restartTimer();
    }

    @HostListener('mouseenter')
    onmouseenter() {
        this.idleService.restartTimer();
    }

    @HostListener('keydown')
    onkeydown() {
        this.idleService.restartTimer();
    }

    @HostListener('keypress')
    onkeypress() {
        this.idleService.restartTimer();
    }

    @HostListener('window:click')
    onClick() {
        this.idleService.restartTimer();
    }

    public readPermission(tag: string): boolean {
        let obj = this.permissions.find(x => x.tag == tag);
        if (obj) {
            let cont = 0;
            while (!obj.atualizado && cont < 1000) {
                obj = this.permissions.find(x => x.tag == tag);
                cont++;
            }
            return obj.valor;
        }
        return false;
    }

    public checkAccessPermission(tag: string): Observable<boolean> {
        if (!tag) {
            return of(false);
        }

        this.loading = true;
        let self = this;
        return this.auth.verificarPermissao(tag)
            .pipe(tap(valida => {
                this.loading = false;
                if (!valida) {
                    this.router.navigate(['/inicio'], { queryParams: { error: this.errorMsg } });
                }
            },
                error => {
                    this.loading = false;
                    this.router.navigate(['/inicio'], { queryParams: { error: error.error } });
                }
            ));

    }

    public checkPermission(tag): Observable<boolean> {
        return this.store.pipe(select(currentUser),
            map(u => {
                let obj;
                if (u != null && u.permissions != null)
                {
                    obj = u.permissions.find(x => x.tag == tag);
                }
                return obj != undefined && obj.tag != null;
            }, error => {
                return false;
            }));
    }
}
