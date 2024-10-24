using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.PayrollLoans;
using BemConsignado.HttpService.Domain.Proponents;
using BemConsignado.HttpService.Infrastructure;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BemConsignado.Tests
{
    public class CreatePayrollLoanHandlerTests
    {
        readonly IProponentRepository proponentRepository = Substitute.For<IProponentRepository>();
        readonly IPayrollLoanRepository payrollLoanRepository = Substitute.For<IPayrollLoanRepository>();
        readonly IAgentRepository agentRepository = Substitute.For<IAgentRepository>();
        readonly ICreditAgreementRepository creditAgreementRepository = Substitute.For<ICreditAgreementRepository>();
        readonly ICpfCheckerClient cpfCheckerClient = Substitute.For<ICpfCheckerClient>();

        [Fact]
        public async Task Handle_Should_Fail_When_Proponent_NotExists()
        {
            // Arrange            
            proponentRepository.GetAsync(Arg.Any<string>()).ReturnsNull();

            // Act
            var payrollLoan = await CreatePayrollLoanHandler().Handle(new CreatePayrollLoanCommand(), CancellationToken.None);

            // Assert
            Assert.True(payrollLoan.IsFailure);
            Assert.Contains("Proponente não foi encontrado com esse CPF: ", payrollLoan.Error);
        }

        [Fact]
        public async Task Handle_Should_Fail_When_CreditAgreement_Unavailable()
        {
            // Arrange            
            proponentRepository.GetAsync(Arg.Any<string>()).Returns(CreateProponent());
            creditAgreementRepository.GetAsync(Arg.Any<string>()).ReturnsNull();

            // Act
            var payrollLoan = await CreatePayrollLoanHandler().Handle(new CreatePayrollLoanCommand(), CancellationToken.None);

            // Assert
            Assert.True(payrollLoan.IsFailure);
            Assert.Contains("Não foi encontrado convênio com esse código ", payrollLoan.Error);
        }

        [Fact]
        public async Task Handle_Should_Fail_When_Cpf_IsInactive()
        {
            // Arrange            
            agentRepository.GetAsync(Arg.Any<string>()).Returns(new Agent() { IsActive = true });
            proponentRepository.GetAsync(Arg.Any<string>()).Returns(CreateProponent());
            creditAgreementRepository.GetAsync(Arg.Any<string>()).Returns(CreateCreditAgreement());
            cpfCheckerClient.IsActive(Arg.Any<string>()).Returns(false);

            // Act
            var payrollLoan = await CreatePayrollLoanHandler().Handle(new CreatePayrollLoanCommand(), CancellationToken.None);

            // Assert
            Assert.True(payrollLoan.IsFailure);
            Assert.Contains("O CPF do proponente informado está bloqueado ", payrollLoan.Error);
        }

        CreatePayrollLoanHandler CreatePayrollLoanHandler()
        {
            return new CreatePayrollLoanHandler(proponentRepository, creditAgreementRepository, agentRepository, payrollLoanRepository, cpfCheckerClient);
        }

        Proponent CreateProponent()
        {
            return new Proponent()
            {
                Name = "Name1",
                Cpf = "02277982016",
                Address = "Address1",
                PhoneNumber = "51999999",
                State = "RS",
                Email = "Email1",
                Income = 50000,
                BirthDate = DateTime.Now.AddYears(-35),
                PayrollLoans = []
            };
        }

        CreditAgreement CreateCreditAgreement()
        {
            return new CreditAgreement()
            {
                MaxLoanAmount = 5000,
                Code= "Partner1",
                State = "RS"
            };
        }
    }
}