using CsvHelper;
using EnsekTechincalTest.Exceptions;
using EnsekTechincalTest.Models;
using EnsekTechincalTest.Results;
using MeterReadingLibrary.DataAccess;
using System.IO;

namespace EnsekTechincalTest.Services;

public class MeterReadUploadService : IMeterReadUploadService
{
    private readonly IMeterReadCsvParserServcice _meterReadCsvParserServcice;
    private readonly IMeterReadingData _meterReadingData;

    public MeterReadUploadService(IMeterReadCsvParserServcice meterReadCsvParserServcice, IMeterReadingData meterReadingData)
    {
        _meterReadCsvParserServcice = meterReadCsvParserServcice ?? throw new ArgumentNullException(nameof(_meterReadCsvParserServcice));
        _meterReadingData = meterReadingData ?? throw new ArgumentNullException(nameof(meterReadingData));

    }

    public async Task<MeterReadUploadResultModel> UploadMeterReads(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        try
        {
            var parsedResult = _meterReadCsvParserServcice.ParseCsv(stream);
            var uploadResult = await _meterReadingData.UploadMeterRead(parsedResult.ParsedMeterReadings);

            return new MeterReadUploadResultModel
            {
                failedParseCount = parsedResult.FailedParseCount,
                successfulUploadCount = uploadResult.successfulUploadCount,
                failedUploadoutCount = uploadResult.RejectedUploadCount
            };
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred uploading the meter reads", ex);
        }
    }

}
