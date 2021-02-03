using System.Collections.Generic;
using Zen.Entities;
using Zen.Exceptions;

namespace Zen
{
	public class Checkout
	{
		private readonly Dictionary<string, Item> _allItems;
		private readonly Dictionary<string, int> _cart = new Dictionary<string, int>();

		public Checkout(Dictionary<string, Item> allItems)
		{
			_allItems = allItems;
		}

		public Checkout AddItem(string code)
		{
			if (!_allItems.ContainsKey(code))
			{
				throw new ItemCodeNotRecognisedException();
			}

			if (!_cart.ContainsKey(code))
			{
				_cart.Add(code, 1);
			}
			else
			{
				_cart[code]++;
			}

			return this;
		}

		public int CalculateTotalCost()
		{
			var total = 0;
			foreach (var scannedItem in _cart)
			{
				if (!_allItems.TryGetValue(scannedItem.Key, out var item))
				{
					throw new ItemCodeDoesNotExistException($"There is no item with code: {scannedItem.Key}.");
				}

				if (item.Discount != null)
				{
					var specialAmount = scannedItem.Value / item.Discount.AmountNeeded;
					total += specialAmount * item.Discount.NewPrice;
					total += (scannedItem.Value % item.Discount.AmountNeeded) * item.Cost;
				}
				else
				{
					total += scannedItem.Value * item.Cost;
				}
			}
			return total;
		}
	}
}
