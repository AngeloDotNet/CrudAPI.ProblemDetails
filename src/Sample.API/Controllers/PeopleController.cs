using Sample.API.BusinessLayer.Logging;
using Sample.API.BusinessLayer.Service;
using Sample.API.BusinessLayer.Validation;
using Sample.API.DataAccessLayer.Entity;
using Sample.API.Shared.CustomResponse;
using Sample.API.Shared.Exception;

namespace Sample.API.Controllers;

public class PeopleController : BaseController
{
    private readonly IPeopleService peopleService;

    public PeopleController(IPeopleService peopleService)
    {
        this.peopleService = peopleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        try
        {
            var people = await peopleService.GetListItemAsync();

            if (people.Count == 0)
            {
                LoggerService.GetLogger<PeopleController>().LogWarning("The people list is empty");
                throw new NotFoundException($"The people list is empty");
            }

            return Ok(new ConfirmResponse(people));
        }
        catch (NotFoundException exc)
        {
            return ExceptionResponse.NotFoundDetails(HttpContext, exc);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(Guid id)
    {
        try
        {
            var person = await peopleService.GetItemAsync(id);

            if (person == null)
            {
                LoggerService.GetLogger<PeopleController>().LogWarning($"Person with id {id} not found");
                throw new NotFoundException($"Person with id {id} not found");
            }

            return Ok(new ConfirmResponse(person));
        }
        catch (NotFoundException exc)
        {
            return ExceptionResponse.NotFoundDetails(HttpContext, exc);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonEntity person, [FromServices] IValidator<PersonEntity> validator)
    {
        try
        {
            await ValidationService.ValidationEntity(person, validator);

            if (await peopleService.GetItemAsync(person.Id) != null)
            {
                LoggerService.GetLogger<PeopleController>().LogWarning($"Person with id {person.Id} already exists");
                throw new ConflictException($"Person with id {person.Id} already exists");
            }

            await peopleService.CreateItemAsync(person);

            LoggerService.GetLogger<PeopleController>().LogInformation($"Create person with id {person.Id}");
            return Ok(new ConfirmResponse(person));
        }
        catch (ValidationException exc)
        {
            LoggerService.GetLogger<PeopleController>().LogError($"Validation not valid");
            return ExceptionResponse.UnprocessableEntityDetails(HttpContext, exc);
        }
        catch (ConflictException exc)
        {
            return ExceptionResponse.ConflictDetails(HttpContext, exc);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson([FromBody] PersonEntity person, [FromServices] IValidator<PersonEntity> validator)
    {
        await ValidationService.ValidationEntity(person, validator);

        await peopleService.UpdateItemAsync(person);

        LoggerService.GetLogger<PeopleController>().LogInformation($"Update person with id {person.Id}");
        return Ok(new ConfirmResponse($"Update person with id {person.Id}"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        try
        {
            var person = await peopleService.GetItemAsync(id);

            if (person == null)
            {
                LoggerService.GetLogger<PeopleController>().LogWarning($"Person with id {id} not found");
                throw new NotFoundException($"Person with id {id} not found");
            }

            await peopleService.DeleteItemAsync(person);

            LoggerService.GetLogger<PeopleController>().LogInformation($"Delete person with id {person.Id}");
            return Ok(new ConfirmResponse($"Delete person with id {person.Id}"));
        }
        catch (NotFoundException exc)
        {
            return ExceptionResponse.NotFoundDetails(HttpContext, exc);
        }
    }
}