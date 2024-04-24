using System.ComponentModel;
using System.Text;
namespace Model

{
    public class Item
    {

        private StringBuilder Data { get; set; }

        public Item(StringBuilder data)
        {
            Data = data;
        }

    }
}

