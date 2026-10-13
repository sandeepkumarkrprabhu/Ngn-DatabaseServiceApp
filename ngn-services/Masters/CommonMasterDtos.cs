namespace ngn_services.Masters;

public sealed record MasterOperationResult<T>(T? Data, bool NotFound = false, bool Conflict = false, string? Error = null);
public sealed record StatusChangeRequest(bool IsActive);
