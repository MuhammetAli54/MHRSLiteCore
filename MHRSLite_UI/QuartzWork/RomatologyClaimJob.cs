using MHRSLite_BLL.Contracts;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MHRSLite_EL.Enums;

namespace MHRSLite_UI.QuartzWork
{
    public class RomatologyClaimJob : IJob
    {
        private readonly ILogger<AppointmentStatusJob> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RomatologyClaimJob(ILogger<AppointmentStatusJob> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var date = DateTime.Now.AddMonths(-1);
                var appointment = _unitOfWork.AppointmentRepository.GetAppointmentsIM(date).OrderByDescending(x=> x.AppointmentDate).ToList();
                foreach (var item in appointment)
                {
                    //usera ait dahiliyeRomatoloji claimi yoksa eklenmeli
                    //yarın devam
                }
            }
            catch (Exception)
            {
                //loglanacak
            }
        }
    }
}
