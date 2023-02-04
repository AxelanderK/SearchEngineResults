using System.Globalization;
using System;

namespace SearchengineResult.Models
{
    public class Result
    {
        public string? Name { get; set; }
        public long ResultCount { get; set; }
        public string ResultCountConverted
        {
            get
            {
                if (ResultCount > 999999999 || ResultCount < -999999999)
                {
                    return ResultCount.ToString("#,##0,,,.##B", CultureInfo.InvariantCulture);
                }
                else if (ResultCount > 999999 || ResultCount < -999999)
                {
                    return ResultCount.ToString("#,##0,,.##M", CultureInfo.InvariantCulture);
                }
                else if (ResultCount > 999 || ResultCount < -999)
                {
                    return ResultCount.ToString("#,##0,.##K", CultureInfo.InvariantCulture);
                }
                else
                {
                    return ResultCount.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

    }
}
