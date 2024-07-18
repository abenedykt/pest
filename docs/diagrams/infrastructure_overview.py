from diagrams import Cluster, Diagram, Edge
from diagrams.aws.compute import ECS
from diagrams.onprem.queue import Kafka
from diagrams.elastic.agent import Integrations
from diagrams.onprem.monitoring import Grafana
import sys

if len(sys.argv) > 1 and sys.argv[1] == "--no-show":
    show_image = False
else:
    show_image = True

def edge_persistency():
    return Edge(style="dotted")


with Diagram(show=show_image, direction="TB", filename="infrastructure_overview", outformat="png"):

    with Cluster("persistency layer"):
            parcel_persistency = ECS("Parcel Persistency")
            pricing_persistency = ECS("Parcel Persistency")
            tracing_persistency = ECS("Tracking Persistency")

    with Cluster("services"):
            
        with Cluster("client services"):
            parcel_service = ECS("Parcel Service")
            parcel_service >> edge_persistency() >> parcel_persistency
            
            pricing_service = ECS("Pricing Service")
            pricing_service >> edge_persistency() >> pricing_persistency

            tracking_service = ECS("Tracking Service")
            tracking_service >> edge_persistency() >> tracing_persistency
            
        with Cluster("infrastructure services"):
            scan_machines = [Integrations("machine 1"), Integrations("machine 2"),Integrations("machine n"),]

    with Cluster("infrastructure"):
        event_bus = Kafka("Kafka")
        monitoring = Grafana("Grafana")


    pricing_service << Edge(label = "subscribe", style="bold") << event_bus
    tracking_service << Edge(label = "subscribe", style="bold")  << event_bus
    parcel_service >> Edge(label = "publish", style="bold")  >> event_bus
    scan_machines >> Edge(label = "publish", style="bold") >> event_bus
