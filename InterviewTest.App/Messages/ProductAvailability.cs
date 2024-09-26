using InterviewTest.App.Models;

namespace InterviewTest.App.Messages;

internal record ProductAvailability(IProduct Product, bool IsAvailable);