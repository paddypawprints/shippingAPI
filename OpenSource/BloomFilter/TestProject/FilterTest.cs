using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using BloomFilter;
namespace TestProject
{
    [TestClass()]
    public class FilterTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        /// There should be no false negatives.
        /// </summary>
        [TestMethod()]
        public void NoFalseNegativesTest()
        {
            // set filter properties
            int capacity = 10000;
            float errorRate = 0.001F; // 0.1%

            // create input collection
            List<string> inputs = generateRandomDataList(capacity);

            // instantiate filter and populate it with the inputs
            Filter<string> target = new Filter<string>(capacity, errorRate, null);
            foreach (string input in inputs)
            {
                target.Add(input);
            }

            // check for each input. if any are missing, the test failed
            foreach (string input in inputs)
            {
                if (target.Contains(input) == false)
                    Assert.Fail("False negative: {0}", input);
            }
        }

        /// <summary>
        /// Only in extreme cases should there be a false positive with this test.
        /// </summary>
        [TestMethod()]
        public void LowProbabilityFalseTest()
        {
            int capacity = 10000; // we'll actually add only one item
            float errorRate = 0.0001F; // 0.01%

            // instantiate filter and populate it with a single random value
            Filter<string> target = new Filter<string>(capacity, errorRate, null);
            target.Add(Guid.NewGuid().ToString());

            // generate a new random value and check for it
            if (target.Contains(Guid.NewGuid().ToString()) == true)
                Assert.Fail("Check for missing item returned true.");
        }

        [TestMethod()]
        public void FalsePositivesInRangeTest()
        {
            // set filter properties
            int capacity = 1000000;
            float errorRate = 0.001F; // 0.1%

            // instantiate filter and populate it with random strings
            Filter<string> target = new Filter<string>(capacity, errorRate, null);
            for (int i = 0; i < capacity; i++)
                target.Add(Guid.NewGuid().ToString());

            // generate new random strings and check for them
            // about errorRate of them should return positive
            int falsePositives = 0;
            int testIterations = capacity;
            int expectedFalsePositives = ((int)(testIterations * errorRate)) * 2;
            for (int i = 0; i < testIterations; i++)
            {
                string test = Guid.NewGuid().ToString();
                if (target.Contains(test) == true)
                    falsePositives++;
            }

            if (falsePositives > expectedFalsePositives)
                Assert.Fail("Number of false positives ({0}) greater than expected ({1}).", falsePositives, expectedFalsePositives);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OverLargeInputTest()
        {
            // set filter properties
            int capacity = int.MaxValue - 1;
            float errorRate = 0.01F; // 1%

            // instantiate filter
            Filter<string> target = new Filter<string>(capacity, errorRate, null);
        }

        [TestMethod()]
        public void LargeInputTest()
        {
            // set filter properties
            int capacity = 2000000;
            float errorRate = 0.01F; // 1%

            // instantiate filter and populate it with random strings
            Filter<string> target = new Filter<string>(capacity, errorRate, null);
            for (int i = 0; i < capacity; i++)
                target.Add(Guid.NewGuid().ToString());

            // if it didn't error out on that much input, this test succeeded
        }

        [TestMethod()]
        public void LargeInputTestAutoError()
        {
            // set filter properties
            int capacity = 2000000;

            // instantiate filter and populate it with random strings
            Filter<string> target = new Filter<string>(capacity);
            for (int i = 0; i < capacity; i++)
                target.Add(Guid.NewGuid().ToString());

            // if it didn't error out on that much input, this test succeeded
        }

        /// <summary>
        /// If k and m are properly choses for n and the error rate, the filter should be about half full.
        /// </summary>
        [TestMethod()]
        public void TruthinessTest()
        {
            int capacity = 10000;
            float errorRate = 0.001F; // 0.1%
            Filter<string> target = new Filter<string>(capacity, errorRate, null);
            for (int i = 0; i < capacity; i++)
                target.Add(Guid.NewGuid().ToString());

            double actual = target.Truthiness;
            double expected = 0.5;
            double threshold = 0.01; // filter shouldn't be < 49% or > 51% "true"
            Assert.IsTrue(Math.Abs(actual - expected) < threshold, "Information density too high or low. Actual={0}, Expected={1}", actual, expected);
        }

        private static List<String> generateRandomDataList(int capacity)
        {
            List<String> inputs = new List<string>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                inputs.Add(Guid.NewGuid().ToString());
            }
            return inputs;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidCapacityConstructorTest()
        {
            float errorRate = 0.1F;
            int capacity = 0; // no good
            Filter<int> target = new Filter<int>(capacity, errorRate, delegate(int input) { return 0; });
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidErrorRateConstructorTest()
        {
            float errorRate = 10F; // no good
            int capacity = 10;
            Filter<int> target = new Filter<int>(capacity, errorRate, delegate(int input) { return 0; });
        }
    }
}
