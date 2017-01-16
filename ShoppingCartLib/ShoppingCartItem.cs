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
            var shoudPaymentItems = shoppingCartItems.Where(item => item.Amount >= 1);
            decimal payment = 0;
            decimal discount = 0;

            if (shoudPaymentItems.Count() == 1)
            { discount = 0; }
            else if (shoudPaymentItems.Count() == 2)
            { discount = 0.05m; }
            else if (shoudPaymentItems.Count() == 3)
            { discount = 0.1m; }
            else if (shoudPaymentItems.Count() == 4)
            { discount = 0.2m; }

            payment = CalculatePayment(discount, shoudPaymentItems);

            return payment;
        }

        private decimal CalculatePayment(decimal discount, IEnumerable<ShoppingCartItem> shoudPaymentItems)
        {
            return 100 * shoudPaymentItems.Sum(item => item.Amount) * (1 - discount);
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