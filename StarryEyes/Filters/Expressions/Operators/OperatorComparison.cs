﻿using System;
using System.Collections.Generic;
using StarryEyes.Anomaly.TwitterApi.DataModels;

namespace StarryEyes.Filters.Expressions.Operators
{
    public abstract class FilterComparisonBase : FilterTwoValueOperator
    {
        public sealed override IEnumerable<FilterExpressionType> SupportedTypes
        {
            get { yield return FilterExpressionType.Boolean; }
        }

        public sealed override Func<TwitterStatus, bool> GetBooleanValueProvider()
        {
            var l = LeftValue.GetNumericValueProvider();
            var r = RightValue.GetNumericValueProvider();
            return this.BuildEvaluator(l, r);
        }

        public override string GetBooleanSqlQuery()
        {
            return this.BuildEvaluatorSql(LeftValue.GetNumericSqlQuery(), RightValue.GetNumericSqlQuery());
        }

        protected abstract Func<TwitterStatus, bool> BuildEvaluator(Func<TwitterStatus, long> left, Func<TwitterStatus, long> right);

        protected abstract string BuildEvaluatorSql(string left, string right);
    }

    public class FilterOperatorLessThan : FilterComparisonBase
    {
        protected override Func<TwitterStatus, bool> BuildEvaluator(Func<TwitterStatus, long> left, Func<TwitterStatus, long> right)
        {
            return _ => left(_) < right(_);
        }

        protected override string BuildEvaluatorSql(string left, string right)
        {
            return left + " < " + right;
        }

        protected override string OperatorString
        {
            get { return "<"; }
        }
    }

    public class FilterOperatorLessThanOrEqual : FilterComparisonBase
    {
        protected override Func<TwitterStatus, bool> BuildEvaluator(Func<TwitterStatus, long> left, Func<TwitterStatus, long> right)
        {
            return _ => left(_) <= right(_);
        }

        protected override string BuildEvaluatorSql(string left, string right)
        {
            return left + " <= " + right;
        }

        protected override string OperatorString
        {
            get { return "<="; }
        }
    }

    public class FilterOperatorGreaterThan : FilterComparisonBase
    {
        protected override Func<TwitterStatus, bool> BuildEvaluator(Func<TwitterStatus, long> left, Func<TwitterStatus, long> right)
        {
            return _ => left(_) > right(_);
        }

        protected override string BuildEvaluatorSql(string left, string right)
        {
            return left + " > " + right;
        }

        protected override string OperatorString
        {
            get { return ">"; }
        }
    }

    public class FilterOperatorGreaterThanOrEqual : FilterComparisonBase
    {
        protected override Func<TwitterStatus, bool> BuildEvaluator(Func<TwitterStatus, long> left, Func<TwitterStatus, long> right)
        {
            return _ => left(_) >= right(_);
        }

        protected override string BuildEvaluatorSql(string left, string right)
        {
            return left + " >= " + right;
        }

        protected override string OperatorString
        {
            get { return ">="; }
        }
    }
}
