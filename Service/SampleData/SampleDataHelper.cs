using Core.Entities;
using Data.UnitOfWorks;
using System.Xml.Serialization;

namespace Service.SampleData
{
    internal class SampleDataHelper : ISampleDataHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public SampleDataHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SeedDataAsync()
        {
            var alreadySeeded = await _unitOfWork.OrderRepository.AnyAsync();
            if (!alreadySeeded)
            {
                var xmlString = @"<Orders>
					<Order Name=""New York Building 1"" State=""NY"">
						<Windows>
							<Window Name=""A51"" QuantityOfWindows=""4"" TotalSubElements=""3"">
								<SubElements>
									<SubElement Element=""1"" Type=""Doors"" Width=""1200"" Height=""1850"" />
									<SubElement Element=""2"" Type=""Window"" Width=""800"" Height=""1850"" />
									<SubElement Element=""3"" Type=""Window"" Width=""700"" Height=""1850"" />
								</SubElements>
							</Window>
							<Window Name=""C Zone 5"" QuantityOfWindows=""2"" TotalSubElements=""1"">
								<SubElements>
									<SubElement Element=""1"" Type=""Window"" Width=""1500"" Height=""2000"" />
								</SubElements>
							</Window>
						</Windows>
					</Order>
					<Order Name=""California Hotel AJK"" State=""CA"">
						<Windows>
							<Window Name=""GLB"" QuantityOfWindows=""3"" TotalSubElements=""2"">
								<SubElements>
									<SubElement Element=""1"" Type=""Doors"" Width=""1400"" Height=""2200"" />
									<SubElement Element=""2"" Type=""Window"" Width=""600"" Height=""2200"" />

								</SubElements>
							</Window>
							<Window Name=""OHF"" QuantityOfWindows=""10"" TotalSubElements=""2"">
								<SubElements>
									<SubElement Element=""1"" Type=""Window"" Width=""1500"" Height=""2000"" />
									<SubElement Element=""1"" Type=""Window"" Width=""1500"" Height=""2000"" />
								</SubElements>
							</Window>
						</Windows>
					</Order>
				</Orders>";

                // Create an instance of XmlSerializer for your C# class
                var serializer = new XmlSerializer(typeof(Orders));

                // Create a StringReader object from the XML string
                var stringReader = new StringReader(xmlString);

                // Deserialize the XML string to a C# object
                var sampleOrders = serializer.Deserialize(stringReader) as Orders;

                if (sampleOrders?.Order?.Length > 0)
                {
                    foreach (var sampleOrder in sampleOrders.Order)
                    {
                        var customer = new Customer
                        {
                            Name = sampleOrder.Name
                        };

                        await _unitOfWork.CustomerRepository.AddAsync(customer);
                        await _unitOfWork.CompleteAsync();

                        var state = new State
                        {
                            Name = sampleOrder.State
                        };

                        await _unitOfWork.StateRepository.AddAsync(state);
                        await _unitOfWork.CompleteAsync();

                        var order = new Order
                        {
                            CustomerId = customer.Id,
                            StateId = state.Id
                        };

                        await _unitOfWork.OrderRepository.AddAsync(order);
                        await _unitOfWork.CompleteAsync();

                        if (sampleOrder.Windows?.Length > 0)
                        {
                            foreach (var sampleWindow in sampleOrder.Windows)
                            {
                                var window = new Window
                                {
                                    Name = sampleWindow.Name
                                };

                                await _unitOfWork.WindowRepository.AddAsync(window);
                                await _unitOfWork.CompleteAsync();

                                var orderWindow = new OrderWindow
                                {
                                    OrderId = order.Id,
                                    WindowId = window.Id,
                                    Quantity = sampleWindow.QuantityOfWindows
                                };

                                await _unitOfWork.OrderWindowRepository.AddAsync(orderWindow);
                                await _unitOfWork.CompleteAsync();

                                if (sampleWindow.SubElements?.Length > 0)
                                {
                                    foreach (var sampleSubElement in sampleWindow.SubElements)
                                    {
                                        var orderWindowElement = new OrderWindowElement
                                        {
                                            Element = sampleSubElement.Element,
                                            Height = sampleSubElement.Height,
                                            OrderWindowId = orderWindow.Id,
                                            Type = sampleSubElement.Type,
                                            Width = sampleSubElement.Width
                                        };

                                        await _unitOfWork.OrderWindowElementRepository.AddAsync(orderWindowElement);
                                        await _unitOfWork.CompleteAsync();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public interface ISampleDataHelper
    {
        Task SeedDataAsync();
    }
}
