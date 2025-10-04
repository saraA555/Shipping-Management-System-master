﻿using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;

namespace ITI.Shipping.Core.Application.Services.BranchServices
{
    internal class BranchService:IBranchService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;
        public BranchService(IUnitOfWork UnitOfWork , IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _Mapper = mapper;
        }
        // Get All Branches
        public async Task<IEnumerable<BranchDTO>> GetBranchesAsync(Pramter pramter)
        {
            return _Mapper.Map<IEnumerable<BranchDTO>>(await _UnitOfWork.GetBranchesRepository().GetAllAsync(pramter));
        }
        // Get Branches By Region ID 
        public async Task<IEnumerable<BranchDTO>> GetBranchesByRegionIdAsync(int regionId)
        {
            return _Mapper.Map<IEnumerable<BranchDTO>>(await _UnitOfWork.GetBranchesRepository().GetBranchesByRegionIdAsync(regionId));
        }
        // Get Branches By City Setting ID
        public async Task<IEnumerable<BranchDTO>> GetBranchesByCitySettingIdAsync(int citySettingId)
        {
            return _Mapper.Map<IEnumerable<BranchDTO>>(await _UnitOfWork.GetBranchesRepository().GetBranchesByCitySettingIdAsync(citySettingId));
        }
        // Get Branch By ID
        public async Task<BranchDTO> GetBranchAsync(int id)
        {
            return _Mapper.Map<BranchDTO>(await _UnitOfWork.GetBranchesRepository().GetByIdAsync(id));
        }
        // Add Branch
        public async Task AddAsync(BranchToAddDTO DTO)
        {
            await _UnitOfWork.GetBranchesRepository().AddAsync(_Mapper.Map<Branch>(DTO));
            await _UnitOfWork.CompleteAsync();
        }
        // Update Branch
        public async Task UpdateAsync(BranchToUpdateDTO DTO)
        {
            var branchRepo = _UnitOfWork.GetBranchesRepository();

            var existingBranch = await branchRepo.GetByIdAsync(DTO.Id);
            if(existingBranch == null)
                throw new KeyNotFoundException($"Branch with ID {DTO.Id} not found.");

            _Mapper.Map(DTO,existingBranch); 

            branchRepo.UpdateAsync(existingBranch);
            await _UnitOfWork.CompleteAsync();
        }
        // Delete Branch
        public async Task DeleteAsync(int id) 
        {
            var branchRepo = _UnitOfWork.GetBranchesRepository();

            var existingBranch = await branchRepo.GetByIdAsync(id);
            if(existingBranch == null)
                throw new KeyNotFoundException($"Branch with ID {id} not found.");

            await branchRepo.DeleteAsync(id); 
            await _UnitOfWork.CompleteAsync();
        }

        
    }
}
