using Med.Api.Data;
using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Med.Api.Handlers;

public sealed class MedicineHandler(AppDbContext _context) : IMedicineHandler
{

    public async Task<Response<Medicine>> CreateAsync(CreateMedicineRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Response<Medicine>.Fail("Nome do medicamento é obrigatório");

        var medicine = new Medicine
        {
            Name = request.Name,
            Time = TimeOnly.FromTimeSpan(request.Time),
            UserId = request.UserId,
            Taken = false
        };

        _context.Medicines.Add(medicine);
        await _context.SaveChangesAsync();

        return Response<Medicine>.Ok(medicine);
    }

    public async Task<Response<Medicine?>> GetByIdAsync(GetMedicineByIdRequest request)
    {
        var medicine = await _context.Medicines
            .AsNoTracking()
            .FirstOrDefaultAsync(m =>
                m.Id == request.Id &&
                m.UserId == request.UserId);

        if (medicine == null)
            return Response<Medicine?>.Fail("Medicamento nao encontrado");

        return Response<Medicine?>.Ok(medicine);
    }

    public async Task<Response<IEnumerable<Medicine>>> GetByUserAsync(
       GetMedicinesByUserRequest request)
    {
        var medicines = await _context.Medicines
            .AsNoTracking()
            .Where(m => m.UserId == request.UserId)
            .OrderBy(m => m.Time)
            .ToListAsync();

        return Response<IEnumerable<Medicine>>.Ok(medicines);
    }

    public async Task<Response<IEnumerable<Medicine>>> SearchByNameAsync(
       SearchMedicineByNameRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Response<IEnumerable<Medicine>>
                .Fail("Termo de busca inválido");

        var medicines = await _context.Medicines
            .AsNoTracking()
            .Where(m =>
                m.UserId == request.UserId &&
                m.Name.Contains(request.Name))
            .OrderBy(m => m.Time)
            .ToListAsync();

        if (medicines is null || !medicines.Any())
            return Response<IEnumerable<Medicine>>.Fail("Nenhum medicamento encontrado");


        return Response<IEnumerable<Medicine>>.Ok(medicines);
    }
    public async Task<Response<Medicine>> MarkAsTakenAsync(
     MarkMedicineAsTakenRequest request)
    {
        var medicine = await _context.Medicines
            .FirstOrDefaultAsync(m =>
                m.Id == request.Id &&
                m.UserId == request.UserId);

        if (medicine is null)
            return Response<Medicine>.Fail("Medicamento não encontrado");

        if (medicine.Taken)
            return Response<Medicine>.Fail("Medicamento já foi marcado como tomado");

        var history = new MedicineHistory
        {
            MedicineId = medicine.Id,
            UserId = medicine.UserId,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Time = TimeOnly.FromDateTime(DateTime.Now)
        };
        _context.MedicineHistories.Add(history);

        medicine.Taken = true;
        await _context.SaveChangesAsync();
        return Response<Medicine>.Ok(medicine);
    }

    public async Task<Response<bool>> DeleteAsync(DeleteMedicineRequest request)
    {
        var medicine = await _context.Medicines
            .FirstOrDefaultAsync(m =>
                m.Id == request.Id &&
                m.UserId == request.UserId);

        if (medicine is null)
            return Response<bool>.Fail("Medicamento não encontrado");

        _context.Medicines.Remove(medicine);
        await _context.SaveChangesAsync();

        return Response<bool>.Ok(true);
    }
}
