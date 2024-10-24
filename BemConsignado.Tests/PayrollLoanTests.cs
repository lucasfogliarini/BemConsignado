using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.PayrollLoans;
using BemConsignado.HttpService.Domain.Proponents;

namespace BemConsignado.Tests
{
    public class PayrollLoanTests
    {
        [Theory]
        [InlineData(true, PayrollLoanStatus.Closed, 50000, 50000, 12.1)]
        [InlineData(true, PayrollLoanStatus.Open, 50000, 50000, 6)]
        [InlineData(false, PayrollLoanStatus.Closed, 50000, 50000, 6)]
        [InlineData(true, PayrollLoanStatus.Closed, 55000, 50000, 6)]
        public void CreatePayrollLoan_Should_Fail_When(bool proponentActive, PayrollLoanStatus payrollLoanStatus, decimal credit, decimal maxLoanAmount, int installments)
        {
            // Arrange
            var proponent = new Proponent
            {
                Name = "Name1",
                Cpf = "02277982016",
                Address = "Address1",
                PhoneNumber = "PhoneNumber1",
                State = "RS",
                BirthDate = DateTime.Now.AddYears(-79),
                IsActive = proponentActive,
                PayrollLoans =
                [
                    new PayrollLoan
                    {
                        Status = payrollLoanStatus
                    }
                ]
            };
            var creditAgreement = new CreditAgreement
            {
                MaxLoanAmount = maxLoanAmount,
                State = "RS",
                Code = "Partner1"
            };

            // Act
            var payrollLoan = PayrollLoan.Create(proponent, creditAgreement, credit, installments);

            // Assert
            Assert.True(payrollLoan.IsFailure);
        }

        [Fact]
        public void CreatePayrollLoan_Should_Sucess_When()
        {
            // Arrange
            var proponent = new Proponent
            {
                Name = "Name1",
                Cpf = "02277982016",
                Address = "Address1",
                PhoneNumber = "PhoneNumber1",
                State = "RS",
                BirthDate = DateTime.Now.AddYears(-79),
                IsActive = true,
                PayrollLoans =
                [
                    new PayrollLoan
                    {
                        Status = PayrollLoanStatus.Closed
                    }
                ]
            };
            var creditAgreement = new CreditAgreement
            {
                MaxLoanAmount = 5000,
                State = "RS",
                Code = "Partner1"
            };
            decimal credit = 5000;
            int installments = 11;

            // Act
            var payrollLoan = PayrollLoan.Create(proponent, creditAgreement, credit, installments);

            // Assert
            Assert.True(payrollLoan.IsSuccess);
        }
    }
}