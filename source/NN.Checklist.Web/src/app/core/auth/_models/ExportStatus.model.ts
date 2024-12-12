//rotas.model.ts
export class ExportStatus {
    path: string;

    clear(): void {
        this.path = '';
    }
}