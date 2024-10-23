using MediatR;

namespace BemConsignado.HttpService.Domain.Proponents.Handlers
{
    public class CreateProponentHandler(ProponentRepository proponentRepository) : IRequestHandler<CreateProponentCommand>
    {
        public async Task Handle(CreateProponentCommand request, CancellationToken cancellationToken)
        {
            var propoonent = Proponent.Create(request.Cpf, request.Income, request.Address, request.PhoneNumber, request.Email, request.IsActive, request.State, request.BirthDate);
            await proponentRepository.AddAsync(propoonent);
            await proponentRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
