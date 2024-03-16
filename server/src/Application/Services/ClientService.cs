using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return clients.Select(client => _mapper.Map<GetClientDto>(client));
        }

        public async Task<GetClientDto> GetClientByIdAsync(int id)
        {
            Client? client = await _clientRepository.GetByIdAsync(id);
            return _mapper.Map<GetClientDto>(client);
        }

        public async Task<GetClientDto> CreateClientAsync(CreateClientDto createClientDto)
        {
            try
            {
                Client newClient = _mapper.Map<Client>(createClientDto);
                
                await _clientRepository.CreateAsync(newClient);
                
                return _mapper.Map<GetClientDto>(newClient);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar cliente: {ex.Message}.");
            }
        }

        public async Task<GetClientDto> UpdateClientAsync(int id, Client client)
        {
            Client? existingClient = await _clientRepository.GetByIdAsync(id);

            if (existingClient == null)
            {
                throw new ArgumentException("O cliente especificado não existe.");
            }

            existingClient.Cpf = client.Cpf;
            existingClient.Name = client.Name;
            existingClient.BirthDate = client.BirthDate;
            existingClient.Address = client.Address;

            await _clientRepository.UpdateAsync(existingClient);

            return _mapper.Map<GetClientDto>(existingClient);
        }

        public async Task DeleteClientAsync(int id)
        {
            Client? clientToDelete = await _clientRepository.GetByIdAsync(id);

            if (clientToDelete == null)
            {
                throw new ArgumentException("O cliente especificado não existe.");
            }

            await _clientRepository.DeleteAsync(clientToDelete);
        }
    }
}
