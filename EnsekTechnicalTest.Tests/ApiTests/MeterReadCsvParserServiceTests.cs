using ApprovalTests;
using ApprovalTests.Reporters;
using EnsekTechincalTest.Services;
using Newtonsoft.Json;
using System.Text;

namespace EnsekTechnicalTest.Tests.ApiTests
{
    [Trait("IO", "CSV")]
    [UseReporter(typeof(DiffReporter))]
    public class MeterReadCsvParserServiceTests
    {
        private readonly MeterReadCsvParserServcice _sut;

        public MeterReadCsvParserServiceTests()
        {
            _sut = new MeterReadCsvParserServcice();
        }

        [Fact]
        public void GivenNullStreamExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.ParseCsv(null));
        }

        [Fact]
        public void GivenEmptyCSVNoRecordsReturned()
        {
            var result = _sut.ParseMeterReadings(new MemoryStream());

            Assert.Empty(result.ParsedMeterReadings);
        }

        [Fact]
        public void GivenEmptyCsvNoFailedParsesAreRecorded()
        {
            var result = _sut.ParseMeterReadings(new MemoryStream());

            Assert.Equal(0, result.FailedParseCount);
        }

        [Fact]
        public void GivenCsvWithBadRecords_BadRecordsAreRemoved()
        {
            const string path = "C:\\Users\\james\\source\\repos\\EnsekTechincalTestApp\\EnsekTechnicalTest.Tests\\Data\\Invalid_Meter_Readings_1.csv";
            ParseAndApproveTestCsvFile(path);
        }

        [Fact]
        public void GivenCsvWithGoodRecords_NoRecordsAreRemoved_()
        {
            const string path = "C:\\Users\\james\\source\\repos\\EnsekTechincalTestApp\\EnsekTechnicalTest.Tests\\Data\\Valid_Meter_Readings_1.csv";
            ParseAndApproveTestCsvFile(path);
        }

        [Fact]
        public void GivenCsvWithGoodAndBadRecords_BadRecordsAreRemoved()
        {
            const string path = "C:\\Users\\james\\source\\repos\\EnsekTechincalTestApp\\EnsekTechnicalTest.Tests\\Data\\Meter_Readings.csv";
            ParseAndApproveTestCsvFile(path);
        }

        private void ParseAndApproveTestCsvFile(string filePath)
        {
            var fileString = CsvLoader(filePath);

            var result = _sut.ParseMeterReadings(fileString);

            Approvals.Verify(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private Stream CsvLoader(string filePath)
        {
            return File.OpenRead(filePath);
        }
    }
}
