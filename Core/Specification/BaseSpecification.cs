using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> 
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
            Criteria=criteria;
        }

        public Expression<Func<T, bool>> Criteria {get;}
        public List<Expression<Func<T, object>>> Includes {get;}=
           new List<Expression<Func<T, object >>>();

        public Expression<Func<T, object>> OrderBy {get;private set;}

        public Expression<Func<T, object>> OrderByDescending {get;private set;}

        public int Take {get;private set;}

        public int Skip {get;private set;}

        public bool IsPaggingEnabled {get;private set;}

        protected void AddInculde(Expression<Func<T,object>> IncludeExpression)
           {
               Includes.Add(IncludeExpression);
           }
           protected void AddOrderBy(Expression<Func<T,object>> OrderByExprssion)
           {
               OrderBy = OrderByExprssion;
           }
            protected void AddOrderByDescending(Expression<Func<T,object>> OrderByDescExprssion)
           {
               OrderByDescending = OrderByDescExprssion;
           }
           protected void ApplyPaging(int skip ,int take)
           {
               Skip=skip;
               Take=take;
               IsPaggingEnabled=true;
           }

    }
}