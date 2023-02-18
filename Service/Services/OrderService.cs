using Core.Entities;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System.Xml.Linq;
using Web.Shared.Dtos;

namespace Service.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderGetByIdDto> AddAsync(OrderAddDto orderAddDto)
        {
            try
            {
                await _unitOfWork.BeginAsync();

                if (orderAddDto.CustomerId <= 0)
                    throw new ArgumentException("Customer is required");

                if (!await _unitOfWork.CustomerRepository.Query().AnyAsync(c => c.Id == orderAddDto.CustomerId))
                    throw new ArgumentException("Customer not found");

                if (orderAddDto.StateId <= 0)
                    throw new ArgumentException("State is required");

                if (!await _unitOfWork.StateRepository.Query().AnyAsync(s => s.Id == orderAddDto.StateId))
                    throw new ArgumentException("State not found");

                var order = await _unitOfWork.OrderRepository.AddAsync(new Order
                {
                    CustomerId = orderAddDto.CustomerId,
                    StateId = orderAddDto.StateId
                });

                await _unitOfWork.CompleteAsync();

                if (orderAddDto.Windows?.Count > 0)
                {
                    foreach (var window in orderAddDto.Windows)
                    {
                        if (window.WindowId <= 0)
                            throw new ArgumentException("Window is required");

                        if (!await _unitOfWork.WindowRepository.Query().AnyAsync(w => w.Id == window.WindowId))
                            throw new ArgumentException("Window not found");

                        var orderWindow = await _unitOfWork.OrderWindowRepository.AddAsync(new OrderWindow
                        {
                            OrderId = order.Id,
                            Quantity = window.Quantity,
                            WindowId = window.WindowId
                        });

                        await _unitOfWork.CompleteAsync();

                        if (window.SubElements?.Count > 0)
                        {
                            foreach (var subElement in window.SubElements)
                            {
                                await _unitOfWork.OrderWindowElementRepository.AddAsync(new OrderWindowElement
                                {
                                    Element = subElement.Element,
                                    Height = subElement.Height,
                                    Width = subElement.Width,
                                    OrderWindowId = orderWindow.Id,
                                    Type = subElement.Type
                                });

                                await _unitOfWork.CompleteAsync();
                            }
                        }
                    }
                }

                await _unitOfWork.CommitAsync();

                return await GetByIdAsync(order.Id);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<OrderGetByIdDto> UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            try
            {
                await _unitOfWork.BeginAsync();

                var order = await _unitOfWork.OrderRepository.Query()
                    .Include(o => o.OrderWindows).ThenInclude(ow => ow.OrderWindowElements)
                    .FirstOrDefaultAsync(o => o.Id == orderUpdateDto.Id);

                if (order == null)
                    throw new ArgumentException("Order not found");

                if (orderUpdateDto.CustomerId <= 0)
                    throw new ArgumentException("Customer is required");

                if (!await _unitOfWork.CustomerRepository.Query().AnyAsync(c => c.Id == orderUpdateDto.CustomerId))
                    throw new ArgumentException("Customer not found");

                if (orderUpdateDto.StateId <= 0)
                    throw new ArgumentException("State is required");

                if (!await _unitOfWork.StateRepository.Query().AnyAsync(s => s.Id == orderUpdateDto.StateId))
                    throw new ArgumentException("State not found");

                order.StateId = orderUpdateDto.StateId;
                order.CustomerId = orderUpdateDto.CustomerId;

                await _unitOfWork.CompleteAsync();

                if (orderUpdateDto.Windows?.Count > 0)
                {
                    foreach (var window in orderUpdateDto.Windows)
                    {
                        if (window.WindowId <= 0)
                            throw new ArgumentException("Window is required");

                        if (!await _unitOfWork.WindowRepository.Query().AnyAsync(w => w.Id == window.WindowId))
                            throw new ArgumentException("Window not found");

                        OrderWindow? orderWindow;
                        if (window.Id > 0)
                        {
                            orderWindow = await _unitOfWork.OrderWindowRepository.GetByIdAsync(window.Id.Value);
                            if (orderWindow == null)
                                throw new ArgumentException("Order window not found");

                            orderWindow.Quantity = window.Quantity;
                            orderWindow.WindowId = window.WindowId;
                        }
                        else
                        {
                            orderWindow = await _unitOfWork.OrderWindowRepository.AddAsync(new OrderWindow
                            {
                                OrderId = order.Id,
                                Quantity = window.Quantity,
                                WindowId = window.WindowId
                            });
                        }

                        await _unitOfWork.CompleteAsync();

                        if (window.SubElements?.Count > 0 && orderWindow != null)
                        {
                            foreach (var subElement in window.SubElements)
                            {
                                if (subElement.Id > 0)
                                {
                                    var orderWindowElement = await _unitOfWork.OrderWindowElementRepository.GetByIdAsync(subElement.Id.Value);
                                    if (orderWindowElement == null)
                                        throw new ArgumentException("Order window sub element not found");

                                    orderWindowElement.Element = subElement.Element;
                                    orderWindowElement.Height = subElement.Height;
                                    orderWindowElement.Width = subElement.Width;
                                    orderWindowElement.Type = subElement.Type;
                                }
                                else
                                {
                                    await _unitOfWork.OrderWindowElementRepository.AddAsync(new OrderWindowElement
                                    {
                                        Element = subElement.Element,
                                        Height = subElement.Height,
                                        Width = subElement.Width,
                                        OrderWindowId = orderWindow.Id,
                                        Type = subElement.Type
                                    });
                                }

                                await _unitOfWork.CompleteAsync();
                            }

                            var deleteableOrderWindowElements = await _unitOfWork.OrderWindowElementRepository.Query()
                                .Where(e => e.OrderWindowId == window.Id
                                    && !window.SubElements.Select(se => se.Id).Contains(e.Id))
                                .ToListAsync();
                            await _unitOfWork.OrderWindowElementRepository.DeleteAsync(deleteableOrderWindowElements);
                            await _unitOfWork.CompleteAsync();
                        }
                    }

                    var deleteableOrderWindows = await _unitOfWork.OrderWindowRepository.Query()
                        .Where(ow => ow.OrderId == order.Id
                            && !orderUpdateDto.Windows.Select(w => w.Id).Contains(ow.Id))
                        .ToListAsync();
                    await _unitOfWork.OrderWindowRepository.DeleteAsync(deleteableOrderWindows);
                    await _unitOfWork.CompleteAsync();
                }

                await _unitOfWork.CommitAsync();

                return await GetByIdAsync(order.Id);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(long id)
        {
            var order = await _unitOfWork.OrderRepository.Query()
                .Include(o => o.OrderWindows).ThenInclude(ow => ow.OrderWindowElements)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new ArgumentException("Order not found");

            await _unitOfWork.OrderWindowElementRepository.DeleteAsync(order.OrderWindows.SelectMany(ow => ow.OrderWindowElements).ToList());
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.OrderWindowRepository.DeleteAsync(order.OrderWindows);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.OrderRepository.DeleteAsync(order);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<OrderGetAllDto>> GetAllAsync()
        {
            return await _unitOfWork.OrderRepository.Query()
                .Include(o => o.Customer)
                .Include(o => o.State)
                .Select(o => new OrderGetAllDto
                {
                    Id = o.Id,
                    Customer = o.Customer != null ? o.Customer.Name : null,
                    State = o.State != null ? o.State.Name : null
                }).ToListAsync();
        }

        public async Task<OrderGetByIdDto> GetByIdAsync(long id)
        {
            var order = await _unitOfWork.OrderRepository.Query()
                .Include(o => o.Customer)
                .Include(o => o.State)
                .Include(o => o.OrderWindows).ThenInclude(ow => ow.Window)
                .Include(o => o.OrderWindows).ThenInclude(ow => ow.OrderWindowElements)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new ArgumentException("Order not found");

            return new OrderGetByIdDto
            {
                Id = id,
                CustomerId = order.CustomerId,
                StateId = order.StateId,
                Customer = order.Customer?.Name,
                State = order.State?.Name,
                Windows = order.OrderWindows?.Select(ow => new OrderGetByIdWindowDto
                {
                    Id = ow.Id,
                    WindowId = ow.WindowId,
                    Name = ow.Window?.Name,
                    Quantity = ow.Quantity,
                    NoOfSubElements = ow.OrderWindowElements?.Count ?? 0,
                    SubElements = ow.OrderWindowElements?.Select(e => new OrderGetByIdSubElementDto
                    {
                        Id = e.Id,
                        Element = e.Element,
                        Height = e.Height,
                        Type = e.Type,
                        Width = e.Width
                    })?.ToList()
                })?.ToList()
            };
        }
    }
}
