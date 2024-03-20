using PageMonitor.Application.Services.ResultRuleCheckers.PropertyChecker;
using PageMonitor.Domain.Entities;
using PageMonitor.Domain.Enums;

namespace PageMonitor.Application.Tests.ResultRuleCheckers
{
    public class StatusCodeCheckerTests
    {
        private StatusCodeChecker _checker = new StatusCodeChecker();

        [Theory]
        [InlineData(ResultPropertyEnum.StatusCode)]
        public void CanProcess(ResultPropertyEnum property)
        {
            var rule = new ResultRule()
            {
                Operator = ResultPropertyCompareOperatorEnum.Equal,
                Property = property,
                Value = "",
            };

            var result = _checker.CanHandle(rule);

            Assert.True(result);
        }

        [Theory]
        [InlineData(ResultPropertyEnum.Content)]
        [InlineData(ResultPropertyEnum.ResponseTime)]
        public void CanNotProcess(ResultPropertyEnum property)
        {
            var rule = new ResultRule()
            {
                Operator = ResultPropertyCompareOperatorEnum.Equal,
                Property = property,
                Value = "",
            };

            var result = _checker.CanHandle(rule);

            Assert.False(result);
        }

        [Theory]
        [InlineData(200, "200")]
        [InlineData(300, "300")]
        public void IsEqual(int dataValue, string ruleValue)
        {
            var rule = new ResultRule()
            {
                Operator = ResultPropertyCompareOperatorEnum.Equal,
                Property = ResultPropertyEnum.StatusCode,
                Value = ruleValue,
            };

            var resultData = new ResultData()
            {
                StatusCode = dataValue,
                Content = "",
                ResponseTime = TimeSpan.FromSeconds(1)
            };

            var result = _checker.Check(rule, resultData);

            Assert.True(result);
        }

        [Theory]
        [InlineData(200, "304")]
        [InlineData(304, "404")]
        public void IsNotEqual(int dataValue, string ruleValue)
        {
            var rule = new ResultRule()
            {
                Operator = ResultPropertyCompareOperatorEnum.NotEqual,
                Property = ResultPropertyEnum.StatusCode,
                Value = ruleValue,
            };

            var resultData = new ResultData()
            {
                StatusCode = dataValue,
                Content = "",
                ResponseTime = TimeSpan.FromSeconds(1)
            };

            var result = _checker.Check(rule, resultData);
            Assert.True(result);
        }

        [Theory]
        [InlineData(ResultPropertyCompareOperatorEnum.GreaterThan)]
        [InlineData(ResultPropertyCompareOperatorEnum.LessThan)]
        [InlineData(ResultPropertyCompareOperatorEnum.Contains)]
        public void OperatorNotSupported(ResultPropertyCompareOperatorEnum op)
        {
            var rule = new ResultRule()
            {
                Operator = op,
                Property = ResultPropertyEnum.StatusCode,
                Value = "100",
            };

            var resultData = new ResultData()
            {
                StatusCode = 100,
                Content = "",
                ResponseTime = TimeSpan.FromSeconds(1)
            };

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var result = _checker.Check(rule, resultData);
            });

            Assert.Equal("Invalid operator", ex.Message);
        }

        [Theory]
        [InlineData(300, "qw")]
        [InlineData(200, null)]
        [InlineData(223, "")]
        public void ParseError(int dataValue, string ruleValue)
        {
            var rule = new ResultRule()
            {
                Operator = ResultPropertyCompareOperatorEnum.Equal,
                Property = ResultPropertyEnum.StatusCode,
                Value = ruleValue,
            };

            var resultData = new ResultData()
            {
                StatusCode = dataValue,
                Content = "",
                ResponseTime = TimeSpan.FromSeconds(1)
            };

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var result = _checker.Check(rule, resultData);
            });

            Assert.Contains("Invalid status code", ex.Message);
        }
    }
}