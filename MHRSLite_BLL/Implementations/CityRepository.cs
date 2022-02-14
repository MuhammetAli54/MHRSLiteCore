using MHRSLite_BLL.Contracts;
using MHRSLite_DAL;
using MHRSLite_EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_BLL.Implementations
{
    public class CityRepository:Repository<City>,ICityRepository
    {

        public CityRepository(MyContext myContext):base(myContext)
        {

        }
    }
}
