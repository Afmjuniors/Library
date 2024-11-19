using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN.Checklist.Domain.DTO
{
    public class QueryParamsDTO
    {
        public string SortOrder { get; set; }
        public string SortField { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public object? Filter { get; set; }
        /// <summary>
        /// The "GetFilter" method converts an object to a file.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        
        public TOutput GetFilter<TOutput>()
        {
            return (TOutput)Convert.ChangeType(Filter, typeof(TOutput));
        }
    }
}
