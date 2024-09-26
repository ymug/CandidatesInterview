using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
