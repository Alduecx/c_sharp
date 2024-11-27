using Shared.Model.Records;

namespace Shared.Model.Messages;

public record GeneratingTeam(int ExperimentId, int HackathonCount, Team Team);