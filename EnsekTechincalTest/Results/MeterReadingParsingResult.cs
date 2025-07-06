using EnsekTechincalTest.Models;

namespace EnsekTechincalTest.Results;

public record MeterReadingParsingResult
{
    public IEnumerable<MeterReadingModel> ParsedMeterReadings { get; init; }
    public int FailedParseCount { get; init; }

    public MeterReadingParsingResult(IEnumerable<MeterReadingModel> parsedMeterReadings, int failedParseCount)
    {
        ParsedMeterReadings = parsedMeterReadings;

        FailedParseCount = failedParseCount;

    }
}
