using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QwickFoodz
{
    public interface IBalance
    {
        //properties
        double walletBalance{get;set;}
        //methods
        void WalletRecharge(double money);
        void DeductBalance(double  money);
    }
}