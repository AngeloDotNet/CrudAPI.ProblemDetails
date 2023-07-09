using CustomLibrary.EFCore.EFCore.Infrastructure.Interfaces;
using Sample.API.DataAccessLayer.Entity;

namespace Sample.API.BusinessLayer.Service;

public class PeopleService : IPeopleService
{
    private readonly IUnitOfWork<PersonEntity, Guid> unitOfWork;

    public PeopleService(IUnitOfWork<PersonEntity, Guid> unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<List<PersonEntity>> GetListItemAsync()
    {
        return await unitOfWork.ReadOnly.GetAllAsync();
    }

    public async Task<PersonEntity> GetItemAsync(Guid id)
    {
        return await unitOfWork.ReadOnly.GetByIdAsync(id);
    }

    public async Task CreateItemAsync(PersonEntity item)
    {
        await unitOfWork.Command.CreateAsync(item);
    }

    public async Task UpdateItemAsync(PersonEntity item)
    {
        await unitOfWork.Command.UpdateAsync(item);
    }

    public async Task DeleteItemAsync(PersonEntity item)
    {
        await unitOfWork.Command.DeleteAsync(item);
    }
}