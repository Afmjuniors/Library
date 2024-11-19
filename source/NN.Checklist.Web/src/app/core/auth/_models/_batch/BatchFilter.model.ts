
export class BatchFilter 
{
	areaId: number;
	processId: number;
  startDate: Date;
  endDate: Date;
    
  clear(): void {
  this.areaId = null;
  this.processId = null;
      this.startDate = null;
      this.endDate = null;
	}
}
