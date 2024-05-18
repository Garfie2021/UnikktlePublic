using Unikktle.Common;
using System.Collections.Generic;

namespace Unikktle.Models
{
    public class BuyViewModel
    {
        // 購入したクレジット
        public int Credit { get; set; }

        // 状態
        public PayPalBuyStatus PayPalBuyStatus { get; set; }
    }
}
