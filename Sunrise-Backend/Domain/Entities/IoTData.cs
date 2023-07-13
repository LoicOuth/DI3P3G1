using Domain.Entities.Base;

namespace Domain.Entities;

public class IoTData : BaseEntity
{
	public double voltage { get; set; }
	public DateTime EventProcessedUtcTime { get; set; }
	public int PartitionId { get; set; }
	public DateTime EventEnqueuedUtcTime { get; set; }
	public IoTHub IoTHub { get; set; }
}

public class IoTHub
{
	public string MessageId { get; set; }
	public string CorrelationId { get; set; }
	public string ConnectionDeviceId { get; set; }
	public string ConnectionDeviceGenerationId { get; set; }
	public DateTime EnqueuedTime { get; set; }
}