using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.History.Infrastructure.Dto
{
    public class BaseDto<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;

        public DateTime CreatedDate { get; set; }
    }
}
