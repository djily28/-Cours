using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class ConsultantRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Consultant>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Consultants.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Consultants.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Consultant>> GetAll()
        {
            var consultants = await appDbContext.Consultants.AsNoTracking().Include(t => t.Town).ThenInclude(b => b.City).ThenInclude(c => c.Country).Include(b => b.Branch)
            .ThenInclude(d => d.Department).ToListAsync();
            return consultants!;
        }
        public async Task<Consultant> GetById(int id)
        {
            var consultants = await appDbContext.Consultants
                .Include(t => t.Town)
                .ThenInclude(b => b.City)
                .ThenInclude(c => c.Country)
                .Include(b => b.Branch)
                .ThenInclude(d => d.Department)
                .FirstOrDefaultAsync(ei => ei.Id == id)!;
            return consultants!;
        }

        public async Task<GeneralResponse> Insert(Consultant item)
        {
            if (!await CheckName(item.FileNumber!)) return new GeneralResponse(false, "Consultant already added");

            appDbContext.Consultants.Add(item);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(Consultant Consultant)
        {
            var findUser = await appDbContext.Consultants.FirstOrDefaultAsync(e=>e.Id== Consultant.Id);
            if (findUser is null) return new GeneralResponse(false, "Consultant does not exist");

            findUser.FirstName = Consultant.FirstName;
            findUser.LastName = Consultant.LastName;
            findUser.Other = Consultant.Other;
            findUser.Address = Consultant.Address;
            findUser.TelephoneNumber = Consultant.TelephoneNumber;
            findUser.BranchId = Consultant.BranchId;
            findUser.TownId = Consultant.TownId;
            findUser.CivilId = Consultant.CivilId;
            findUser.FileNumber = Consultant.FileNumber;
            findUser.JobName = Consultant.JobName;
            findUser.Salary = Consultant.Salary;
            findUser.StartDate = Consultant.StartDate;
            findUser.EndDate = Consultant.EndDate;
            findUser.Photo = Consultant.Photo;
            await appDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry branch not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Consultants.FirstOrDefaultAsync(x => x.FileNumber!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
