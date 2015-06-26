using System;
using AkkaPersistence.Messages;

namespace AkkaPersistence.Models {
    public class Booking {
        public enum BookingStatusEnum { Created, Confirmed, Cancelled }

        public string BookingNo { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime Departure { get; set; }
        public string Route { get; set; }
        public BookingStatusEnum BookingStatus { get; set; }

        public bool HandleEvent(BookingMessage msg) {
            if(msg is InitializeBooking)
                HandleEvent((InitializeBooking) msg);
            else if(msg is BookingConfirmed)
                HandleEvent((BookingConfirmed) msg);
            else if(msg is BookingCancelled)
                HandleEvent((BookingCancelled) msg);
            else
                return false;
            return true;
        }

        public void HandleEvent(InitializeBooking msg) {
            CustomerNumber = msg.CustomerNumber;
            Departure = msg.Departure;
            Route = msg.Route;
            BookingStatus = BookingStatusEnum.Created;
        }

        public void HandleEvent(BookingConfirmed msg) {
            BookingStatus = BookingStatusEnum.Confirmed;
        }

        public void HandleEvent(BookingCancelled msg) {
            BookingStatus = BookingStatusEnum.Cancelled;
        }
    }
}
