
using EnsekTechincalTest.Models;
using EnsekTechincalTest.Results;

namespace EnsekTechincalTest.Services;

public interface IMeterReadCsvParserServcice
{
    MeterReadingParsingResult ParseCsv(Stream stream);
}