using PokemonTypeGuesser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGuessingGame
{
    public partial class Form1 : Form
    {
        private static string DIALOG_TITLE = "Pokémon Guessing Game";

        //Initial node of the tree
        Node root;

        public Form1()
        {
            root = new Node() { NodeType = NodeType.PokemonReferenceNode, Caption = "lives in the water" };
            root.LeftNode = new Node() { NodeType = NodeType.PokemonNameNode, Caption = "Magikarp", ParentNode = root };
            root.RightNode = new Node() { NodeType = NodeType.PokemonNameNode, Caption = "Mankey", ParentNode = root };
        }

        private void StartGame()
        {
            DialogResult result = MessageBox.Show("Thing about a Pokémon...", DIALOG_TITLE, MessageBoxButtons.OKCancel);

            if(result == DialogResult.OK)
                ShowDialogFromNode(root, NodeDirection.Left);
        }

        private void ShowDialogFromNode(Node node, NodeDirection fromDirection)
        {
            string questionText = node.NodeType == NodeType.PokemonReferenceNode ?
                              "Does the Pokémon that you thought about " + node.Caption + "?" :
                              "Is the Pokémon that you thought about a " + node.Caption + "?";

            DialogResult result = MessageBox.Show(questionText, DIALOG_TITLE, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                ReadLeftNode(node);
            else
                ReadRightNode(node, fromDirection);
        }
        
        private void ReadLeftNode(Node node)
        {
            if(node.LeftNode != null)
            {
                ShowDialogFromNode(node.LeftNode, NodeDirection.Left);
            }
            else
            {
                MessageBox.Show("I win!");
                StartGame();
            }
        }

        private void ReadRightNode(Node node, NodeDirection fromDirection)
        {
            if (node.RightNode != null)
            {
                ShowDialogFromNode(node.RightNode, NodeDirection.Right);
            }
            else
            {
                string pokemonName = null;

                while (string.IsNullOrEmpty(pokemonName))
                {
                    pokemonName = Microsoft.VisualBasic.Interaction.InputBox("What was the name of the Pokémon that you thought about?", DIALOG_TITLE);
                }

                string pokemonReference = null;

                while (string.IsNullOrEmpty(pokemonReference))
                {
                    pokemonReference = Microsoft.VisualBasic.Interaction.InputBox("A " + pokemonName + " ______ but a Mankey does not (fill it with a Pokémon trait, like 'uses Tackle')", DIALOG_TITLE);
                }

                Node newPokemonReferenceNode = new Node() { NodeType = NodeType.PokemonReferenceNode, Caption = pokemonReference };
                Node newPokemonNameNode = new Node() { NodeType = NodeType.PokemonNameNode, Caption = pokemonName, ParentNode = newPokemonReferenceNode };
                newPokemonReferenceNode.LeftNode = newPokemonNameNode;
                newPokemonReferenceNode.RightNode = node;
                newPokemonReferenceNode.ParentNode = node.ParentNode;

                if(fromDirection == NodeDirection.Left)
                    node.ParentNode.LeftNode = newPokemonReferenceNode;
                else
                    node.ParentNode.RightNode = newPokemonReferenceNode;

                node.ParentNode = newPokemonReferenceNode;

                StartGame();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            StartGame();
        }

        private void Form1_Load(object sender, EventArgs e) {}
    }

    enum NodeDirection
    {
        Right, Left
    }
}
