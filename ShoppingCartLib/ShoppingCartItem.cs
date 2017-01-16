using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartLib
{
    public class ShoppingCart
    {
        public decimal CheckOut(List<ShoppingCartItem> shoppingCartItems)
        {
            return CalculatePayment(shoppingCartItems);
        }

        private decimal CalculatePayment(List<ShoppingCartItem> shoppingCartItems)
        {
            // 數量大於等於 1 的品項 = 1
            var shoudPaymentItems = shoppingCartItems.Where(item => item.Amount >= 1);
            if (shoudPaymentItems.Count() == 1)
            {
                return 100 * shoudPaymentItems.Count();
            }
            return 0;
        }
    }

    public class ShoppingCartItem
    {
        private int _productId;
        private string _productName;
        private int _amount;

        public int Amount { get { return _amount; } }

        public ShoppingCartItem(int productId, string productName, int amount)
        {
            this._productId = productId;
            this._productName = productName;
            this._amount = amount;
        }
    }
}