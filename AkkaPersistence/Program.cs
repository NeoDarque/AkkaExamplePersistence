using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Persistence.SqlServer;
using AkkaPersistence.Actors;
using AkkaPersistence.Messages;
using AkkaPersistence.Views;

namespace AkkaPersistence {
    class Program {
        static void Main(string[] args) {
            using(var system = ActorSystem.Create("PersistenceSystem")) {
                SqlServerPersistence.Init(system);
                var manager = system.ActorOf<BookingManager>("booking-manager");

                var bookingReadView = system.ActorOf(Props.Create(() => new BookingView("7072401234")));

                //manager.Tell(new InitializeBooking {
                //    BookingNo = "7072401234",
                //    CustomerNumber = "12345",
                //    Departure = DateTime.Now,
                //    Route = "GOT-FRE"
                //});

                //manager.Tell(new BookingConfirmed { BookingNo = "7072401234" });
                //manager.Tell(new BookingCancelled { BookingNo = "7072401234" });

                system.AwaitTermination();
            }
        }
    }
}
