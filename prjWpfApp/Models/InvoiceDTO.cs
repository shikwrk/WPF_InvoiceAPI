using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjWpfApp.Models
{
    public class InvoiceDTO
    {
        [DisplayName("城市代碼")]
        public string HsnCd { get; set; }
        [DisplayName("城市名稱")]
        public string HsnNm { get; set; }
        [DisplayName("產業類型")]
        public string BusiNm { get; set; }
        [DisplayName("發票日期") ]
        public string InvoiceYm { get; set; }
        [DisplayName("發票金額")]
        public long InvoiceCreateAmt { get; set; }
        [DisplayName("發票數量")]
        public int InvoiceCreateCnt { get; set; }
        [DisplayName("儲存類型")]
        public string CarrierTypeNm { get; set; }

    }
}
