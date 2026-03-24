using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;

namespace Med.Core.Handlers
{
    public interface IMedicineHandler
    {
        Task<Response<Medicine>> CreateAsync(CreateMedicineRequest request);

        Task<Response<Medicine?>> GetByIdAsync(GetMedicineByIdRequest request);

        Task<Response<IEnumerable<Medicine>>> GetByUserAsync(GetMedicinesByUserRequest request);

        Task<Response<IEnumerable<Medicine>>> SearchByNameAsync(SearchMedicineByNameRequest request);

        Task<Response<Medicine>> MarkAsTakenAsync(MarkMedicineAsTakenRequest request);

        Task<Response<bool>> DeleteAsync(DeleteMedicineRequest request);
    }
}
