using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Enums
{
    public class AllEnums
    {
    }
    public enum Genders:byte
    {
        Belirtilmemis,
        Bay,
        Bayan
    }
    public enum RoleNames:byte
    {
        Passive,
        Admin,
        Patient,
        PassiveDoctor,
        ActiveDoctor
    }

    public enum AppointmentStatus:byte
    {
        Active=1,
        Pass=0,
        Cancelled=2
    }

}
