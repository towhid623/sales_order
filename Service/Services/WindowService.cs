using Core.Entities;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Web.Shared.Dtos;

namespace Service.Services
{
    internal class WindowService : IWindowService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WindowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WindowGetByIdDto> AddAsync(WindowAddDto windowAddDto)
        {
            if (string.IsNullOrWhiteSpace(windowAddDto.Name))
                throw new ArgumentException("Name is required");

            if (await _unitOfWork.WindowRepository.Query().AnyAsync(s => s.Name == windowAddDto.Name))
                throw new ArgumentException("Another window already exists with this name");

            var window = await _unitOfWork.WindowRepository.AddAsync(new Window
            {
                Name = windowAddDto.Name
            });

            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(window.Id);
        }

        public async Task<WindowGetByIdDto> UpdateAsync(WindowUpdateDto windowUpdateDto)
        {
            if (string.IsNullOrWhiteSpace(windowUpdateDto.Name))
                throw new ArgumentException("Name is required");

            if (await _unitOfWork.WindowRepository.Query().AnyAsync(s => s.Name == windowUpdateDto.Name && s.Id != windowUpdateDto.Id))
                throw new ArgumentException("Another window already exists with this name");

            var window = await _unitOfWork.WindowRepository.GetByIdAsync(windowUpdateDto.Id);
            if (window == null)
                throw new ArgumentException("Window not found");

            window.Name = windowUpdateDto.Name;

            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(window.Id);
        }

        public async Task DeleteAsync(long id)
        {
            if (await _unitOfWork.OrderWindowRepository.Query().AnyAsync(o => o.WindowId == id))
                throw new ArgumentException("Window cannot be deleted as there are existing orders");

            var window = await _unitOfWork.WindowRepository.GetByIdAsync(id);
            if (window == null)
                throw new ArgumentException("Window not found");

            await _unitOfWork.WindowRepository.DeleteAsync(window);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<WindowGetAllDto>> GetAllAsync()
        {
            return await _unitOfWork.WindowRepository.Query()
                .Select(s => new WindowGetAllDto
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToListAsync();
        }

        public async Task<WindowGetByIdDto> GetByIdAsync(long id)
        {
            var window = await _unitOfWork.WindowRepository.Query()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (window == null)
                throw new ArgumentException("Window not found");

            return new WindowGetByIdDto
            {
                Id = id,
                Name = window.Name
            };
        }
    }
}
