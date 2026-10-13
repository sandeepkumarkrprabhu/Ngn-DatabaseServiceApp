using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.CallTypes;

public sealed record CallTypeRequest([Required, StringLength(50)] string CallName);
public sealed record CallTypeResponse(int CallTypeId, string CallName);
