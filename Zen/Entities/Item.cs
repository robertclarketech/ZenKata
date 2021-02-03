namespace Zen.Entities
{
	public class Item
	{
		public string Code { get; set; } = string.Empty;
		public int Cost { get; set; }

		public Discount? Discount { get; set; }
	}
}
