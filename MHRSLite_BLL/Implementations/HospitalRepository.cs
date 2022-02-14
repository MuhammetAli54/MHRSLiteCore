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
   public class HospitalRepository: Repository<Hospital>, IHospitalRepository
    {
        private readonly MyContext _myContext;

        public HospitalRepository(MyContext myContext):base(myContext)
        {
            _myContext = myContext;
        }
    }
}
