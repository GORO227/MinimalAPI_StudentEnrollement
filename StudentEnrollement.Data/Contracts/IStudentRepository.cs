using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollement.Data.Contracts
{
    public interface IStudentRepository : IGenericRepository<Stutent>
    {
        Task<Stutent> GetStudentDetails(int studentId);
    }
}
