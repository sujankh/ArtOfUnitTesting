using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MocksTesting.Tests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_ShortFileName_LogsError()
        {
            var mockWebService = new MockWebService();

            var logAnalyzer = new LogAnalyzer
            {
                WebService = mockWebService
            };

            logAnalyzer.Analyze("abc.txt");

            Assert.AreEqual(mockWebService.LogText, "Short FileName:abc.txt");
        }
    }

    class MockWebService : IWebService
    {
        public string LogText { get; set; }
        public void LogError(string errorMessage)
        {
            this.LogText = errorMessage;
        }
    }
}
