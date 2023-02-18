using Core.Entities;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Web.Shared.Dtos;

namespace Service.Services
{
    internal class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StateGetByIdDto> AddAsync(StateAddDto stateAddDto)
        {
            if (string.IsNullOrWhiteSpace(stateAddDto.Name))
                throw new ArgumentException("Name is required");

            if (await _unitOfWork.StateRepository.Query().AnyAsync(s => s.Name == stateAddDto.Name))
                throw new ArgumentException("Another state already exists with this name");

            var state = await _unitOfWork.StateRepository.AddAsync(new State
            {
                Name = stateAddDto.Name
            });

            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(state.Id);
        }

        public async Task<StateGetByIdDto> UpdateAsync(StateUpdateDto stateUpdateDto)
        {
            if (string.IsNullOrWhiteSpace(stateUpdateDto.Name))
                throw new ArgumentException("Name is required");

            if (await _unitOfWork.StateRepository.Query().AnyAsync(s => s.Name == stateUpdateDto.Name && s.Id != stateUpdateDto.Id))
                throw new ArgumentException("Another state already exists with this name");

            var state = await _unitOfWork.StateRepository.GetByIdAsync(stateUpdateDto.Id);
            if (state == null)
                throw new ArgumentException("State not found");

            state.Name = stateUpdateDto.Name;

            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(state.Id);
        }

        public async Task DeleteAsync(long id)
        {
            if (await _unitOfWork.OrderRepository.Query().AnyAsync(o => o.StateId == id))
                throw new ArgumentException("State cannot be deleted as there are existing orders");

            var state = await _unitOfWork.StateRepository.GetByIdAsync(id);
            if (state == null)
                throw new ArgumentException("State not found");

            await _unitOfWork.StateRepository.DeleteAsync(state);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<StateGetAllDto>> GetAllAsync()
        {
            return await _unitOfWork.StateRepository.Query()
                .Select(s => new StateGetAllDto
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToListAsync();
        }

        public async Task<StateGetByIdDto> GetByIdAsync(long id)
        {
            var state = await _unitOfWork.StateRepository.Query()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (state == null)
                throw new ArgumentException("State not found");

            return new StateGetByIdDto
            {
                Id = id,
                Name = state.Name
            };
        }
    }
}
