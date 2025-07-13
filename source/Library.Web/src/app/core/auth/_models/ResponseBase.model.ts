export class ResponseBase<T> {
    result: T;
    success: boolean;
    message: string;
    errors: string[];


    clear(): void {
        this.result = null;
        this.success = null;
        this.message = null;
        this.errors = null;
	}
}
