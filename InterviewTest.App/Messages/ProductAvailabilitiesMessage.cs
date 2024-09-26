using CommunityToolkit.Mvvm.Messaging.Messages;
using InterviewTest.App.Models;

namespace InterviewTest.App.Messages
{
    internal class ProductAvailabilitiesMessage : ValueChangedMessage<ProductAvailability[]>

    {
        public ProductAvailabilitiesMessage(ProductAvailability[] availabilities) : base(availabilities)
        {
        }
    }
}
