﻿using MHRSLite_BLL.Contracts;
using MHRSLite_DAL;
using MHRSLite_EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_BLL.Implementations
{
   public class PatientRepository:Repository<Patient>, IPatientRepository
    {
        public PatientRepository(MyContext myContext):base(myContext)
        {

        }
    }
}
