using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryShop
{
    public interface IBalance
    {
        //properties
         double WalletBalance{get;set;}
         void WalletRecharge(double money);

    }
}