using DDAZ32_gyar.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDAZ32_gyar.Entities
{
    public class BallFactory : IToyFactory
    {
         public Toy CreateNew()
         {
             return new Ball();
         }
        
    }
}
