using DoorManipulation.Models;

namespace DoorManipulation.Services
{
	public class StubMQTTMessagingService : IMQTTMessagingService
	{
		public Task PublishDoorStatusMessage(string doorId, string status)
		{
			// Assume that message has been sent as we do not have door control

			return Task.CompletedTask;
		}

		public Task<DoorStatus> WaitForDoorStatusChange(string doorId, CancellationToken cancellationToken = default)
		{
			// Assume that 3 cases may happen: door will not accept command, door will do scenario or message would not be delivered.
			// Based on that for the stub we will use just random.
			var result = new Random().Next(0, 3);

			switch (result)
			{
				case 1:
					return Task.FromResult(new DoorStatus
					{
						DoorId = doorId,
						Status = "Open",
						BatteryLevel = 56
					});
				case 2:
					return Task.FromResult(new DoorStatus
					{
						DoorId = doorId,
						Status = "Closed",
						BatteryLevel = 89
					});
				case 3:
					Thread.Sleep(2000);
					throw new TimeoutException("MQTT response timeout simulated");
				default:
					throw new NotSupportedException();
			}
		}
	}
}
