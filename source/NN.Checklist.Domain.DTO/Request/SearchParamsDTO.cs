using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO.Request
{
    public class SearchParamsDTO
    {
        public enum enumType
        {
            Alarm = 1,
            Event = 2
        }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LastCondition { get; set; }
        public int? Status { get; set; }
        public int? Process{ get; set; }
        public int? Area { get; set; }
        public int? OccurrenceRecordId { get; set; }
        public bool? Impacts { get; set; }
        public int? IdType { get; set; }
        public enumType OccurrenceType 
        { 
            get
            {
                if (IdType.HasValue)
                {
                    return IdType.Value == 1 ? enumType.Alarm : (IdType.Value == 2 ? enumType.Event : enumType.Alarm);
                }

                return enumType.Alarm;
            }
        }
        public string SendMail { get; set; }
        public string Alarm { get; set; }
        public int? State { get; set; }
        public bool? EndDateFilled { get; set; }
        public bool? AssessmentNeeded { get; set; }

    }
}
