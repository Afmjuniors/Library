import { OptionItemVersionChecklist } from "./optionItemVersionChecklist.model";


export class HistorySignutureModel {
    signature: string;
    optionsSelected:OptionItemVersionChecklist[];
    optionsAvalible:OptionItemVersionChecklist[];
    isRejected:boolean;

    clear(): void {
		this.signature = "";
        this.optionsSelected = null;
        this.optionsAvalible = null;

	}
}
