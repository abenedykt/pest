from diagrams import Cluster, Diagram, Edge
from diagrams.aws.compute import ECS
from diagrams.onprem.queue import Kafka
from diagrams.onprem.network import Nginx
from diagrams.azure.database import DatabaseForPostgresqlServers
from architecture_diagrams.diagram1 import diag1
from architecture_diagrams.diagram2 import diag2
from tools.DiagramTools import based_on_params

show_image = based_on_params()

with Diagram(show=show_image, direction="LR", filename="join1", outformat="png"):
    diag1()

with Diagram(show=show_image, direction="LR", filename="join2", outformat="png"):
    diag2()


with Diagram(show=show_image, direction="LR", filename="join3", outformat="png"):
    with Cluster("Services"):
        puid, rest, hello = diag1()

    with Cluster("Other stuff"):
        box, worker = diag2()   

    puid >> worker


