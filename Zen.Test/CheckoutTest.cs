using System.Collections.Generic;
using Xunit;
using Zen.Entities;
using Zen.Exceptions;

namespace Zen.Test
{
	public class CheckoutTest
	{
		private Dictionary<string, Item> AllItems;
		private Checkout Checkout;

		public CheckoutTest()
		{
			AllItems = new Dictionary<string, Item>
			{
				{ "A", new Item { Code = "A", Cost = 10, Discount = new Discount{ AmountNeeded = 3, NewPrice = 25 }  } },
				{ "B", new Item { Code = "B", Cost = 20, Discount = new Discount{ AmountNeeded = 2, NewPrice= 30 }  } },
				{ "C", new Item { Code = "C", Cost = 30 } }
			};
			Checkout = new Checkout(AllItems);
		}

		[Fact]
		public void AddItem_ThrowsItemCodeNotRecognisedException_WhenItemCodeDoesNotExist()
		{
			//arrange
			var itemCodeToAdd = "THIS CODE DOES NOT EXIST";

			//act + assert
			Assert.Throws<ItemCodeNotRecognisedException>(() => Checkout.AddItem(itemCodeToAdd));
		}

		[Fact]
		public void CalculateTotalCost_ReturnsADiscountedCostOfFifty_WhenItemAIsAddedSixTimes()
		{
			//arrange
			const int expected = 50;
			for (int i = 0; i < 6; i++)
			{
				Checkout.AddItem("A");
			}

			//act
			var actual = Checkout.CalculateTotalCost();

			//assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("A", 10)]
		[InlineData("B", 20)]
		[InlineData("C", 30)]
		public void CalculateTotalCost_ReturnsTheCostOfTheItem_WhenOnlyOneItemIsAdded(string itemCode, int expectedCost)
		{
			//arrange
			Checkout.AddItem(itemCode);

			//act
			var actual = Checkout.CalculateTotalCost();

			//assert
			Assert.Equal(expectedCost, actual);
		}

		[Theory]
		[InlineData("A", 3, 25)]
		[InlineData("B", 2, 30)]
		public void CalculateTotalCost_ReturnsTheDiscountedCost_WhenTheItemHasADiscount_AndTheCorrectAmountIsAdded(string itemCode, int amount, int expectedCost)
		{
			//arrange
			for (int i = 0; i < amount; i++)
			{
				Checkout.AddItem(itemCode);
			}

			//act
			var actual = Checkout.CalculateTotalCost();

			//assert
			Assert.Equal(expectedCost, actual);
		}

		[Fact]
		public void CalculateTotalCost_ReturnsTotalCostOfSixty_WhenItemAAndItemBAndItemCIsAdded()
		{
			//arrange
			const int expected = 60;
			Checkout.AddItem("A")
				.AddItem("B")
				.AddItem("C");

			//act
			var actual = Checkout.CalculateTotalCost();

			//assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CalculateTotalCost_ReturnsTotalCostOfTwenty_WhenItemAIsAddedTwice()
		{
			//arrange
			const int expected = 20;
			var itemCodeToAdd = "A";
			Checkout.AddItem(itemCodeToAdd)
				.AddItem(itemCodeToAdd);

			//act
			var actual = Checkout.CalculateTotalCost();

			//assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CalculateTotalCost_ThrowsItemCodeDoesNotExistException_WhenItemCodeNoLongerExists()
		{
			//arrange
			Checkout.AddItem("A");
			AllItems.Remove("A");

			//act + assert
			Assert.Throws<ItemCodeDoesNotExistException>(() => Checkout.CalculateTotalCost());
		}
	}
}
