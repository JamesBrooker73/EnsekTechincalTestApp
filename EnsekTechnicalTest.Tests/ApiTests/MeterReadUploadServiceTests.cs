using CsvHelper;
using EnsekTechincalTest.Models;
using EnsekTechincalTest.Results;
using EnsekTechincalTest.Services;
using MeterReadingLibrary.DataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextCopy;

namespace EnsekTechnicalTest.Tests.ApiTests
{
    public class MeterReadUploadServiceTests
    {
        private readonly Mock<IMeterReadCsvParserServcice> _mockMeterReadCsvParserServcice;
        private readonly Mock<IMeterReadingData> _mockMeterReadingData;
        private readonly Mock<ISqlDataAccess> _mockSql;
        private  MeterReadUploadService _sut;

        public MeterReadUploadServiceTests()
        {
            _mockMeterReadCsvParserServcice = new Mock<IMeterReadCsvParserServcice>();
            _mockMeterReadingData = new Mock<IMeterReadingData>();
            _mockSql = new Mock<ISqlDataAccess>();
            _sut = new MeterReadUploadService(
                _mockMeterReadCsvParserServcice.Object,
                _mockMeterReadingData.Object);

        }

        [Fact]
        public async void GivenNullStreamExceptionIsThrown()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.UploadMeterReads(null));
        }

        [Fact]
        public async Task GivenAMeterReadingIsUploadedSuccessfully_SuccessfulUploadCountWillIncrease()
        {

            _mockSql.Setup(x => x.UploadMeterRead<int, dynamic>(
                It.IsAny<string>(), It.IsAny<object>(), "Default"))
                .ReturnsAsync(1);

            var sut = new MeterReadingData(_mockSql.Object);

            var meterReadings = new List<MeterReadingModel>
            {
                new() { AccountId = 1, MeterReadValue = 12345, MeterReadDateTime = DateTime.UtcNow }
            };

            var result = await sut.UploadMeterRead(meterReadings);

            Assert.Equal(1, result.successfulUploadCount);
        }

        [Fact]
        public async Task GivenAMeterReadingIsRejected_RejectedUploadCountWillIncrease()
        {

            _mockSql.Setup(x => x.UploadMeterRead<int, dynamic>(
                It.IsAny<string>(), It.IsAny<object>(), "Default"))
                .ReturnsAsync(0);

            var sut = new MeterReadingData(_mockSql.Object);

            var meterReadings = new List<MeterReadingModel>
            {
               new() { AccountId = 2, MeterReadValue = -6, MeterReadDateTime = DateTime.UtcNow }
            };

            var result = await sut.UploadMeterRead(meterReadings);

            Assert.Equal(1, result.RejectedUploadCount);
        }
    }
}
