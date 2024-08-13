using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] RequestRegisterExpenseJson request)
    {
        var useCase = new RegisterExpenseUseCase().Execute(request);

        return Created(string.Empty, useCase);
    }
}
