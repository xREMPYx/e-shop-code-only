namespace e_shop.Utils
{
	public class MyRandom : Random
	{
		private List<int> _ints = new List<int>();

		public override int Next(int minValue, int maxValue)
		{
			int result = base.Next(minValue, maxValue);

			if (!_ints.Contains(result) || _ints.Count() > (maxValue - minValue))
			{
				_ints.Add(result);
				return result;				
			}
			else
			{
				return Next(minValue, maxValue);
			}
		}
	}
}
