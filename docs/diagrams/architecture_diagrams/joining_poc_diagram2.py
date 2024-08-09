from diagrams.k8s.network import Ingress, Service
from diagrams.k8s.compute import Pod
from diagrams.onprem.queue import Kafka

def subsystem2():
    ingress = Ingress("domain.com")
    service = Service("service")
    kafka = Kafka("stream")

    ingress >> service >>[
        Pod("pod1"),
        Pod("pod2"),
        Pod("pod3"),
    ] >> kafka
  
    return kafka