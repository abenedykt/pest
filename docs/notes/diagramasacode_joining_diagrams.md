Working with diagrams in a more complex solutions often requires to show different perspective on a system. More than often we need to draw view from numerous angles. With Diagram as as Code approach it's quite easy to mix and match once defined diagram without continuos copy/paste method. Simply follow DRY principle. 

Bellow you can see working PoC. Different library may require different steps but you get the idea. 

To mix and match diagrams we can wrap each diagram in a function. Below theres just and example of imaginary system. Later we will be creating a combined diagram connecting different partial diagrams, so here we return kafka (just as an example).

```python
# ../diagrams/architecture_diagrams/joining_poc_diagram1.py
```
No we need another subsystem:

```python
# ../diagrams/architecture_diagrams/joining_poc_diagram2.py
```
Just like before we want to return kafka. We could return a tuple of multiple elements though. For the sake of example lets keep it as simple as possible.

Now the fun part. Lets create 3 diagrams. Two subsubsystems and one combined one. 

```python
# ../diagrams/multiple_diagrams.py


```

Now running the last scrip will generate the following images:

First subsystem:
![first subsystem](../diagrams/poc_diagram1.png)

Second subsystem:
![second subsystem](../diagrams/poc_diagram2.png)

Combined diagrams:
![combined diagrams](../diagrams/poc_combined.png)