from diagrams import Cluster, Diagram
from diagrams.aws.compute import ECS
from diagrams.onprem.queue import Kafka
from diagrams.onprem.network import Nginx

show_image = True

with Diagram("Parcel service with outbox", show=show_image):

    ingres =  Nginx("ingress")

    with Cluster("parcel service"): 
        parcel_service = ECS("parcel service") 
        outbox_database = ECS("outbox database")
        outbox_worker = ECS("outbox worker")
    
    with Cluster("puid service"):
        puid_service = ECS("puid")

    event_bus = Kafka("events")



    ingres >> parcel_service
    parcel_service << puid_service
    parcel_service >> outbox_database
    

    outbox_worker << outbox_database
    outbox_worker >> event_bus