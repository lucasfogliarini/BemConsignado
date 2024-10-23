using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.CreditProposals;
using BemConsignado.HttpService.Domain.Proponents;

namespace BemConsignado.Tests
{
    public class CreditProposalTests
    {
        [Theory]
        [InlineData(true, CreditProposalStatus.Closed, 12.1)]
        [InlineData(true, CreditProposalStatus.Open, 6)]
        [InlineData(false, CreditProposalStatus.Closed, 6)]
        public void CreateProposal_Should_Fail_When(bool proponentActive, CreditProposalStatus creditProposalStatus, int installments)
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
                Proposals =
                [
                    new CreditProposal
                    {
                        Status = creditProposalStatus
                    }
                ]
            };
            var creditAgreement = new CreditAgreement
            {
                MaxLoanAmount = 5000,
                State = "RS",
                PartnerName = "Partner1"
            };
            decimal credit = 5000;

            // Act
            var creditProposal = CreditProposal.Create(proponent, creditAgreement, credit, installments);

            // Assert
            Assert.True(creditProposal.IsFailure);
        }

        [Fact]
        public void CreateProposal_Should_Sucess_When()
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
                Proposals =
                [
                    new CreditProposal
                    {
                        Status = CreditProposalStatus.Closed
                    }
                ]
            };
            var creditAgreement = new CreditAgreement
            {
                MaxLoanAmount = 5000,
                State = "RS",
                PartnerName = "Partner1"
            };
            decimal credit = 5000;
            int installments = 11;

            // Act
            var creditProposal = CreditProposal.Create(proponent, creditAgreement, credit, installments);

            // Assert
            Assert.True(creditProposal.IsSuccess);
        }
    }
}