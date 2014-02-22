using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MocksTesting
{
    public class LogAnalyzer
    {
        public IWebService WebService { get; set; }

        public void Analyze(string fileName)
        {
            if(fileName.Length < 8)
            {
                WebService.LogError("Short FileName:" + fileName);
            }
        }
    }

    public interface IWebService
    {
        void LogError(string errorMessage);
    }
}
