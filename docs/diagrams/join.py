from diagrams import Cluster, Diagram, Edge
from diagrams.aws.compute import ECS
from diagrams.onprem.queue import Kafka
from diagrams.onprem.network import Nginx
from diagrams.azure.database import DatabaseForPostgresqlServers
from tools.DiagramTools import based_on_params

show_image = based_on_params()

def diag1():
    puid = ECS("PUID Service")
    hello = ECS("Hello Service")
    rest = ECS("REST API")
    whartever = ECS("Whatever Service")

    puid >> rest >> [hello, whartever] >> puid


    return puid, rest, hello

def diag2():
    box = ECS("Persisted Outbox")
    worker = ECS("Outbox Worker")

    box >> worker

    return box, worker

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


