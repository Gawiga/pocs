using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async
{
    //generic constraints
    public class generic_constraints<T, V>
        where T : class
        where V : ITipo
    { }

    public class teste
    {
        generic_constraints<string, Tipo> handle = new generic_constraints<string, Tipo>();
    }

    public interface ITipo { }
    public class Tipo : ITipo { }
}
