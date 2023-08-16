using cargosiklink.Models.Enum;

namespace cargosiklink.Models
{
    public class State
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StateType Type { get; set; }
        public string Color { get; set; }
        public State(string name, StateType type, string color)
        {
            Id = Guid.NewGuid();
            Name = name; 
            Type = type; 
            Color = color;
        }
        public State(Guid id, string name, StateType type, string color)
        {
            Id = id;
            Name = name;
            Type = type;
            Color = color;
        }
    }
}
