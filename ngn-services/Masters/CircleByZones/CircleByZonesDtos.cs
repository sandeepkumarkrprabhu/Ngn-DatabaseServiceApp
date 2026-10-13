using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.CircleByZones;

public sealed record CircleByZoneRequest([Required, StringLength(50)] string Zone, [Required, StringLength(100)] string CircleName);
public sealed record CircleByZoneResponse(int CircleZoneId, string Zone, string CircleName);
