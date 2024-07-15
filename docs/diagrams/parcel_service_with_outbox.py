from diagrams import Cluster, Diagram, Edge
from diagrams.aws.compute import ECS
from diagrams.onprem.queue import Kafka
from diagrams.onprem.network import Nginx
from diagrams.azure.database import DatabaseForPostgresqlServers

show_image = True

with Diagram(show=show_image, direction="LR", filename="parcel_service_with_outbox", outformat="png"):

    ingres =  Nginx("ingress")

    with Cluster("PUID service"):
        puid_service = ECS("puid")
    
    
    with Cluster("Events"):
        event_bus = Kafka("Kafka")     

    with Cluster("Parcel service"):         
        parcel_service = ECS("REST API") 
        outbox = DatabaseForPostgresqlServers("Outbox")
        parcel_service >> [puid_service, outbox]
        outbox_worker = ECS("Outbox Worker")
        outbox >> outbox_worker

    outbox_worker >> Edge(label = "publish") >> event_bus
    ingres >> parcel_service