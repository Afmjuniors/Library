using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request
{
    public class SearchChecklistParamsDTO
    {

    public DateTime? StartDate{get; set;}
	public DateTime? EndDate {get; set;}
	public long? ChecklistId{get; set;}
	public long? ChecklistTemplateId{get; set;}
	public long? VersionChecklistTemplateId { get; set; }
    }
}
