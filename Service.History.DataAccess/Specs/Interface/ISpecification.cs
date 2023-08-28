using Service.History.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.History.DataAccess.Specs.Interface
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        int? Offset { get; }
        int? Size { get; }
        Expression<Func<T, object>> SortOn { get; }
        SortDirection SortDirection { get; }
    }
}
