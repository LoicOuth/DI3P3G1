namespace Domain.Model;

public class ConsumptionInfo
{
	public ConsumptionInfo(){}

	public ConsumptionInfo(string time, double value)
	{
		Time = time;
		Value = value;
	}
	public string Time { get; set; }
	public double Value { get; set; }
}