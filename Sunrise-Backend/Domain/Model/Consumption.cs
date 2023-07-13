namespace Domain.Model;

public class Consumption
{
	public Dictionary<string,object> AdditionalProperties { get; set; }
	public string Date { get; set; }
	public string Interval_length { get; set; }
	public string Measure_type { get; set; }
	public string Value { get; set; }
}