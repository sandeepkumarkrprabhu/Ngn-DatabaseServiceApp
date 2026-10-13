using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.NatureOfProblems;

public sealed record NatureOfProblemRequest([Required, StringLength(200)] string NatureOfProblemName, int? ProjectItemId);
public sealed record NatureOfProblemResponse(int NatureOfProblemId, string NatureOfProblemName, int? ProjectItemId, string? ProjectItemName);
