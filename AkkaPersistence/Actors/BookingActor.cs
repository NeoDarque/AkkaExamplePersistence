using System;
using Akka.Persistence;
using AkkaPersistence.Messages;
using AkkaPersistence.Models;

namespace AkkaPersistence.Actors {
    public class BookingActor : PersistentActor {
        private readonly Booking _bookingData;

        public override string PersistenceId { get { return _bookingData.BookingNo; } }

        public BookingActor(string bookingNo) {
            _bookingData = new Booking { BookingNo = bookingNo };
        }

        protected override bool ReceiveRecover(object message) {
            if(message is RecoveryCompleted) {
                return true;

            } else if(message is BookingMessage) {
                return _bookingData.HandleEvent((BookingMessage) message);

            }

            throw new NotImplementedException();
        }

        protected override bool ReceiveCommand(object message) {
            if(message is BookingMessage) {
                Persist((BookingMessage)message, e => {
                    _bookingData.HandleEvent(e);
                    //Context.System.EventStream.Publish(e);
                });
                return true;
            }

            Unhandled(message);
            return false;
        }
    }
}
