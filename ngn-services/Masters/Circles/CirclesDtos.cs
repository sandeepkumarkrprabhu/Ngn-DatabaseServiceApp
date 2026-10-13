using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.Circles;

public sealed record CircleRequest([Required, StringLength(50)] string CircleCode, [Required, StringLength(100)] string CircleName, [Required, StringLength(50)] string CityCode, [Required, StringLength(100)] string CityName);
public sealed record CircleResponse(int CircleCityId, string CircleCode, string CircleName, string CityCode, string CityName);
