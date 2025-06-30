using Grpc.Core;

namespace DoorManipulation.Services
{
	public class DoorControlService(ILogger<DoorControlService> logger, IMQTTMessagingService mqttMessagingService) : DoorControl.DoorControlBase
	{
		public override async Task<DoorResponse> OpenDoor(DoorRequest request, ServerCallContext context)
		{
			return await ChangeDoorStatus(request, context, "Open");
		}

		public override async Task<DoorResponse> CloseDoor(DoorRequest request, ServerCallContext context)
		{
			return await ChangeDoorStatus(request, context, "Closed");
		}

		private async Task<DoorResponse> ChangeDoorStatus(DoorRequest request, ServerCallContext context, string targetStatus)
		{
			try
			{
				mqttMessagingService.PublishDoorStatusMessage(request.Number, targetStatus);

				var result =
					await mqttMessagingService.WaitForDoorStatusChange(request.Number, context.CancellationToken);

				return new DoorResponse()
				{
					Battery = result.BatteryLevel.ToString(),
					Number = request.Number,
					Status = result.Status,
					Success = result.Status == targetStatus,
				};
			}
			catch
			{
				return new DoorResponse()
				{
					Number = request.Number,
					Status = "Error",
					Success = false,
					Battery = "0"
				};
			}
		}

		
	}
}
