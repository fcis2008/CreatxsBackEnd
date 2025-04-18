using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Interfaces;
using Core.Models;

namespace Application.Services
{
    public class BranchService : BaseService<BranchCreateDto, BranchDto, Branch>, IBranchService
    {
        public BranchService(IBaseRepository<Branch> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
