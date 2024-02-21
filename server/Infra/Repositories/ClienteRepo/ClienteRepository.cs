using AutoMapper;
using Domain.Models.DTO.ClienteDTO;
using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.ClienteRepo;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ClienteRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> CreateAsync(Cliente novoCliente)
    {
        _context.Clientes.Add(novoCliente);
        await _context.SaveChangesAsync();

        return novoCliente;
    }

    public async Task UpdateAsync(int id, UpdateClienteDto updateClienteDto)
    {
        Cliente clienteExistente = await GetByIdAsync(id);
        if (clienteExistente != null)
        {
            _mapper.Map(updateClienteDto, clienteExistente);
        
            _context.Entry(clienteExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(int id)
    {
        Cliente cliente = await GetByIdAsync(id);

        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();
        }
    }
}