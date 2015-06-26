using System;

namespace AkkaPersistence.Messages {
    public class InitializeBooking : BookingMessage {
        public string Route { get; set; }
        public DateTime Departure { get; set; }
        public string CustomerNumber { get; set; }
    }
}