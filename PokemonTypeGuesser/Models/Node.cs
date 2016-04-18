using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTypeGuesser.Models
{
    public class Node
    {
        public NodeType NodeType { get; set; }
        public string Caption { get; set; }

        public Node ParentNode { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
    }
}
