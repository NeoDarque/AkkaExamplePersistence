using System;
using Akka.Persistence;
using AkkaPersistence.Messages;
using AkkaPersistence.Models;

namespace AkkaPersistence.Views {
    public class BookingView : PersistentView {
        private readonly Booking _bookingData;

        public override string ViewId { get { return "view-"+_bookingData.BookingNo; } }
        public override string PersistenceId { get { return _bookingData.BookingNo; } }

        public BookingView(string bookingNo) {
            _bookingData = new Booking { BookingNo = bookingNo };
        }

        //protected override void PreStart() {
        //    base.PreStart();
        //    Context.System.EventStream.Subscribe(Self, typeof(BookingMessage));
        //}

        protected override bool Receive(object message) {
            var bookingMessage = message as BookingMessage;

            if(bookingMessage != null) {
                if(bookingMessage.BookingNo == _bookingData.BookingNo) {
                    _bookingData.HandleEvent(bookingMessage);
                    Console.WriteLine("Read view updated with {0}", message.GetType().Name);
                }
                return true;
            }

            throw new NotImplementedException();
        }
    }
}