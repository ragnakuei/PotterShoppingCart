using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartLib
{
    public class ShoppingCart
    {
        public decimal CheckOut(List<ShoppingCartItem> shoppingCartItems)
        {
            var shouldPaymentItems = shoppingCartItems.AsEnumerable();
            return CalculatePayment(shouldPaymentItems);
        }

        private decimal CalculatePayment(IEnumerable<ShoppingCartItem> shoppingCartItems)
        {
            decimal payment = 0;
            decimal discount = 0;

            // 用一個變數儲存，來減少執行第二次數量計算
            int countForAmountGreaterEqual1 = GetCountForAmountGreaterEqual1(shoppingCartItems);
            switch (countForAmountGreaterEqual1)
            {
                case 1:
                    discount = 0;
                    break;
                case 2:
                    discount = 0.05m;
                    break;
                case 3:
                    discount = 0.1m;
                    break;
                case 4:
                    discount = 0.2m;
                    break;
                case 5:
                    discount = 0.25m;
                    break;
                default:
                    break;
            }
            payment += 100 * countForAmountGreaterEqual1 * (1 - discount);

            ItemsAmountMinus1(ref shoppingCartItems);
            while (GetCountForAmountGreaterEqual1(shoppingCartItems) >= 1)
            {
                payment += CalculatePayment(shoppingCartItems);
            }

            return payment;
        }

        // 計算 數量大於等於 1 的品項數
        private int GetCountForAmountGreaterEqual1(IEnumerable<ShoppingCartItem> shouldPaymentItems)
        {
            return shouldPaymentItems.Where(item => item.Amount >= 1).Count();
        }

        // 對數量大於等於 1 的品項數量減1
        private void ItemsAmountMinus1(ref IEnumerable<ShoppingCartItem> shouldPaymentItems)
        {
            foreach (var shoppingCartItem in shouldPaymentItems.Where(item => item.Amount >= 1).Select(i => i))
            {
                shoppingCartItem.AmountMinus1();
            }
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

        internal void AmountMinus1()
        {
            this._amount--;
        }
    }
}