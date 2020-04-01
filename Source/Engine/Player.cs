using System.ComponentModel.DataAnnotations;
namespace EngineClasses
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string UserName { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public void AddPlayerToDb(Player Player)
        {
            using (var context = new LudoContext())
            {
                context.Player.Add(Player);
                context.SaveChanges();
            }
        }

    }
}
