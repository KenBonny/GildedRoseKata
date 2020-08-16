using System;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace KenBonny.GildedRoseKata
{
    [UseReporter(typeof(DiffReporter))]
    public class ApprovalTests
    {
        [Fact]
        public void ThirtyDays()
        {
            StringBuilder fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));
            Console.SetIn(new StringReader("a\n"));

            Program.Main();
            var output = fakeoutput.ToString();

            Approvals.Verify(output);
        }
    }
}