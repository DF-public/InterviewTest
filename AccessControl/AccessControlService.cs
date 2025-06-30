namespace AccessControl
{
    public class AccessControlService
    {
        public Door AddDoor(int doorNumber, int doorType, string doorName)
        {
            using (var db = new DBContext())
            {
                Door d = new Door() { Id = "D" + doorNumber + "T" + doorType, Number = doorNumber, Name = doorName, CreatedAt = DateTime.Now };

                if (doorType == 0)
                {
                    d.Description = "Regular door";
                }
                else if (doorType == 1)
                {
                    d.Description = "Tripod";
                }
                else if (doorType == 2)
                {
                    d.Description = "Elevator";
                }

                if (doorNumber < 0 || doorNumber > 100)
                {
                    throw new Exception("Incorrect door number");
                }

                db.Doors.Add(d);
                db.SaveChanges();

                return d;
            }
        }

        public string AddCard(int cardNumber, string firstName, string lastName)
        {
            using (var db = new DBContext())
            {
                Card c = new Card()
                {
                    Id = "Card" + cardNumber,
                    Number = cardNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now + new TimeSpan(365, 0, 0, 0)
                };

                db.Cards.Add(c);
                db.SaveChanges();

                return c.Id;
            }
        }
        public string GrantAccess(int cardNumber, int doorNumber)
        {
            try
            {
                using (var db = new DBContext())
                {
                    db.Cards.Where(c => c.Number == cardNumber).First().DoorsNumbersWithAccess.Add(doorNumber);
                }
            }
            catch (Exception ex)
            {
                return "Failure";
            }

            return "Success";
        }
    }
}