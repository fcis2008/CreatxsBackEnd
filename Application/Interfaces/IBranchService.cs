using Application.DTOs;
using Core.Models;

namespace Application.Interfaces
{
    public interface IBranchService : IBaseService<BranchCreateDto, BranchDto, Branch>
    {
    }
}
