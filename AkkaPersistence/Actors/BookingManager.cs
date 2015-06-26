using System.Collections.Generic;
using Akka.Actor;
using AkkaPersistence.Messages;

namespace AkkaPersistence.Actors {
    public class BookingManager : ReceiveActor {
        private readonly IDictionary<string, IActorRef> _bookingActors = new Dictionary<string, IActorRef>();

        public BookingManager() {
            Receive<BookingMessage>(msg => {
                if(!_bookingActors.ContainsKey(msg.BookingNo)) {
                    var newActor = Context.System.ActorOf(Props.Create(() => new BookingActor(msg.BookingNo)));
                    _bookingActors.Add(msg.BookingNo, newActor);
                }
                _bookingActors[msg.BookingNo].Forward(msg);
            });
        }
    }
}