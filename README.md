# Binary Tree Guessing Game
> An example of a binary tree sctructure for a Pokémon guessing game

This project is an example of a binary tree usage to a guessing game. The idea is that the program keeps asking the user until it discover the name of the Pokémon or gives up by asking its info and adding it to its memory.

We only make questions that can be answered by "true" or "false", so we store the "true" path on the left node and the "false" path on the right node. If the user answers "true", then we go to left, otherwise we go to right. If there is no left node, then the program wins the game. If there is no right node, then the program must ask for the Pokémon info.

Example: Suppose that the program only knows Magikarp and Mankey as Pokémon, and that a Magikarp lives in the water, but a Mankey does not. The tree would initally looks like this:

```
                        mankey
                     /
> lives in the water
                     \
                       magikarp
```

The Program starts by asking if the Pokémon lives in the water. If the user says "yes", then the program goes left, otherwise it goes right. Suppose it went left, so the tree will look like this

```
                      mankey
                   /
lives in the water
                   \
                     > magikarp
```
This is a "Pokémon" node, so the program will ask if it's a "Magikarp". If the user say "yes", then the program wins. Otherwise, there will be no child on the right, and the program must ask two things:

1. The Pokémon "trait"
2. The Pokémon name

Suppose that the user tought about a Rattata, which we know that knows the move "super fang" and Magikarp does not. So we must insert a new node on the parent of last node, link the left node to the left of this new node and add a new node to the right of the new node. The tree would be like this:

```
                      mankey
                   /
lives in the water                                       magikarp
                   \                                   /
                     does it knows the move super fang? 
                                                       \
                                                         rattata
```

As the game repeats, the program will add knowledge of existing pokémon and it's uniques.

## Development
This project is really simple. We have the tree structure inside "Models" folder, and all the program logic is inside "MainForm.cs"
