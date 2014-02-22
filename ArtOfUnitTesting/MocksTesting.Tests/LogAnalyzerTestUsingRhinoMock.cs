using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;

namespace MocksTesting.Tests
{
    [TestFixture]
    public class LogAnalyzerTestUsingRhinoMock
    {
        [Test]
        public void Analyze_ShortFileName_LogsError()
        {
            var mocks = new MockRepository();
            var mockWebService = mocks.StrictMock<IWebService>();

            using (mocks.Record())
            {
                mockWebService.LogError("Short FileName:abc.txt");
            }

            var logAnalyzer = new LogAnalyzer
            {
                WebService = mockWebService
            };

            logAnalyzer.Analyze("abc.txt");

            mocks.Verify(mockWebService);
        }
    }
}

