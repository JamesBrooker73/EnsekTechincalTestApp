using CsvHelper;
using CsvHelper.Configuration;
using EnsekTechincalTest.DataMap;
using EnsekTechincalTest.Models;
using EnsekTechincalTest.Results;
using System.Globalization;

namespace EnsekTechincalTest.Services
{
    public class MeterReadCsvParserServcice : IMeterReadCsvParserServcice
    {
        public MeterReadingParsingResult ParseCsv(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            return ParseMeterReadings(stream);

        }

        public MeterReadingParsingResult ParseMeterReadings(Stream stream)
        {
            int failedParseCount = default;
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = (_) => { failedParseCount++; },
                ReadingExceptionOccurred = (_) => { failedParseCount++; return false; },
            };

            using var reader = new StreamReader(stream);
            using var csvReader = new CsvReader(reader, csvConfiguration);
            csvReader.Context.RegisterClassMap<CsvDataMap>();
            var parsedMeterReadings = csvReader.GetRecords<MeterReadingModel>().ToList();

            return new MeterReadingParsingResult(parsedMeterReadings, failedParseCount);
                
        }
    }
}
