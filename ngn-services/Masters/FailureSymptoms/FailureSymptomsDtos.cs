using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.FailureSymptoms;

public sealed record FailureSymptomsRequest([Range(1, int.MaxValue)] int ProjectId, [Required, StringLength(200)] string FailureSymptomsName);
public sealed record FailureSymptomsResponse(int FailureSymptomsId, int ProjectId, string ProjectName, string FailureSymptomsName);
