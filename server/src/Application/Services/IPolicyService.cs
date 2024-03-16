using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services;

public interface IPolicyService
{
    Task<IEnumerable<GetPolicyDto>> GetAllPoliciesAsync();
    Task<GetPolicyDto> GetPolicyByIdAsync(int id);
    Task<GetPolicyDto> CreatePolicyAsync(CreatePolicyDto createPolicyDto);
    Task<GetPolicyDto> UpdatePolicyAsync(int id, Policy policy);
    Task DeletePolicyAsync(int id);
}