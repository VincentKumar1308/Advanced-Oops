using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeteriaManagement
{
    public interface IBalance
    {
        //wallet balance
         double WalletBalance{get;}

        //methods
        void WalletRecharge(double money);
        void DeductAmount(double money);
         
    }
}