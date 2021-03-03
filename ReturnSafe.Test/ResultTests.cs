using NUnit.Framework;
using ReturnSafe.Result;
using static ReturnSafe.Result.Essentials;

namespace ReturnSafe.Test {
    public class ResultTests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void TestResultIsOk() {
            Result<string, string> resOk = Ok("Result");
            Result<string, string> resError = Error("Error");
            Assert.IsTrue(resOk.IsOk);
            Assert.IsFalse(resError.IsOk);
        }

        [Test]
        public void TestResultIf() {
            Result<string, string> resOk = Ok("Result");
            Result<string, string> resError = Error("Error");
            Assert.IsTrue(resOk);
            Assert.IsFalse(resError);
        }

        [Test]
        public void TestResultIsError() {
            Result<string, string> resOk = Ok("Result");
            Result<string, string> resError = Error("Error");
            Assert.IsFalse(resOk.IsError);
            Assert.IsTrue(resError.IsError);
        }

        [Test]
        public void TestResultUnwrap() {
            Result<string, string> resOk = Ok("Result");
            Result<string, string> resError = Error("Error");
            Assert.IsNotNull(resOk.Unwrap());
            Assert.IsTrue(resOk.Unwrap() == "Result");
            Assert.Throws(typeof(ReturnSafe.UnwrapException), delegate { resError.Unwrap(); });
        }

        [Test]
        public void TestResultExpect() {
            Result<string, string> resOk = Ok("Result");
            Result<string, string> resError = Error("Error");
            Assert.IsNotNull(resOk.Expect("Expect nuthin"));
            Assert.IsTrue(resOk.Expect("Expect nuthin") == "Result");
            ReturnSafe.UnwrapException exception = Assert.Throws<ReturnSafe.UnwrapException>( delegate { resError.Expect("Expect Exception"); });
            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(exception.Message == "Expect Exception");
        }
    }
}