from diagrams import Cluster, Diagram, Edge
from diagrams.aws.compute import ECS
from diagrams.onprem.queue import Kafka
from diagrams.onprem.network import Nginx
from diagrams.azure.database import DatabaseForPostgresqlServers

show_image = True

with Diagram("Parcel service with outbox", show=show_image):

    ingres =  Nginx("ingress")

    with Cluster("puid service"):
        puid_service = ECS("puid")

    with Cluster("parcel service"): 
        parcel_service = ECS("parcel service") 
        outbox_database = DatabaseForPostgresqlServers("outbox database")
        outbox_worker = ECS("outbox worker")

    event_bus = Kafka("events")


    ingres >> parcel_service
    parcel_service << Edge(label = "fetch puid") << puid_service
    parcel_service >> Edge(label = "save outgoing letter") >> outbox_database
    

    outbox_worker << Edge(label = "fetch outgoing letters") << outbox_database
    outbox_worker >> Edge(label = "publish") >> event_bus