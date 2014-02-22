using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;

namespace MocksTesting.Tests
{
    [TestFixture]
    public class LogAnalyzerTestUsingRhinoMock
    {
        [Test]
        public void Analyze_ShortFileName_LogsError()
        {
            var mocks = new MockRepository();
            var mockWebService = mocks.DynamicMock<IWebService>();

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

        [Test]
        public void Analyze_WhenExceptionThrown_EmailIsSent()
        {
            var mocks = new MockRepository();
            var stubWebService = mocks.Stub<IWebService>();
            var mockEmailSender = mocks.DynamicMock<IEmailSender>();

            using (mocks.Record())
            {
                stubWebService.LogError("Anything");
                LastCall.Constraints(Is.Anything());
                LastCall.Throw(new Exception("Fake Exception"));

                mockEmailSender.SendEmail("to", "subject", "Fake Exception");
            }

            var logAnalyzer = new LogAnalyzer
            {
                EmailSender = mockEmailSender, 
                WebService = stubWebService
            };

            logAnalyzer.Analyze("abc.txt");

            mocks.Verify(mockEmailSender);
        }
    }
}

