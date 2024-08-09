from diagrams import Diagram, Cluster
from architecture_diagrams.joining_poc_diagram1 import subsystem1
from architecture_diagrams.joining_poc_diagram2 import subsystem2
from tools.DiagramTools import based_on_params

show_image = based_on_params()

# generate diagram for system 1
with Diagram(show=show_image, direction="LR", filename="poc_diagram1", outformat="png"):
    subsystem1()

# generate diagram for system 2
with Diagram(show=show_image, direction="LR", filename="poc_diagram2", outformat="png"):
    subsystem2()


# generate diagram for combined systems
with Diagram(show=show_image, direction="LR", filename="poc_combined", outformat="png"):
    with Cluster("Subsystem 1"):
        kafka1 = subsystem1()

    with Cluster("Subsystem 2"):
        kafka2 = subsystem2()

    kafka1 - kafka2


