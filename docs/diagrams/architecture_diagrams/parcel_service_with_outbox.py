from diagrams import Cluster, Diagram, Edge
from diagrams.aws.compute import ECS
from diagrams.onprem.queue import Kafka
from diagrams.onprem.network import Nginx
from diagrams.azure.database import DatabaseForPostgresqlServers
from diagrams.c4 import SystemBoundary

def parcel_service_with_outbox_diagram():

    with SystemBoundary("External"):
        puid_service = ECS("PUID Service")
        event_bus = Kafka("Kafka")

    ingres = Nginx("ingress")

    with SystemBoundary("Parcel Service"):
        with Cluster("Parcel service", direction="LR"):
            parcel_service = ECS("REST API")
            parcel_service >> Edge(label="/GET", style="dotted") >> puid_service
            outbox = DatabaseForPostgresqlServers("Persisted Outbox")
            parcel_service >> Edge(style="bold", label="to outbox") >> outbox
            outbox_worker = ECS("Outbox Worker")
            outbox << Edge(style="bold", label="process") << outbox_worker

        outbox_worker >> Edge(label="publish", style="bold") >> event_bus

        ingres >> parcel_service
