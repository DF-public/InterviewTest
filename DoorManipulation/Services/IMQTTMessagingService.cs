using DoorManipulation.Models;

namespace DoorManipulation.Services;

public interface IMQTTMessagingService
{
	public Task PublishDoorStatusMessage(string doorId, string status);

	public Task<DoorStatus> WaitForDoorStatusChange(string doorId,
		CancellationToken cancellationToken = default);
}