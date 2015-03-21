using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{

    static public class ActionResultExtentions
    {
        static public ContentResult JsonNet(this Controller src, Object obj)
        {
            var results = JsonConvert.SerializeObject(obj);
            return new ContentResult
            {
                Content = results,
                ContentType = "application/json"
            };
        }

        static public ContentResult JsonNet(this Controller src, Object obj, JsonSerializerSettings jss)
        {
            var results = JsonConvert.SerializeObject(obj, jss);
            return new ContentResult
            {
                Content = results,
                ContentType = "application/json"
            };
        }

        static public ContentResult JsonNetError(this Controller src, Object obj, int statucCode)
        {
            src.HttpContext.Response.StatusCode = statucCode;
            src.HttpContext.Response.TrySkipIisCustomErrors = true;
            var results = JsonConvert.SerializeObject(obj);
            return new ContentResult
            {
                Content = results,
                ContentType = "application/json"
            };
        }

        static System.Text.Encoding sjisenc = System.Text.Encoding.GetEncoding("shift_jis");

        static public FileContentResult CsvFile(this Controller src, string csv)
        {
            var bufffer = sjisenc.GetBytes(csv);
            var result = new FileContentResult(bufffer, "text/csv");
            return result;
        }
        static public FileContentResult CsvFile(this Controller src, string csv, string fileName)
        {
            //new FileContentResult(csv, "text/csv", "formdata.csv")
            var result = CsvFile(src, csv);
            result.FileDownloadName = fileName;
            return result;
        }
    }
}
