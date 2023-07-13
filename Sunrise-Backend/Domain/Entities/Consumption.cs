using Domain.Model.Stepper;

namespace Domain.Entities;

public class Consumption
{
	public TimeSpan StartDate { get; set; }
	public TimeSpan EndDate { get; set; }
	public int PeopleNumber { get; set; }
	public List<Device> Device { get; set; }
	public int WashingNumber { get; set; }
	public double EstimatedConsumption { get; set; }
}
