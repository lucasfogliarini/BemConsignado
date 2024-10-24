using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.CreditProposals;
using BemConsignado.HttpService.Domain.Proponents;
using BemConsignado.HttpService.Infrastructure;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BemConsignado.Tests
{
    public class CreateCreditProposalHandlerTests
    {
        readonly IProponentRepository proponentRepository = Substitute.For<IProponentRepository>();
        readonly ICreditProposalRepository creditProposalRepository = Substitute.For<ICreditProposalRepository>();
        readonly ICreditAgreementRepository creditAgreementRepository = Substitute.For<ICreditAgreementRepository>();
        readonly ICpfCheckerClient cpfCheckerClient = Substitute.For<ICpfCheckerClient>();

        [Fact]
        public async Task Handle_Should_Fail_When_Proponent_NotExists()
        {
            // Arrange            
            proponentRepository.GetAsync(Arg.Any<string>()).ReturnsNull();

            // Act
            var creditProposal = await CreateCreditProposalHandler().Handle(new CreateCreditProposalCommand(), CancellationToken.None);

            // Assert
            Assert.True(creditProposal.IsFailure);
            Assert.Contains("Proponente não foi encontrado com esse CPF: ", creditProposal.Error);
        }

        [Fact]
        public async Task Handle_Should_Fail_When_CreditAgreement_Unavailable()
        {
            // Arrange            
            proponentRepository.GetAsync(Arg.Any<string>()).Returns(CreateProponent());
            creditAgreementRepository.GetAsync(Arg.Any<string>()).ReturnsNull();

            // Act
            var creditProposal = await CreateCreditProposalHandler().Handle(new CreateCreditProposalCommand(), CancellationToken.None);

            // Assert
            Assert.True(creditProposal.IsFailure);
            Assert.Contains("Não foi encontrado convênio com esse código ", creditProposal.Error);
        }

        [Fact]
        public async Task Handle_Should_Fail_When_Cpf_IsInactive()
        {
            // Arrange            
            proponentRepository.GetAsync(Arg.Any<string>()).Returns(CreateProponent());
            creditAgreementRepository.GetAsync(Arg.Any<string>()).Returns(CreateCreditAgreement());
            cpfCheckerClient.IsActive(Arg.Any<string>()).Returns(false);

            // Act
            var creditProposal = await CreateCreditProposalHandler().Handle(new CreateCreditProposalCommand(), CancellationToken.None);

            // Assert
            Assert.True(creditProposal.IsFailure);
            Assert.Contains("O CPF informado está bloqueado ", creditProposal.Error);
        }

        CreateCreditProposalHandler CreateCreditProposalHandler()
        {
            return new CreateCreditProposalHandler(proponentRepository, creditAgreementRepository, creditProposalRepository, cpfCheckerClient);
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
                IsActive = true,
                BirthDate = DateTime.Now.AddYears(-35),
                Proposals = []
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