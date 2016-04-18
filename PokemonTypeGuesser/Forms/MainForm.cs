using PokemonTypeGuesser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGuessingGame
{
    public partial class MainForm : Form
    {
        private static string DIALOG_TITLE = "Pokémon Guessing Game";

        //Initial node of the tree
        Node root;

        public MainForm()
        {
            //creating initial tree
            root = new Node() { NodeType = NodeType.PokemonReferenceNode, Caption = "lives in the water" };
            root.LeftNode = new Node() { NodeType = NodeType.PokemonNameNode, Caption = "Magikarp", ParentNode = root };
            root.RightNode = new Node() { NodeType = NodeType.PokemonNameNode, Caption = "Mankey", ParentNode = root };
        }

        //starts or restars the game
        private void StartGame()
        {
            DialogResult result = MessageBox.Show("Thing about a Pokémon...", DIALOG_TITLE, MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
                ShowDialogFromNode(root);
            else
                Application.Exit();
        }

        //Display dialogs from each node
        private void ShowDialogFromNode(Node node)
        {
            string questionText = node.NodeType == NodeType.PokemonReferenceNode ?
                              "Does the Pokémon that you thought about " + node.Caption + "?" :
                              "Is the Pokémon that you thought about a " + node.Caption + "?";

            DialogResult result = MessageBox.Show(questionText, DIALOG_TITLE, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                ReadLeftNode(node);
            else
                ReadRightNode(node);
        }

        //reads left children from node. If there is no left children, then it means that the application chose the right path
        private void ReadLeftNode(Node node)
        {
            lastDirection = NodeDirection.Left;

            if (node.LeftNode != null)
            {
                ShowDialogFromNode(node.LeftNode);
            }
            else
            {
                MessageBox.Show("I win!");
                StartGame();
            }
        }

        //reads right children from node. If there is no right children, then it means that the Application does not know the answer
        private void ReadRightNode(Node node)
        {
            lastDirection = NodeDirection.Right;

            if (node.RightNode != null)
            {
                ShowDialogFromNode(node.RightNode);
            }
            else
            {
                string pokemonName = null;

                while (string.IsNullOrEmpty(pokemonName))
                {
                    pokemonName = ShowDialog("What was the name of the Pokémon that you thought about?", DIALOG_TITLE);
                }

                string pokemonReference = null;

                while (string.IsNullOrEmpty(pokemonReference))
                {
                    pokemonReference = ShowDialog("A " + pokemonName + " ______ but a Mankey does not (fill it with a Pokémon trait, like 'uses Tackle')", DIALOG_TITLE);
                }

                Node newPokemonReferenceNode = new Node() { NodeType = NodeType.PokemonReferenceNode, Caption = pokemonReference };
                Node newPokemonNameNode = new Node() { NodeType = NodeType.PokemonNameNode, Caption = pokemonName, ParentNode = newPokemonReferenceNode };
                newPokemonReferenceNode.LeftNode = newPokemonNameNode;
                newPokemonReferenceNode.RightNode = node;
                newPokemonReferenceNode.ParentNode = node.ParentNode;

                if (lastDirection == NodeDirection.Left)
                    node.ParentNode.LeftNode = newPokemonReferenceNode;
                else
                    node.ParentNode.RightNode = newPokemonReferenceNode;

                node.ParentNode = newPokemonReferenceNode;

                StartGame();
            }
        }

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        protected override void OnLoad(EventArgs e)
        {
            StartGame();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        NodeDirection lastDirection;
    }

    enum NodeDirection
    {
        Right, Left
    }
}
