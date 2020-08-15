using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Wpf._Test
{
    public class STATestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var testResults = default(TestResult[]);
            void testExecuter()
            {
                testResults = base.Execute(testMethod);
            }

            var staThread = new Thread(testExecuter);
            staThread.Name = "STATestMethodAttributeThread";
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            return testResults;
        }
    }
}
