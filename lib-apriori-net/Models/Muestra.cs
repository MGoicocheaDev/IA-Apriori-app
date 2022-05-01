namespace lib_apriori_net.Models
{
    public class Muestra
    {
        private string _name;
        public string Name { get => string.IsNullOrWhiteSpace(_name) ? string.Empty : _name; set { _name = value; } }

        public Muestra(string name)
        {
            this.Name = name;
        }
        
    }
}
