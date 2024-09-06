using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        var faker = new Faker();

        return new RequestRegisterExpenseJson
        {
            Date = faker.Date.Past(),
            Title = faker.Commerce.Product(),
            PaymentType = faker.PickRandom<EPaymentType>(),
            Amouont = faker.Random.Decimal(min: 1, max: 1000),
            Description = faker.Commerce.ProductDescription()
        };
    }
}