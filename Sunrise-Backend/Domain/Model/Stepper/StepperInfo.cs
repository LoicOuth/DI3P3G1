namespace Domain.Model.Stepper;

public class StepperInfo
{
	public string StartDate { get; set; }
	public string EndDate { get; set; }
	public int PeopleNumber { get; set; }
	public List<string> Device { get; set; }
	public int WashingNumber { get; set; }
}

public class HomeDevice
{
	public List<Device> devices { get; }=
		new List<Device>()
		{
			new Device("WashingMachine", "Machine à laver", 0.5),
			new Device("Tv", "Télévision", 3),
			new Device("Oven", "Four", 2),
			new Device("Dishwasher", "Lave Vaisselle", 5.5),
			new Device("ElectricHeating", "chauffage électrique", 2.5),
			new Device("WaterHeating", "Chauffe eau", 15)
		};
}

public class SelectedDevice
{
	public string Id { get; set; }
}

public class Device
{
	public string Id { get; set; }
	public string Name { get; set; }
	public double Value { get; set; }
	


	public Device(string id, string name, double value)
	{
		Id = id;
		Name = name;
		Value = value;
		 
	}
}