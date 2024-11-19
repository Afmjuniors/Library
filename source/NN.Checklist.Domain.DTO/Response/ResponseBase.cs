using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace NN.Checklist.Domain.DTO.Common
{
    public class ResponseBase<T>
    {
        public ResponseBase()
        { }

        public ResponseBase(bool success)
        {
            Result = default;
            Success = success;
        }

        public ResponseBase(T result, bool success)
        {
            Result = result;
            Success = success;
        }

        public T Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ResponseBase<T> Error(params string[] errors)
        {
            var response = new ResponseBase<T>
            {
                Result = default,
                Success = false
            };

            response.Errors = errors.ToList();

            return response;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });
        }
    }
}
