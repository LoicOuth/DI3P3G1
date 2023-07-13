namespace Domain.Model;

public class ProductionInfo
{
	public ProductionInfo(){}

	public ProductionInfo(string time, double value)
	{
		Time = time;
		Value = value;
	}
	public string Time { get; set; }
	public double Value { get; set; }
}