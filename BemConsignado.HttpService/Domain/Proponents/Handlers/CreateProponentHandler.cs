using MediatR;

namespace BemConsignado.HttpService.Domain.Proponents.Handlers
{
    public class CreateProponentHandler(IProponentRepository proponentRepository) : IRequestHandler<CreateProponentCommand, Proponent>
    {
        public async Task<Proponent> Handle(CreateProponentCommand request, CancellationToken cancellationToken)
        {
            var proponent = Proponent.Create(request.Cpf, request.Name, request.Income, request.Address, request.PhoneNumber, request.Email, request.IsActive, request.State, request.BirthDate);
            await proponentRepository.AddAsync(proponent);
            await proponentRepository.UnitOfWork.SaveChangesAsync();
            return proponent;
        }
    }
}
