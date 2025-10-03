
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { set; get; }
        public int Count { set; get; }
        public int Skip {set; get; }
        public bool IsPagination { get; set; }

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> cretiria)
        {
            Criteria= cretiria;
        }
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        { OrderByDesc = orderByDesc; }
        public void ApplyPagination(int skip,int take)
        {
         IsPagination = true;
            Take= take;
            Skip=skip;
        }

    }
}
