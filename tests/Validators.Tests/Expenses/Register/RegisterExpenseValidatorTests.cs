using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Act - Executa a instância
        var result = validator.Validate(request);


        // Assert - Compare o resultado da requisição com o resultado que você estava esperando
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Title_Empty()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        request.Amouont = -1;


        // Act - Executa a instância
        var result = validator.Validate(request);


        // Assert - Compare o resultado da requisição com o resultado que você estava esperando
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Data_Future()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);


        // Act
        var result = validator.Validate(request);


        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessage.EXPENSES_CANNOT_FOR_THE_FUTURE));
    }

    [Fact]
    public void Error_Payment_Invalid()
    {
        // Arrange
        var validaor = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (EPaymentType)10;

        // Act
        var result = validaor.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessage.PAYMENT_TYPE_INVALID));



    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Error_Amount_Invalid(decimal amount)
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amouont = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}

