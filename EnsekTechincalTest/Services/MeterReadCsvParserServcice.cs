using CsvHelper;
using CsvHelper.Configuration;
using EnsekTechincalTest.DataMap;
using EnsekTechincalTest.Exceptions;
using EnsekTechincalTest.Models;
using EnsekTechincalTest.Results;
using System.Globalization;

namespace EnsekTechincalTest.Services;

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
            BadDataFound = _ => { failedParseCount++; },
            ReadingExceptionOccurred = _ => { failedParseCount++; return false; },
        };

        using var reader = new StreamReader(stream);
        using var csvReader = new CsvReader(reader, csvConfiguration);
        csvReader.Context.RegisterClassMap<CsvDataMap>();

        try
        {
            var parsedMeterReadings = csvReader.GetRecords<MeterReadingModel>().ToList();
            return new MeterReadingParsingResult(parsedMeterReadings, failedParseCount);
        }
        catch (ParsingException ex) 
        {
            throw new ParsingException("An error occurred parsing the meter reads", ex);
        }
        

        
            
    }
}
