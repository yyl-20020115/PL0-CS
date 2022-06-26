using PL0;

namespace PL0_CS_Tests
{
    [TestClass]
    public class UnitTest1
    {
        public static string[] Paths = 
        {
            "../Examples/primes.pl0",
            "../Examples/procedures.pl0",
            "../Examples/squares.pl0",
            "../Examples/BenchMark.pl0"
        };
        [TestMethod]
        public void TestExamples()
        {
            foreach(var name in Paths)
            try
            {
                Assert.AreEqual(0,
                    ProgramEntry.Main(new string[] { name }));

            }catch(Exception e)
            {
                Assert.Fail(e.ToString()); 
            }
        }
    }
}