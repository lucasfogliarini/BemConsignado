using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.PayrollLoans;
using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using BemConsignado.HttpService.Domain.Proponents;

namespace BemConsignado.Tests
{
    public class PayrollLoanTests
    {
        [Theory]
        [InlineData(true, PayrollLoanStatus.Closed, 50000, 50000, false, 12.1)]
        [InlineData(true, PayrollLoanStatus.Closed, 50000, 50000, true, 6)]
        [InlineData(true, PayrollLoanStatus.Open, 50000, 50000, false, 6)]
        [InlineData(false, PayrollLoanStatus.Closed, 50000, 50000, false, 6)]
        [InlineData(true, PayrollLoanStatus.Closed, 55000, 50000, false, 6)]
        public void CreatePayrollLoan_Should_Fail_When(bool agentActive, PayrollLoanStatus payrollLoanStatus, decimal credit, decimal maxLoanAmount, bool refinancing, int installments)
        {
            // Arrange
            var payrollLoanInput = new PayrollLoanInput
            {
                Proponent = new Proponent
                {
                    Name = "Name1",
                    Cpf = "02277982016",
                    Address = "Address1",
                    PhoneNumber = "PhoneNumber1",
                    State = "RS",
                    BirthDate = DateTime.Now.AddYears(-79),
                    PayrollLoans =
                    [
                        new PayrollLoan
                        {
                            Status = payrollLoanStatus
                        }
                    ]
                },
                CreditAgreement = new CreditAgreement
                {
                    MaxLoanAmount = maxLoanAmount,
                    State = "RS",
                    Code = "Partner1",
                    AcceptRefinancing = false
                },
                Agent = new Agent
                {
                    IsActive = agentActive,
                },
                Installments = installments,
                Credit = credit,
                Refinancing = refinancing,
            };

            // Act
            var payrollLoan = PayrollLoan.Create(payrollLoanInput);

            // Assert
            Assert.True(payrollLoan.IsFailure);
        }

        [Fact]
        public void CreatePayrollLoan_Should_Sucess_When()
        {
            // Arrange
            var payrollLoanInput = new PayrollLoanInput
            {
                Proponent = new Proponent
                {
                    Name = "Name1",
                    Cpf = "02277982016",
                    Address = "Address1",
                    PhoneNumber = "PhoneNumber1",
                    State = "RS",
                    BirthDate = DateTime.Now.AddYears(-79),
                    PayrollLoans = []
                },
                CreditAgreement = new CreditAgreement
                {
                    MaxLoanAmount = 600000,
                    State = "RS",
                    Code = "Partner1"
                },
                Agent = new Agent
                {
                    IsActive = true,
                },
                Installments = 11,
                Credit = 50000,
            };

            // Act
            var payrollLoan = PayrollLoan.Create(payrollLoanInput);

            // Assert
            Assert.True(payrollLoan.IsSuccess);
        }
    }
}