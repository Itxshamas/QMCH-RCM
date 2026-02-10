using QMCH.Models;

namespace QMCH.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetHRReportsAsync();
        Task<List<Report>> GetOperationReportsAsync();
    }
}
