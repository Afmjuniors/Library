
export class QueryResultsModel {
	// fields
	entities: any[];
	rowsCount: number;
	totalPages: number;
	pageSize: number;
	errorMessage: string;	

	constructor(_items: any[] = [], _totalCount: number = 0, _pageSize = 0, _errorMessage: string = '') {
		this.entities = _items;
		this.rowsCount = _totalCount;
		this.pageSize = _pageSize;
		//this.totalPages = toInteger(_totalCount / _pageSize);
		if ((_totalCount % _pageSize) > 0)
		{
			this.totalPages++;
		}
	}
}
